using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using E_ProjectSem3.Models;

namespace E_ProjectSem3.Models
{
    public class Membership
    {
        [Key]
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; }

        public int MemberId { get; set; }
        public virtual Member Member { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}