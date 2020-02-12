using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_ProjectSem3.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter student name")]
        [DisplayName(" User Name")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your avatar ")]
        public string Avatar { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your password ")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter phone number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
            ErrorMessage = "Entered phone format is not valid.")]
        public string Phone { get; set; }

        public virtual WishList WishList { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }


        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }


    }
}