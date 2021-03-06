using System.Collections.Generic;

namespace Appt.Scheduler
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetPersons();
        Person GetPersonByID(int personId);
        void InsertPerson(Person person);
        void DeletePerson(int personId);
        void UpdatePerson(Person person);
        void Save();
    }
}