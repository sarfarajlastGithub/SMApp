using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SMApp.Web.LIB.Context;
using SMApp.Web.LIB.ViewModels;
using SMApp.Web.LIB.ViewModels.AccountVM;

namespace SMApp.Web.LIB.BL.Account
{
    public class SchoolProfile
    {

        public UpdateViewModel GetProfileInfo(string currentUserId)
        {
            var context = new AppDbContext();
            var model = context.Users.Include(u => u.SAddress).First(m => m.Id == currentUserId);

            UpdateViewModel vm = new UpdateViewModel();

            vm.RegistarDate = DateTimeConvert.GetString(model.RegistarDate);
            vm.AnnulDateOfExam = DateTimeConvert.GetString(model.AnnulDateOfExam);
            vm.Board = model.Board;
            vm.CpName = model.CpName;
            vm.CpPhone = model.CpPhone;
            vm.Email = model.Email;
            vm.EstablishedDate = DateTimeConvert.GetString(model.EstablishedDate);
            vm.Medium = model.Medium;
            vm.AddL1 = model.SAddress.AddL1;
            vm.AddL2 = model.SAddress.AddL2;
            vm.City = model.SAddress.City;
            vm.Pin = model.SAddress.Pin;
            vm.State = model.SAddress.State;
            vm.SchoolFType = model.SchoolFType;
            vm.SchoolGType = model.SchoolGType;
            vm.SchoolName = model.SchoolName;
            vm.SchoolPhoneNumber = model.SchoolPhoneNumber;
            vm.TotalStudent = model.TotalStudent;
            return vm;
        }
    }
}