using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sheddy.Models
{
    public class confirmationcode
    {
        [Required(ErrorMessage ="verification code required",AllowEmptyStrings =false)]
        [DataType(DataType.Text)]
        public string verificationcode { get; set; }
       
    }
}