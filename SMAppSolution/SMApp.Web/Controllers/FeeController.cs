using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using SMApp.Web.LIB.Context;
using SMApp.Web.LIB.Models.SchoolEN;
using SMApp.Web.LIB.Models.StudentFeeEN;
using SMApp.Web.LIB.ViewModels.AccountVM;
using SMApp.Web.LIB.ViewModels.StudentFeeVM;

namespace SMApp.Web.Controllers
{
    public class FeeController : Controller
    {
        // GET: Fee

        public ActionResult MainFeeCategory()
        {
            return View();
        }

        private readonly AppDbContext _context = new AppDbContext();
        private static readonly GetAppUserInfo _appUserInfo = new GetAppUserInfo();
        private readonly string _curid = _appUserInfo.CurrentUserId;
        #region MainFeeCategory actions

        [HttpPost]
        public JsonResult ListOfMainFeeCategory(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {

                var query = _context.FeeMainCategories.Where(f => f.SchoolId == _curid);

                if (string.IsNullOrEmpty(jtSorting) || jtSorting.Equals("Name ASC"))
                {
                    query = query.OrderBy(p => p.Name);
                }
                else if (jtSorting.Equals("Name DESC"))
                {
                    query = query.OrderByDescending(p => p.Name);
                }
                else
                {
                    query = query.OrderBy(p => p.Name); //Default!
                }

                int count = 0;
                List<FeeMainCategory> feeList =  count > 0
                           ? query.Skip(jtStartIndex).Take(count).ToList() //Paging
                           : query.ToList(); //No paging

                return Json(new { Result = "OK", Records = feeList, TotalRecordCount = query.Count() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateOfMainFeeCategory(FeeMainCategory feeMainCategory)
        {
            try
            {
                feeMainCategory.SchoolId = _curid;
                var addedPerson = _context.FeeMainCategories.Add(feeMainCategory);

                _context.SaveChanges();
                return Json(new { Result = "OK", Record = addedPerson });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateOfMainFeeCategory(FeeMainCategory feeMainCategory)
        {
            try
            {
                var updatedFeeCategory = _context.FeeMainCategories.First(f => f.Id == feeMainCategory.Id);
                updatedFeeCategory.Name = feeMainCategory.Name;
                _context.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteOfMainFeeCategory(int Id)
        {
            try
            {
                var fee = _context.FeeMainCategories.First(f => f.Id == Id);
                _context.FeeMainCategories.Remove(fee);
                _context.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        #endregion
    }


}