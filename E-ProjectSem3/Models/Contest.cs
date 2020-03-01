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
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter contest name ")]
        [DisplayName(" Contest Name")]
        
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter description ")]
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Prize> Prizes { get; set; }
        public virtual ICollection<ContestUser> ContestUsers { get; set; }

    }
}