using AppointmentManagement.Models;
using AppointmentManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentManagement.Controllers;

public class AppointmentsController : Controller
{
    private readonly IAppointmentService _service;

    public AppointmentsController(IAppointmentService service)
    {
        _service = service;
    }

    public IActionResult Index(AppointmentStatus? status)
    {
        ViewBag.SelectedStatus = status;
        return View(_service.GetAll(status));
    }

    [HttpGet]
    public IActionResult Create() => View(new Appointment());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("CustomerName,CustomerEmail,AppointmentDate,AppointmentTime,Notes,Status")] Appointment appointment)
    {
        CheckDoubleBooking(appointment, null);

        if (!ModelState.IsValid)
            return View(appointment);

        _service.Add(appointment);
        TempData["Success"] = "Appointment added successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var appointment = _service.GetById(id);
        if (appointment == null) return NotFound();
        return View(appointment);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("CustomerName,CustomerEmail,AppointmentDate,AppointmentTime,Notes,Status")] Appointment appointment)
    {
        appointment.Id = id;
        CheckDoubleBooking(appointment, id);

        if (!ModelState.IsValid)
            return View(appointment);

        if (!_service.Update(appointment)) return NotFound();

        TempData["Success"] = "Appointment updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        if (_service.Delete(id))
            TempData["Success"] = "Appointment deleted successfully.";
        return RedirectToAction(nameof(Index));
    }


    private void CheckDoubleBooking(Appointment appointment, int? excludeId)
    {
        if (appointment.Status == AppointmentStatus.Cancelled) return;
        if (appointment.AppointmentDate is not { } date || appointment.AppointmentTime is not { } time) return;

        if (_service.IsSlotTaken(date, time, excludeId))
        {
            ModelState.AddModelError(nameof(Appointment.AppointmentTime),
                "This date and time is already booked. Please choose another time.");
        }
    }
}
