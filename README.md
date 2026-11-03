# Appointment Management System

نظام بسيط لإدارة المواعيد مبني بـ **ASP.NET Core MVC (.NET 10)**.

## الفكرة
- إضافة، عرض، تعديل، وحذف المواعيد (الحذف مع رسالة تأكيد).
- التحقق من البيانات باستخدام Data Annotations.
- فلترة المواعيد حسب الحالة (Scheduled / Completed / Cancelled).
- ترتيب المواعيد من الأقدم للأحدث حسب التاريخ ثم الوقت.
- **منع الحجز المزدوج**: لا يمكن حجز موعدين فعّالين في نفس التاريخ والوقت.
  - المواعيد بحالة `Cancelled` لا تمنع حجز نفس الوقت.
  - عند التعديل، الموعد لا يتعارض مع نفسه.
  - الشرط مطبّق عند الإضافة والتعديل.
- التخزين في `List<Appointment>` داخل الذاكرة (تُفقد البيانات عند إعادة تشغيل التطبيق).

## هيكل المشروع
```
Controllers/   AppointmentsController.cs
Models/        Appointment.cs, AppointmentStatus.cs
Services/      IAppointmentService.cs, AppointmentService.cs
Views/         Appointments/ (Index, Create, Edit, _Form), Shared/_Layout
Program.cs
```

## طريقة التشغيل
المتطلبات: [.NET 10 SDK](https://dotnet.microsoft.com/download)

```bash
git clone <repository-url>
cd AppointmentManagement
dotnet run
```
ثم افتح الرابط الظاهر في الـ Terminal (مثل `https://localhost:5001`).
