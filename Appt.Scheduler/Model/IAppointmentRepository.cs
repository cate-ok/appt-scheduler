using System.Collections.Generic;

namespace Appt.Scheduler
{
    public interface IAppointmentRepository
    {
        IEnumerable<Appointment> GetAppointments();
        Appointment GetAppointmentByID(int appointmentId);
        void InsertAppointment(Appointment appointment);
        void DeleteAppointment(int appointmentId);
        void UpdateAppointment(Appointment appointment);
        void Save();
    }
}