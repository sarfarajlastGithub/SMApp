using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using SMApp.Web.Controllers;
using Microsoft.AspNet.Identity;
using SMApp.Web.LIB.ViewModels.AccountVM;

namespace SMApp.Web.LIB.ViewModels
{
    public class DashBoardVM
    {
        public DashBoardVM()
        {
            _isComplete = false;
            var ob = new GetAppUserInfo();
            _isComplete = ob.CurrentAppUser.IsComplete;
            IsProfileComplete = _isComplete;
        }
        
        private readonly bool _isComplete;
        public bool IsProfileComplete
        {
            get { return _isComplete; }
            private set{}
        }

        public string StudentCount { get; set; }
        public string ColorClass { get; set; }
    }

}