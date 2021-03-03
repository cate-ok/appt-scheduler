using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Npgsql;
using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;

namespace Appt.Scheduler.Controllers
{
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        public AppointmentController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [Route("appointment")]
        [HttpGet]
        public IEnumerable<Appointment> Get()
        {
            List<Appointment> appointments = new List<Appointment>();
            string connectionString = Configuration["ConnectionString"];
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            conn.Open();

            var query = @"select a.id, a.date, a.time, t.name, a.person_id
                        from appointment a
                        inner
                        join appointment_type t
                        on a.type_id = t.id;";

            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            // Read all rows and output the first column in each row
            while (dr.Read())
            {
                appointments.Add(new Appointment()
                {
                    Id = (short)dr[0],
                    //Date = DateTime.ParseExact(dr[1].ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    AppointmentType = (short)dr[2],
                    PersonId = (short)dr[3]
                });
            }
            conn.Close();
            return appointments;
        }
    }
}
