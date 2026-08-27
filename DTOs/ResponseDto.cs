namespace EmployeeDataExporter.DTOs;

public partial class responseDto
{
    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string? Address { get; set; }

    public string? Contact { get; set; }

    public string? Department { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }
}