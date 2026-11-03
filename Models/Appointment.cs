using System.ComponentModel.DataAnnotations;

namespace AppointmentManagement.Models;

public class Appointment
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Customer name is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Customer name must be at least 3 characters.")]
    [Display(Name = "Customer Name")]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Customer email is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    [Display(Name = "Customer Email")]
    public string CustomerEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Appointment date is required.")]
    [DataType(DataType.Date)]
    [Display(Name = "Date")]
    public DateOnly? AppointmentDate { get; set; }

    [Required(ErrorMessage = "Appointment time is required.")]
    [DataType(DataType.Time)]
    [Display(Name = "Time")]
    public TimeOnly? AppointmentTime { get; set; }

    [StringLength(500)]
    public string? Notes { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    [EnumDataType(typeof(AppointmentStatus), ErrorMessage = "Invalid status.")]
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;
}
