using IntegrationEvents.Models.Entities;

namespace IntegrationEvents.Models.Events
{
    public class NewStudentRegistrationEvent : DomainEvent
    {
        public NewStudentRegistrationEvent(Student student, TrainingCourse course)
        {
            Student = student;
            Course = course;
        }

        public Student Student { get; }
        public TrainingCourse Course { get; }
    }
}