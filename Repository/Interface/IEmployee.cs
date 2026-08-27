using EmployeeDataExporter.DTOs;
namespace EmployeeDataExporter.Repository.Interface;
public interface IEmployeeGet
{
    public Task<IEnumerable<responseDto>> getEmployee();
    public Task<byte[]> getEmployeeExcelBytes();
}