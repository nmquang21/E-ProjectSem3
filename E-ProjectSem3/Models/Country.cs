using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_ProjectSem3.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Country_code { get; set; }
        public string Country_name { get; set; }
    }
}