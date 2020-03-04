using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using E_ProjectSem3;
using E_ProjectSem3.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace E_ProjectSem3.Controllers
{
    public class PayPalController : Controller
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        private const string BusinessEmail = "sb-u3prf891076@business.example.com";
        private const string Currency = "USD";
        private ApplicationUserManager _userManager;
        protected readonly string CurrentUserId;

        public PayPalController()
        {
            CurrentUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        }

        public PayPalController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }

        [HttpPost]
        public async Task<HttpStatusCodeResult> Receive()
        {
            //Store the IPN received from PayPal
            var listString = LogRequest(Request);
            var input = listString.Aggregate("", (current, item) => current + (item + " = " + Request[item] + "; "));
            Debug.WriteLine(input);

            if (Request["business"] == null || Request["txn_id"] == null ||
                Request["custom"] == null || Request["item_number"] == null)
            {
                return null;
            }

            if (Request["business"] != BusinessEmail) return null;
            if (Request["mc_currency"] != Currency) return null;

            if (!CheckValidAmount(Request["payment_gross"])) return null;
            var amount = double.Parse(Request["payment_gross"]);

            //var user = await UserManager.FindByIdAsync(Request["custom"]);
            //var checkUser = user != null && user.Status == ApplicationUser.UserStatusEnum.Activated;
            //if (!checkUser) return null;

            var order = new OrderInfo()
            {
                Amount = (decimal)amount,
            };

            //Fire and forget verification task
            var result = await Task.Run(async () => await VerifyTask(Request, order));
            return result ? new HttpStatusCodeResult(HttpStatusCode.OK) : null;
            //Reply back a 200 code
        }
        // ReSharper disable once UnusedParameter.Local
        private static IEnumerable<string> LogRequest(HttpRequestBase request)
        {
            return (from object item in request.Params select item.ToString()).ToList();
        }
        private async Task<bool> VerifyTask(HttpRequestBase ipnRequest, OrderInfo order)
        {
            Debug.WriteLine(ipnRequest);
            var verificationResponse = string.Empty;
            try
            {
                var verificationRequest = (HttpWebRequest)WebRequest.Create("https://www.sandbox.paypal.com/cgi-bin/webscr");

                //Set values for the verification request
                verificationRequest.Method = "POST";
                verificationRequest.ContentType = "application/x-www-form-urlencoded";
                var param = Request.BinaryRead(ipnRequest.ContentLength);
                var strRequest = Encoding.ASCII.GetString(param);

                //Add cmd=_notify-validate to the payload
                strRequest = "cmd=_notify-validate&" + strRequest;
                verificationRequest.ContentLength = strRequest.Length;

                //Attach payload to the verification request
                var streamOut = new StreamWriter(verificationRequest.GetRequestStream(), Encoding.ASCII);
                await streamOut.WriteAsync(strRequest);
                streamOut.Close();

                //Send the request to PayPal and get the response
                var streamIn = new StreamReader(verificationRequest.GetResponse().GetResponseStream()
                                                ?? throw new InvalidOperationException());
                verificationResponse = await streamIn.ReadToEndAsync();
                streamIn.Close();
            }
            #pragma warning disable 168
            catch (Exception exception)
            #pragma warning restore 168
            {
                //Capture exception for manual investigation
            }
            return await ProcessVerificationResponse(verificationResponse, order);
        }
        private static async Task<bool> ProcessVerificationResponse(string verificationResponse, OrderInfo order)
        {
            //var project = await Db.Projects.FindAsync(donation.ProjectId);
            //if (project == null) return false;
            //var checkDonation = await Db.Donations.Where(d => d.txn_id == donation.txn_id).FirstOrDefaultAsync();
            //if (checkDonation != null) return false;
            if (verificationResponse.Equals("VERIFIED"))
            {
                order.Status = (int)OrderStatus.Paid;
                order.CreatedAt = DateTime.Now;
                db.Entry(order).State = EntityState.Modified;
            }
            else
            {
                order.Status = (int)OrderStatus.Error;
                order.CreatedAt = DateTime.Now;
                order.UpdatedAt = DateTime.Now;
            }
            var result = order.Status == (int)OrderStatus.Paid;
            db.OrderInfos.Add(order);
            await db.SaveChangesAsync();
            return result;
            // check that Payment_status=Completed
            // check that Txn_id has not been previously processed
            // check that Receiver_email is your Primary PayPal email
            // check that Payment_amount/Payment_currency are correct
            // process payment
        }
        private static bool CheckValidAmount(string amount)
        {
            return double.TryParse(amount, out var output) && !double.IsNaN(output) && !double.IsInfinity(output);
        }
    }
}