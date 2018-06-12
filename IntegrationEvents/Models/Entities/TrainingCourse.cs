using IntegrationEvents.Models.Events;
using System.Collections.Generic;

namespace IntegrationEvents.Models.Entities
{
    public class TrainingCourse : BaseEntity
    {
        public string Name { get; set; }
        public List<Student> RegisteredStudents { get; set; } = new List<Student>();

        public void Register(Student student)
        {
            RegisteredStudents.Add(student);
            DomainEvents.Raise(new NewStudentRegistrationEvent(student, this));
        }
    }
}