using AutoMapper;
using DarEfta.BL.Helper;
using DarEfta.BL.Interface;
using DarEfta.BL.Models;
using DarEfta.DAL.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;


namespace DarEfta.Portal.Controllers
{
    public class EmployeeController : Controller
    {



        #region Fields

        private readonly IEmployeeRep employee;
        private readonly IDepartmentRep department;
        private readonly IMapper mapper;
        private readonly ICityRep city;
        private readonly IDistrictRep district;

        #endregion


        #region Ctor

        public EmployeeController(IEmployeeRep employee, IDepartmentRep department, 
            IMapper mapper , ICityRep city , 
            IDistrictRep district)

        {
            this.employee = employee;
            this.department = department;
            this.mapper = mapper;
            this.city = city;
            this.district = district;
        }

        #endregion


        #region Actions

        public async Task<IActionResult> Index(string SearchValue = null)
        {
            if(SearchValue == null)
            {
                var data = await employee.GetAsync(x => x.IsDeleted == false && x.IsActive == true);
                var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return View(result);
            }
            else
            {
                var data = await employee.SearchAsync(x => x.IsDeleted == false && x.IsActive == true && x.Name.Contains(SearchValue));
                var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return View(result);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var data = await employee.GetByIdAsync(x => x.IsDeleted == false && x.IsActive == true && x.Id == id);
            var result = mapper.Map<EmployeeVM>(data);
            ViewBag.DepartmentList = new SelectList(await department.GetAsync(), "Id", "Name" , result.DepartmentId);
            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.DepartmentList = new SelectList(await department.GetAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVM obj)
        {
            try
            {

                ViewBag.DepartmentList = new SelectList(await department.GetAsync(), "Id", "Name");


                if (ModelState.IsValid)
                {


                    obj.ImageName = FileUploader.UploadFile("Imgs", obj.Image);
                    obj.CvName = FileUploader.UploadFile("Docs", obj.Cv);

                    var result = mapper.Map<Employee>(obj);
                    await employee.CreateAsync(result);
                    return RedirectToAction("Index");
                }

                TempData["Msg"] = "Validation Error";
                return View(obj);
            }
            catch (Exception ex)
            {
                TempData["Msg"] = ex.Message;
                return View(obj);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await employee.GetByIdAsync(x => x.IsDeleted == false && x.IsActive == true && x.Id == id);
            var result = mapper.Map<EmployeeVM>(data);
            ViewBag.DepartmentList = new SelectList(await department.GetAsync(), "Id", "Name", result.DepartmentId);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeVM obj)
        {
            try
            {

                ViewBag.DepartmentList = new SelectList(await department.GetAsync(), "Id", "Name", obj.DepartmentId);

                if (ModelState.IsValid)
                {
                    var result = mapper.Map<Employee>(obj);
                    await employee.UpdateAsync(result);
                    return RedirectToAction("Index");
                }

                TempData["Msg"] = "Validation Error";
                return View(obj);
            }
            catch (Exception ex)
            {
                TempData["Msg"] = ex.Message;
                return View(obj);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var data = await employee.GetByIdAsync(x => x.IsDeleted == false 
                                                        && x.IsActive == true 
                                                        && x.Id == id);
            var result = mapper.Map<EmployeeVM>(data);
            ViewBag.DepartmentList = new SelectList(await department.GetAsync(), "Id", "Name", result.DepartmentId);
            return View(result);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(EmployeeVM obj)
        {

            //ViewBag.DepartmentList = new SelectList(await department.GetAsync(), "Id", "Name", obj.DepartmentId);

            try
            {
                var result = mapper.Map<Employee>(obj);
                await employee.DeleteAsync(result);

                FileUploader.RemoveFile("Imgs", obj.ImageName);
                FileUploader.RemoveFile("Docs", obj.CvName);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Msg"] = ex.Message;
                return View(obj.Id);
            }
        }

        #endregion


        #region Ajax Call

        // Get Cities By Country Id ("CityRep - GetAsynch")

        [HttpPost]
        public async Task<IActionResult> GetCitiesByCountryId(int CntryId)
        {
            var data = await city.GetAsync(x => x.CountryId == CntryId);
            var result = mapper.Map<IEnumerable<CityVM>>(data);
            return Json(result);
        }


        // Get Districts By City ID ("DistrictRep - GetAsynch")
        [HttpPost]
        public async Task<JsonResult> GetDistrictsByCityId(int CtyId)
        {
            var data = await district.GetAsync(x => x.CityId == CtyId);
            var result = mapper.Map<IEnumerable<DistrictVM>>(data);
            return Json(result);
        }

        #endregion


    }
}
