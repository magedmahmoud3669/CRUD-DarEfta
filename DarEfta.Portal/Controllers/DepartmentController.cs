using AutoMapper;
using DarEfta.BL.Interface;
using DarEfta.BL.Models;
using DarEfta.BL.Repository;
using DarEfta.DAL.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRep department;
        private readonly IMapper mapper;

        public DepartmentController(IDepartmentRep department , IMapper mapper)
        {
            this.department = department;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var data = await department.GetAsync();
            var result = mapper.Map<IEnumerable<DepartmentVM>>(data);
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var data = await department.GetByIdAsync(a=>a.Id == id);
            var result = mapper.Map<DepartmentVM>(data);
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentVM obj)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = mapper.Map<Department>(obj);
                    await department.CreateAsync(result);
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
            var data = await department.GetByIdAsync(a => a.Id == id);
            var result = mapper.Map<DepartmentVM>(data);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentVM obj)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = mapper.Map<Department>(obj);
                    await department.UpdateAsync(result);
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
            var data = await department.GetByIdAsync(a => a.Id == id);
            var result = mapper.Map<DepartmentVM>(data);
            return View(result);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            try
            {
                await department.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Msg"] = ex.Message;
                return View(id);
            }
        }

    }
}
