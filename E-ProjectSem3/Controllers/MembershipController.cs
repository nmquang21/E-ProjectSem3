using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_ProjectSem3.Models;
using log4net;
using Microsoft.AspNet.Identity;
using WebGrease;
using LogManager = log4net.LogManager;

namespace E_ProjectSem3.Controllers
{
    public class MembershipController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        [Authorize]
        // GET: Membership
        public ActionResult VnPayMembership(string amount, string description)
        {
            var getAmount = Convert.ToDecimal(amount);
            var listMemberType = db.Members.ToList();
            var validate = false;
            foreach (var memberType in listMemberType)
            {
                if (description == memberType.MemberType && getAmount == memberType.Price)
                {
                    validate = true;
                    break;
                }
            }

            if (validate == false)
            {
                return RedirectToAction("Index", "Home");
            }

            //Get Config Info
            string vnp_Returnurl = "https://localhost:44302/Home"; //URL nhan ket qua tra ve 
            string vnp_Url = "http://sandbox.vnpayment.vn/paymentv2/vpcpay.html"; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = "P1V8JH37"; //Ma website
            string vnp_HashSecret = "XAUJIMFNKYUUWWNWOLLNIHJCUGLOIGEF"; //Chuoi bi mat


            //Get payment input
            OrderInfo order = new OrderInfo();
            //Save order to db
            order.OrderId = "DH" + DateTime.Now.Ticks.ToString();
            TempData["OrderId"] = order.OrderId;
            order.Status = Convert.ToInt16(OrderStatus.Pending);
            order.UserId = User.Identity.GetUserId();
            order.Amount = Math.Round(getAmount);
            order.OrderDescription = description;
            order.CreatedAt = DateTime.Now.ToString();


            db.OrderInfos.Add(order);
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", "2.0.0");
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (order.Amount * 23145 * 100).ToString());
            vnpay.AddRequestData("vnp_BankCode", "NCB");
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", order.OrderDescription);
            vnpay.AddRequestData("vnp_OrderType", "190002"); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.OrderId.ToString());

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            log.InfoFormat("VNPAY URL: {0}", paymentUrl);
            return Redirect(paymentUrl);
        }
    }
}