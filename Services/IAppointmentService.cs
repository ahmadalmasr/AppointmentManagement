using AppointmentManagement.Models;

namespace AppointmentManagement.Services;

public interface IAppointmentService
{
    List<Appointment> GetAll(AppointmentStatus? statusFilter = null);
    Appointment? GetById(int id);
    void Add(Appointment appointment);
    bool Update(Appointment appointment);
    bool Delete(int id);
    bool IsSlotTaken(DateOnly date, TimeOnly time, int? excludeId = null);
}
