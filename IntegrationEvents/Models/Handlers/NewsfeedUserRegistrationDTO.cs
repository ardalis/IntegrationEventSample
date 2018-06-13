using MediatR;

namespace IntegrationEvents.Models.Handlers
{
    public class NewsfeedUserRegistrationDTO : IRequest
    {
        public int TrainingCourseId { get; set; }
        public string UserDetails { get; set; }
        public string StudentName { get; set; }
        public string TrainingCourseName { get; set; }
    }
}