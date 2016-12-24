using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SMApp.Web.LIB.Context;
using SMApp.Web.LIB.Models.SchoolEN;
using SMApp.Web.LIB.ViewModels;
using SMApp.Web.LIB.ViewModels.AccountVM;
using SMApp.Web.LIB.BL.Account;
using SMApp.Web.LIB.ViewModels.Enums;

namespace SMApp.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }



        //Update tenure
        [HttpPost]
        public PartialViewResult TenureViewAndUpdate(ClassViewAndUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_ViewTenurePartial", model);
            }
            var crid = User.Identity.GetUserId();
            //ClassViewAndUpdateModel vm = new ClassViewAndUpdateModel();
            //vm = GetTenureData();
            var context = new AppDbContext();

            if (context.TenureTimes.Any(t => t.SchoolProfileId == crid && t.TenureYearName == model.TenureYearName))
            {
                var tanureTime =
                    context.TenureTimes.First(t => t.SchoolProfileId == crid && t.TenureYearName == model.TenureYearName);
                tanureTime.TenureStartDate = DateTimeConvert.GetDate(model.StartDate);
                tanureTime.TenureEndDate = DateTimeConvert.GetDate(model.EndDate);
                context.SaveChanges();
            }
            else
            {
                TenureTime tn = new TenureTime
                {
                    SchoolProfileId = crid,
                    TenureYearName = model.TenureYearName,
                    TenureStartDate = DateTimeConvert.GetDate(model.StartDate),
                    TenureEndDate = DateTimeConvert.GetDate(model.EndDate)
                };
                context.TenureTimes.Add(tn);
                context.SaveChanges();
            }

            Thread.Sleep(2000);

            return PartialView("_ViewTenurePartial", GetClassSectionData());
        }


        public ActionResult ClassViewAndUpdate()
        {

            return View(GetClassSectionData());
        }


        [HttpPost]
        public ActionResult ClassViewAndUpdate(FormCollection form)
        {

            Dictionary<int, string> sc = new Dictionary<int, string>();
            if (Request.Form["A"] == null)
            {
                sc.Add(1, "off");
            }
            else
            {
                sc.Add(1, "on");
            }
            if (Request.Form["B"] == null)
            {
                sc.Add(2, "off");
            }
            else
            {
                sc.Add(2, "on");
            }
            if (Request.Form["C"] == null)
            {
                sc.Add(3, "off");
            }
            else
            {
                sc.Add(3, "on");
            }
            if (Request.Form["D"] == null)
            {
                sc.Add(4, "off");
            }
            else
            {
                sc.Add(4, "on");
            }
            if (Request.Form["E"] == null)
            {
                sc.Add(5, "off");
            }
            else
            {
                sc.Add(5, "on");
            }
            if (Request.Form["F"] == null)
            {
                sc.Add(6, "off");
            }
            else
            {
                sc.Add(6, "on");
            }
            ClassViewAndUpdateModel md = GetClassSectionData();
            string className = Request.Form["ClassName"];
            if (string.IsNullOrEmpty(className))
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_AddClassPartial", md);
                }

                return View(md);

            }

            int A = (Request.Form["A"] == null) ? 0 : 1;
            int B = (Request.Form["B"] == null) ? 0 : 2;
            int C = (Request.Form["C"] == null) ? 0 : 3;
            int D = (Request.Form["D"] == null) ? 0 : 4;
            int E = (Request.Form["E"] == null) ? 0 : 5;
            int F = (Request.Form["F"] == null) ? 0 : 6;


            var clsName = EnumUtil.ParseEnum<SClass>(className);
            var crid = User.Identity.GetUserId();


            var context = new AppDbContext();


            //Fro each Section There will be One Insert to ClassAndSection table
            foreach (var item in sc)
            {
                var itemEnum = EnumUtil.ParseEnum<SSectionEnum>(item.Key.ToString());
                var issExistClasSec = context.ClassAndSection.Any(c => c.SchoolProfileId == crid && c.SClass == clsName && c.SSection == itemEnum);

                if (item.Value == "on")
                {

                    if (!issExistClasSec)
                    {
                        ClassAndSection sec = new ClassAndSection()
                        {
                            SchoolProfileId = crid,
                            SClass = clsName,
                            SSection = itemEnum
                        };
                        context.ClassAndSection.Add(sec);
                        context.SaveChanges();

                    }
                    //}
                }

                if (item.Value == "off" && issExistClasSec)
                {
                    var existClasSec =
                        context.ClassAndSection.First(
                            c => c.SchoolProfileId == crid && c.SClass == clsName && c.SSection == itemEnum);
                    context.ClassAndSection.Remove(existClasSec);
                    context.SaveChanges();
                }
            }
            md = GetClassSectionData();

            Thread.Sleep(2000);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_AddClassPartial", md);
            }

            return View(md);
        }

        /// <summary>
        /// Tis is for Getting View Model of Class And Section form
        /// </summary>
        /// <returns></returns>
        internal ClassViewAndUpdateModel GetClassSectionData()
        {
            var crUser = User.Identity.GetUserId();
            var context = new AppDbContext();
            var clsSes = context.ClassAndSection.Where(c => c.SchoolProfileId == crUser).ToList();

            //Get All Distinct Class of School
            var listClass = clsSes.Select(m => m.SClass).Distinct().ToList();


            if (listClass.Count > 3 && listClass.Count < 5)
            {
                var sp = context.Users.First(p => p.Id == crUser);
                sp.IsComplete = true;
                context.SaveChanges();
            }

            //Creating object of ViewModel
            ClassViewAndUpdateModel vm = new ClassViewAndUpdateModel();

            List<SClassVM> cVMList = new List<SClassVM>();
            List<SSectionVM> sList = new List<SSectionVM>();
            //Iterating through all Class
            foreach (var scls in listClass)
            {
                SClassVM cVM = new SClassVM();
                //Find List of distinct Section list
                List<SSectionVM> result = (from p in context.ClassAndSection
                                           where p.SClass == scls && p.SchoolProfileId == crUser
                                           select new SSectionVM()
                                           {
                                               Name = p.SSection,
                                           }).Distinct().ToList();

                //Adding Class Name
                cVM.Name = scls;
                //Adding Section list to that class
                cVM.ListOfSection = result;

                //The whole class object
                //Adding to ViewModel
                //So that we can iterate

                cVMList.Add(cVM);
            }
            cVMList = cVMList.OrderBy(l => l.Name).ToList();
            vm.SClassList = cVMList;

            #region TenureRegion
            var tenureTimes = context.TenureTimes.Where(c => c.SchoolProfileId == crUser).ToList();

            //Get All Distinct Class of School
            //var listClass = tenureTimes.Select(m => m.SClass).Distinct().ToList();


            if (tenureTimes.Count > 1 && tenureTimes.Count < 10)
            {
                var sp = context.Users.First(p => p.Id == crUser);
                sp.IsComplete = true;
                context.SaveChanges();
            }


            vm.TenureYearList = tenureTimes.Select(t => new TenureTimeVM
            {
                TenureYearName = t.TenureYearName,
                TenureStartDate = DateTimeConvert.GetString(t.TenureStartDate),
                TenureEndDate = DateTimeConvert.GetString(t.TenureEndDate)
            });

            #endregion



            return vm;
        }


        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes.
            // If a user enters incorrect codes for a specified amount of time then the user account
            // will be locked out for a specified amount of time.
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        public ActionResult AppUserView()
        {
            SchoolProfile ob = new SchoolProfile();
            var currentUserId = User.Identity.GetUserId();
            UpdateViewModel vm = ob.GetProfileInfo(currentUserId);

            return View(vm);
        }
        public ActionResult UpdateAppUser()
        {
            SchoolProfile ob = new SchoolProfile();
            var currentUserId = User.Identity.GetUserId();
            UpdateViewModel vm = ob.GetProfileInfo(currentUserId);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAppUser(UpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please insert Required Field.");
                return View(model);
            }

            var currentUserId = User.Identity.GetUserId();
            var context = new AppDbContext();
            var vm = context.Users.Include(u => u.SAddress).First(m => m.Id == currentUserId);

            vm.RegistarDate = DateTimeConvert.GetDate(model.RegistarDate);
            vm.AnnulDateOfExam = DateTimeConvert.GetDate(model.AnnulDateOfExam);
            vm.Board = model.Board;
            vm.CpName = model.CpName;
            vm.CpPhone = model.CpPhone;
            vm.Email = model.Email;
            vm.EstablishedDate = DateTimeConvert.GetDate(model.EstablishedDate);
            vm.Medium = model.Medium;
            vm.SAddress = new SAddress
            {
                AddL1 = model.AddL1,
                AddL2 = model.AddL2,
                City = model.City,
                Pin = model.Pin,
                State = model.State
            };

            vm.SchoolFType = model.SchoolFType;
            vm.SchoolGType = model.SchoolGType;
            vm.SchoolName = model.SchoolName;
            vm.SchoolPhoneNumber = model.SchoolPhoneNumber;
            vm.TotalStudent = model.TotalStudent;
            context.SaveChanges();

            return RedirectToLocal("AppUserView");
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    SchoolName = model.SchoolName,
                    TotalStudent = model.TotalStudent,
                    Board = model.Board,
                    Medium = model.Medium,
                    CpName = model.CpName,
                    CpPhone = model.CpPhone,
                    RegistarDate = DateTime.Now,
                    SAddress = new SAddress
                    {
                        City = model.City,
                        State = model.State
                    }
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}