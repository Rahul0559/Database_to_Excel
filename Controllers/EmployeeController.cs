using Microsoft.AspNetCore.Mvc;
using EmployeeDataExporter.Repository.Interface;
namespace EmployeeDataExporter.Controllers;

[ApiController]
[Route("api/employees")]

public class EmployeeControllers : ControllerBase
{
    private readonly IEmployeeGet _iemployeeGet;
    public EmployeeControllers(IEmployeeGet iemployeeGet)
    {
        _iemployeeGet = iemployeeGet;
    }
    [HttpGet("excel")]
    public async Task<IActionResult> getEmployeeExcel()
    {
        try
        {
            var result = await _iemployeeGet.getEmployeeExcelBytes();
            if(result.Length < 0 || result == null)
            {
                return StatusCode(500, "Failed to generate Excel file.");
            }
            return File(
                result,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Employees.xlsx"
            );
        }  
        catch(Exception ex)
        {
            return StatusCode(500,$"Internal server Error: {ex.Message}");
        } 
    }
    [HttpGet]
    public async Task<IActionResult> getEmployee()
    {
        try
        {
            var result = await _iemployeeGet.getEmployee();
            if(!result.Any() || result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }  
        catch(Exception ex)
        {
            return StatusCode(500,$"Internal server Error: {ex.Message}");
        } 
    }
}