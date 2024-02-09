using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sheddy.Models
{
    public class SignInModel
    {

        [Required(ErrorMessage = "Email Required", AllowEmptyStrings = false)]
       [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Required")]
        public string UserPassword { get; set; }
        public Nullable<int> active { get; set; }

        public virtual UserAccount UserAccount { get; set; }
    }
}