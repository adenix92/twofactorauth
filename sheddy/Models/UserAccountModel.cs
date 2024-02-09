using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sheddy.Models
{
    public class UserAccountModel
    {
        public int UserId { get; set; }
        public string username { get; set; }

        [Required(ErrorMessage = "Email Required", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Display(Name = "Your Name")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Your Name Required", AllowEmptyStrings = false)]
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be between 6 and 20 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string UserPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name ="Repeat Your Password")]
        [Required(ErrorMessage ="Repeat Your Password Required",AllowEmptyStrings =false)]
        [Compare("UserPassword",ErrorMessage ="Password and Confirmation Password must be match")]
        public string VerifyPassword { get; set; }
        public Nullable<System.DateTime> registerdate { get; set; }
        public Nullable<int> active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SignIn> SignIns { get; set; }
    }
}