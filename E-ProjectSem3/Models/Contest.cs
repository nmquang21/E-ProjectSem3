using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_ProjectSem3.Models
{
    public class Contest
    {
        [Key]
        public int ContestId { get; set; }

        public virtual ICollection<ContestUser> ContestUsers { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter contest name ")]
        [DisplayName(" Contest Name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter description ")]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter prize ")]
        public string Prize { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}