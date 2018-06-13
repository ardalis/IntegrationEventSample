using IntegrationEvents.Models.Events;
using System.Web;
using MediatR;

namespace IntegrationEvents.Models.Handlers
{

    public class StudentRegistrationNewsfeedMediatrHandler : IHandle<NewStudentRegistrationEvent>
    {
        private readonly HttpContext _currentContext;
        private readonly IMediator _mediator;

        public StudentRegistrationNewsfeedMediatrHandler(HttpContext currentContext,
            IMediator mediator)
        {
            _currentContext = currentContext;
            _mediator = mediator;
        }

        /// <summary>
        /// This method is responsible for translating from the domain event (NewStudentRegistrationEvent) to an
        /// integration event which may be denormalized. 
        /// </summary>
        /// <param name="args"></param>
        public void Handle(NewStudentRegistrationEvent args)
        {
            var cookieString = _currentContext.Request.Cookies["UserCookie"].Value;

            // denormalize data into DTO for external consumption
            var message = new NewsfeedUserRegistrationDTO
            {
                StudentName = args.Student.Name,
                TrainingCourseId = args.Course.Id,
                TrainingCourseName = args.Course.Name,
                UserDetails = cookieString
            };

            _mediator.Send(message);
        }
    }
}