using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace Appt.Scheduler.Controllers
{
    //[Route("[controller]")]
    //[Route("[controller]/{id}")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        public PersonController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [Route("person")]
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            List<Person> persons = new List<Person>();            
            string connectionString = Configuration["ConnectionString"];
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            conn.Open();

            NpgsqlCommand cmd = new NpgsqlCommand("select id, first_name, last_name, email, phone from person;", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            // Read all rows and output the first column in each row
            while (dr.Read())
            {
                persons.Add(new Person()
                {
                    Id = (short)dr[0],
                    FirstName = dr[1].ToString(),
                    LastName = (string)dr[2],
                    Phone = dr[3].ToString(),
                    Email = dr[4].ToString()
                });
            }
            conn.Close();
            return persons;
        }

        [Route("person/{personId}")]
        [HttpGet]
        public Person Get(int personId)
        {
            Person person = new Person();
            string connectionString = Configuration["ConnectionString"];
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            conn.Open();

            NpgsqlCommand cmd = new NpgsqlCommand($"select id, first_name, last_name, email, phone from person where id={personId};", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            // Read all rows and output the first column in each row
            while (dr.Read())
            {
                person.Id = (short)dr[0];
                person.FirstName = dr[1].ToString();
                person.LastName = (string)dr[2];
                person.Phone = dr[3].ToString();
                person.Email = dr[4].ToString();
            }
            conn.Close();
            return person;
        }
    }
}