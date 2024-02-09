using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sheddy.Models;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography;

namespace sheddy.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        sheddydbEntities db = new sheddydbEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserAccountModel uA)
        {
            string msg = "";
            try
            {
                   if (ModelState.IsValid)
            {
                    UserAccount u = new UserAccount();
                    u.Firstname = uA.Firstname;
                    u.Lastname = "Nill";
                    u.registerdate = DateTime.Now;
                    u.username = uA.email;
                    u.active = 0;
                    
                    u.email = uA.email;
                    u.UserPassword = uA.UserPassword;
                    db.UserAccounts.Add(u);
                    db.SaveChanges();
                    int Id = u.UserId;
                    if (Id > 0)
                    {
                        var from = "from:Account Confirmation<email@anything.com>";
                        MailMessage mail = new MailMessage();
                        mail.To.Add(u.email);
                        mail.From = new MailAddress(from);
                        mail.Subject = "Account Confirmation";
                        var emailconv = Stringbyte__Convertion(u.email);
                        string Body = "<div width:960px; margin:0 auto; background-color:#CCC;><h3>Hi</h3>"
                                
                                + "<p align=\"justify\">Thank you for creating account with <i>Sheddy Demo</i></p>"

                               + "<p align=\"justify\">Kindly activate your account with the link below to enjoy the full service. </p>"

    + "<p><a href=\"http://localhost:18468/Home/loginverification/" + emailconv + "\" style=\"display:inline-block; font-weight:400; line-height:1.5; color:#212529;text-align:center;text-decoration:none;vertical-align:middle;cursor:pointer;-webkit-user-select:none;-moz-user-select:none;user-select:none;background-color:transparent;border:1px solid transparent;padding:.375rem .75rem;font-size:1rem;border-radius:.25rem;transition:color .15s ease-in-out,background-color .15s ease-in-out,border-color .15s ease-in-out,box-shadow .15s ease-in-out;color:#fff;background-color:#0d6efd;border-color:#0d6efd;\">Click here to verify your new account</a></p><p>If you experience any issues logging into your account, reach out to us at info@sheddy.com.</p><p>Regards,</p><p>THE SHEDDY TEAM.</p></div>";
                        mail.Body = Body;
                        mail.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "host";
                        smtp.Port = 8889;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential("", ""); // Enter seders User name and password  
                        smtp.EnableSsl = false;
                        smtp.Send(mail);
                        msg = "Verification Link has been sent to your email";

                    }
                    else
                    {
                        msg = "Application fail";
                    }



                }
            else
            {
                    Console.WriteLine("The Field not set");
            }
         
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message.ToString());
            }
            ViewBag.error = msg;
            return View(uA);
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(SignInModel si)
        {
            string msg = "";
            if (ModelState.IsValid)
            {

                var access = (from a in db.UserAccounts where a.email==si.email && a.UserPassword==si.UserPassword && a.active==1 select a).ToList();
                if(access.Count != 0)
                {
                    //
                    var from = "from:2FAOTP<noreply@minpip.com>";
                    MailMessage mail = new MailMessage();
                    mail.To.Add(si.email);
                    mail.From = new MailAddress(from);
                    mail.Subject = "Your confirmation Code";
                    var emailconv = GetUniqueKey(5);
                    string Body = "<div width:960px; margin:0 auto; background-color:#CCC;><h3>Verification needed</h3>"

                            + "<strong align=\"justify\">Please confirm your sign-in request</strong>"

                           + "<p align=\"justify\">We have detected an account sin-in request from a device</p>"
                           +"<p>To verify your account is safe, please use the following code to enable your new device- it will expire in 30 minute</p>"
+ "<div style=\"display:inline-block; font-weight:400; line-height:1.5; color:#212529;text-align:center;text-decoration:none;vertical-align:middle;cursor:pointer;-webkit-user-select:none;-moz-user-select:none;user-select:none;background-color:transparent;border:1px solid transparent;padding:.375rem .75rem;font-size:1rem;border-radius:.25rem;transition:color .15s ease-in-out,background-color .15s ease-in-out,border-color .15s ease-in-out,box-shadow .15s ease-in-out;color:#fff;background-color:#0d6efd;border-color:#0d6efd;\">"+emailconv+"</div><p>If you experience any issues logging into your account, reach out to us at info@sheddy.com.</p><p>Regards,</p><p>THE SHEDDY TEAM.</p></div>";
                    mail.Body = Body;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "host";
                    smtp.Port = 0;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential("username", "password"); // Enter seders User name and password  
                    smtp.EnableSsl = false;
                    smtp.Send(mail);
                   

                    Session["email"] = si.email;
                    return Redirect("loginverification");
                }
                else
                {
                    msg = "<h3>Invalid Username and Password</h3>";
                }
            }
            else
            {
                Console.WriteLine("Model State is Invalid");
            }
            ViewBag.error = msg;
            return View(si);
        }

        public ActionResult loginverification()
        {
            if(Session["email"] != null)
            {
                ViewBag.email = Session["email"];
                return View();
            }
            else
            {
                return Redirect("SignIn");
            }
            
        }

        public ActionResult Welcome()
        {

            return View();
        }

        public string testing()
        {
            var key = GetUniqueKey(5);
            return key;
        }

        public static string Stringbyte__Convertion(string Parameter)
        {
            byte[] b = Encoding.ASCII.GetBytes(Parameter);
            return Convert.ToBase64String(b);
        }

        public static string bytesString_Convertion(string Parameter)
        {
            byte[] s = Convert.FromBase64String(Parameter);
            return Encoding.ASCII.GetString(s);
        }
        //
        internal static readonly char[] chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray(); 

        public static string GetUniqueKey(int size)
        {            
            byte[] data = new byte[4*size];
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }

            return result.ToString();
        }

        //
    }

}
