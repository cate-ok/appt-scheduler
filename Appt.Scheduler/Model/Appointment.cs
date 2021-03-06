using System;

namespace Appt.Scheduler
{
    public class Appointment
    {
        public short Id { get; set; }
        public short PersonId { get; set; }
        public Person Person { get; set; }
        public DateTime Date { get; set; }
        public string AppointmentType { get; set; }
    }
}
