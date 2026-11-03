using AppointmentManagement.Models;

namespace AppointmentManagement.Services;

// In-memory storage using a List (data is lost when the app restarts).
public class AppointmentService : IAppointmentService
{
    private readonly List<Appointment> _appointments = new();
    private readonly object _lock = new();
    private int _nextId = 1;

    public List<Appointment> GetAll(AppointmentStatus? statusFilter = null)
    {
        lock (_lock)
        {
            return _appointments
                .Where(a => statusFilter == null || a.Status == statusFilter)
                .OrderBy(a => a.AppointmentDate)
                .ThenBy(a => a.AppointmentTime)
                .ToList();
        }
    }

    public Appointment? GetById(int id)
    {
        lock (_lock)
        {
            return _appointments.FirstOrDefault(a => a.Id == id);
        }
    }

    public void Add(Appointment appointment)
    {
        lock (_lock)
        {
            appointment.Id = _nextId++;
            _appointments.Add(appointment);
        }
    }

    public bool Update(Appointment appointment)
    {
        lock (_lock)
        {
            var existing = _appointments.FirstOrDefault(a => a.Id == appointment.Id);
            if (existing == null) return false;

            existing.CustomerName = appointment.CustomerName;
            existing.CustomerEmail = appointment.CustomerEmail;
            existing.AppointmentDate = appointment.AppointmentDate;
            existing.AppointmentTime = appointment.AppointmentTime;
            existing.Notes = appointment.Notes;
            existing.Status = appointment.Status;
            return true;
        }
    }

    public bool Delete(int id)
    {
        lock (_lock)
        {
            var existing = _appointments.FirstOrDefault(a => a.Id == id);
            return existing != null && _appointments.Remove(existing);
        }
    }

    // A slot is taken if another NON-cancelled appointment has the same date and time.
    public bool IsSlotTaken(DateOnly date, TimeOnly time, int? excludeId = null)
    {
        lock (_lock)
        {
            return _appointments.Any(a =>
                a.Status != AppointmentStatus.Cancelled &&
                a.AppointmentDate == date &&
                a.AppointmentTime == time &&
                a.Id != excludeId);
        }
    }
}
