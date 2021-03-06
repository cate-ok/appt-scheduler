using System;
using System.Collections.Generic;
using Npgsql;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace Appt.Scheduler
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly IConfiguration Configuration;
        private readonly string ConnectionString;

        public AppointmentRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration["ConnectionString"];
        }        

        public IEnumerable<Appointment> GetAppointments()
        {
            List<Appointment> appointments = new List<Appointment>();
            using (NpgsqlConnection conn = new NpgsqlConnection(ConnectionString))
            {
                conn.Open();
                var query = @"select a.id, a.date, a.time, t.name, a.person_id
                        from appointment a
                        inner
                        join appointment_type t
                        on a.type_id = t.id;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Appointment appointment = new Appointment();
                            appointment.Id = (short)dr[0];

                            DateTime dt = DateTime.ParseExact(dr[1].ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                            TimeSpan tm = TimeSpan.Parse(dr[2].ToString());
                            appointment.Date = dt + tm;

                            appointment.AppointmentType = dr[3].ToString();
                            appointment.PersonId = (short)dr[4];
                            appointments.Add(appointment);
                        }
                        dr.Close();
                    }
                }
            }
            return appointments;
        }

        public Appointment GetAppointmentByID(int appointmentId)
        {
            Appointment appointment = new Appointment();
            using (NpgsqlConnection conn = new NpgsqlConnection(ConnectionString))
            {
                conn.Open();
                var query = @"select a.id, a.date, a.time, t.name, a.person_id
                        from appointment a
                        inner
                        join appointment_type t
                        on a.type_id = t.id
                        where a.id = @apptId;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("apptId", appointmentId);
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            appointment.Id = (short)dr[0];

                            DateTime dt = DateTime.ParseExact(dr[1].ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                            TimeSpan tm = TimeSpan.Parse(dr[2].ToString());
                            appointment.Date = dt + tm;

                            appointment.AppointmentType = dr[3].ToString();
                            appointment.PersonId = (short)dr[4];
                        }
                        dr.Close();
                    }
                }
            }
            return appointment;
        }

        public void InsertAppointment(Appointment appointment)
        {            
            using (NpgsqlConnection conn = new NpgsqlConnection(ConnectionString))
            {
                conn.Open();
                var query = @"INSERT INTO appointment (date, time, type_id, person_id) 
                              VALUES (@date, @time, @type, @personId);";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("date", appointment.Date.Year.ToString());
                    cmd.Parameters.AddWithValue("time", appointment.Date.ToString("hh:mm tt"));
                    cmd.Parameters.AddWithValue("type", appointment.AppointmentType);
                    cmd.Parameters.AddWithValue("personId", appointment.Person.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteAppointment(int appointmentId)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(ConnectionString))
            {
                conn.Open();
                var query = @"delete from appointment
                        where id = @apptId;";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("apptId", appointmentId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateAppointment(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }        
    }
}