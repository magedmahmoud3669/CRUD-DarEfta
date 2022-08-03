using AutoMapper;
using DarEfta.BL.Helper;
using DarEfta.BL.Interface;
using DarEfta.BL.Models;
using DarEfta.DAL.Entity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DarEfta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        #region Fields

        private readonly IEmployeeRep employee;
        private readonly IMapper mapper;

        #endregion


        #region Ctor

        public EmployeeController(IEmployeeRep employee, IMapper mapper )

        {
            this.employee = employee;
            this.mapper = mapper;
        }

        #endregion

        //[HttpGet]
        //[Route("~/api/GetData")] // API
        //public string [] GetData()
        //{
        //    string[] data = new string[] { "Ahmed" , "Ali" , "Sara"};
        //    return data;
        //}


        //[HttpGet]
        //[Route("~/api/GetData2")] // API
        //public string[] GetData2()
        //{
        //    string[] data = new string[] { "fffff", "vvvvv", "aaaaa" };
        //    return data;
        //}



        [HttpGet]
        [Route("~/api/GetEmployees")] // API
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var data = await employee.GetAsync(x => x.IsDeleted == false && x.IsActive == true);
                var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return Ok(new ApiResponse<IEnumerable<EmployeeVM>>()
                {
                    Code = "200" ,
                    Status = "Ok" ,
                    Msg = "Data Retrieved Successfully ." ,
                    Data = result
                });

            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {
                    Code = "404",
                    Status = "Not Found",
                    Msg = "No Data Retrieved .",
                    Data = ex.Message
                });
            }
        }


        // Maged 23/7/2022
        [HttpGet]
        [Route("~/api/GetEmpById/{id}")]
        public async Task<IActionResult> GetEmpById(int id)
        {
            try
            {
                var data = await employee.GetByIdAsync(x => x.IsDeleted == false && x.IsActive == true && x.Id == id);
                var result = mapper.Map<EmployeeVM>(data);

                return Ok(new ApiResponse<EmployeeVM>()
                {
                    Code = "200",
                    Status = "Ok",
                    Msg = "Data Retrieved Successfully .",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {
                    Code = "404",
                    Status = "Not Found",
                    Msg = "No Data Retrieved .",
                    Data = ex.Message
                });
            }
        }

        
        [HttpPost]
        [Route("~/api/PostEmp")] // API
        public async Task<IActionResult> PostEmp(EmployeeVM obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = mapper.Map<Employee>(obj);
                    var data = await employee.CreateAsync(result);
                    return Ok(new ApiResponse<Employee>()
                    {
                        Code = "201",
                        Status = "Created",
                        Msg = "Data Created Successfully .",
                        Data = data
                    });
                }


                return BadRequest(new ApiResponse<string>()
                {
                    Code = "400",
                    Status = "Bad Request",
                    Msg = "Validation Error ."
                });


            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>()
                {
                    Code = "400",
                    Status = "Bad Request",
                    Msg = "Data Not Created .",
                    Data = ex.Message
                });
            }



        }


        //[DisableCors]
        //[EnableCors("https://localhost:7255")]
        [HttpPut]
        [Route("~/api/PutEmployee")] // API
        public async Task<IActionResult> PutEmployee(EmployeeVM obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = mapper.Map<Employee>(obj);
                    var data = await employee.UpdateAsync(result);
                    return Ok(new ApiResponse<Employee>()
                    {
                        Code = "200",
                        Status = "Updated",
                        Msg = "Data Updated Successfully .",
                        Data = data
                    });
                }


                return BadRequest(new ApiResponse<string>()
                {
                    Code = "400",
                    Status = "Bad Request",
                    Msg = "Validation Error ."
                });


            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>()
                {
                    Code = "400",
                    Status = "Bad Request",
                    Msg = "Data Not Created .",
                    Data = ex.Message
                });
            }



        }


        [HttpDelete]
        [Route("~/api/DeleteEmployee")] // API
        public async Task<IActionResult> DeleteEmployee(EmployeeVM obj)
        {
            try
            {
                    var result = mapper.Map<Employee>(obj);
                    await employee.DeleteAsync(result);
                    return Ok(new ApiResponse<Employee>()
                    {
                        Code = "200",
                        Status = "Deleted",
                        Msg = "Data Deleted Successfully ."
                    });

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>()
                {
                    Code = "400",
                    Status = "Bad Request",
                    Msg = "Data Not Deleted .",
                    Data = ex.Message
                });
            }



        }

    }
}

