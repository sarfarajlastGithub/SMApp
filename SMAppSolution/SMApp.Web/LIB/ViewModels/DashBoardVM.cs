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
        
        private bool _isComplete;
        public bool IsProfileComplete
        {
            get { return _isComplete; }
            private set
            {
                _isComplete = false;
                var ob = new GetAppUserInfo();
                _isComplete = ob.CurrentAppUser.IsComplete;
            }
        }
    }

}