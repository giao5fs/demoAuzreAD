using System.ComponentModel.DataAnnotations;

namespace adminApp.Models;
public class Timesheet
{
    public int TimesheetId { get; set; }

    [Required(ErrorMessage = "Employee name is required")]
    public string EmployeeName { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    [Required]
    [Range(0.5, 24, ErrorMessage = "Hours worked must be between 0.5 and 24")]
    public decimal HoursWorked { get; set; }

    [StringLength(500, ErrorMessage = "Task description should not exceed 500 characters")]
    public string TaskDescription { get; set; }
}

