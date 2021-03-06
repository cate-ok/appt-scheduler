using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Appt.Scheduler.Controllers
{
    [ApiController]
    [Route("appointment")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository Repository;
        public AppointmentController(IAppointmentRepository repository)
        {
            Repository = repository;
        }

        [HttpGet]
        public IEnumerable<Appointment> Get()
        {
            var appointments = Repository.GetAppointments();
            return appointments;
        }

        [HttpGet("{id}")]
        public ActionResult<Appointment> Get(int id)
        {
            if (id == 0)
                return BadRequest("Value must be passed in the request body.");
            var appointment = Repository.GetAppointmentByID(id);
            return Ok(appointment);
        }

        [HttpPost]
        public void Post([FromBody] Appointment appt) => Repository.InsertAppointment(appt);

        //[HttpPut]
        //public Appointment Put([FromBody] Appointment appt) => Repository.UpdateAppointment(appt);

        //[HttpPatch("{id}")]
        //public StatusCodeResult Patch(int id, [FromBody] JsonPatchDocument<Appointment> patch)
        //{
        //    var appt = (Appointment)((OkObjectResult)Get(id).Result).Value;
        //    if (appt != null)
        //    {
        //        patch.ApplyTo(appt);
        //        return Ok();
        //    }
        //    return NotFound();
        //}

        [HttpDelete("{id}")]
        public void Delete(int id) => Repository.DeleteAppointment(id);
    }
}