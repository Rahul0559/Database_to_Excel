using EmployeeDataExporter.Repository.Interface;
using EmployeeDataExporter.DTOs;
using EmployeeDataExporter.Data;   
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;

namespace EmployeeDataExporter.Repository;

public class EmployeeGet : IEmployeeGet
{
    private readonly AppDbContext _dbcontext;
    public EmployeeGet(AppDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }
    public async Task<IEnumerable<responseDto>> getEmployee()
    {
        var result = _dbcontext.Employees.Select(
            e => new responseDto
            {
                EmployeeId = e.EmployeeId,
                EmployeeName = e.EmployeeName,
                Address = e.Address,
                Contact= e.Contact,
                Department = e.Department,
                IsActive = e.IsActive,
                IsDeleted = e.IsDeleted
            }
        );
        return result;
    }
    public async Task<byte[]> getEmployeeExcelBytes()
    {
        var employees = await _dbcontext.Employees.ToListAsync();

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Employees");

            // Define Excel Headers
            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "Name";
            worksheet.Cell(1, 3).Value = "Address";
            worksheet.Cell(1, 4).Value = "Contact";
            worksheet.Cell(1, 5).Value = "Department";
            worksheet.Cell(1, 6).Value = "IsActive";
            worksheet.Cell(1, 7).Value = "IsDeleted";
            worksheet.Row(1).Style.Font.Bold = true;

            // Populate rows from list variables
            int rowNum = 2;
            foreach (var e in employees)
            {
                worksheet.Cell(rowNum, 1).Value = e.EmployeeId;
                worksheet.Cell(rowNum, 2).Value = e.EmployeeName;
                worksheet.Cell(rowNum, 3).Value = e.Address;
                worksheet.Cell(rowNum, 4).Value = e.Contact;
                worksheet.Cell(rowNum, 5).Value = e.Department;
                worksheet.Cell(rowNum, 6).Value = e.IsActive;
                worksheet.Cell(rowNum, 7).Value = e.IsDeleted;
                rowNum++;
            }

            worksheet.Columns().AdjustToContents();

            using var memoryStream = new MemoryStream();
                workbook.SaveAs(memoryStream);
                return memoryStream.ToArray(); 
        }
    }
}
