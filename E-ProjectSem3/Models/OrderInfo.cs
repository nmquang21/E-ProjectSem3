using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_ProjectSem3.Models
{
    public class OrderInfo
    {
        [Key]
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string OrderDescription { get; set; }
        public string BankCode { get; set; }
        public decimal vnp_TransactionNo { get; set; }
        public string vpn_Message { get; set; }
        public string vpn_TxnResponseCode { get; set; } }
        public enum OrderStatus
        {
            Pending = 5,
            Confirmed = 4,
            Shipping = 3,
            Paid = 2,
            Done = 1,
            Cancel = 0,
            Deleted = -1
        }
}