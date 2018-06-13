using IntegrationEvents.Models.Events;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MediatR;

namespace IntegrationEvents.Models.Handlers
{

    public class NewsfeedEventHandler : RequestHandler<NewsfeedUserRegistrationDTO>
    {
        private readonly ApplicationDbContext _db;

        public NewsfeedEventHandler(ApplicationDbContext db)
        {
            _db = db;
        }
        protected override void Handle(NewsfeedUserRegistrationDTO request)
        {
            var newsfeed = _db.Newsfeeds
                .Include(n => n.Items)
                .FirstOrDefault(n => n.TrainingCourseId == request.TrainingCourseId);
            if (newsfeed == null)
            {
                newsfeed = new Entities.Newsfeed() { TrainingCourseId = request.TrainingCourseId };
                _db.SaveChanges();
            }

            // add item to newsfeed along with user info
            newsfeed.Items.Add(new Entities.NewsfeedItem()
            {
                Title = $"Student {request.StudentName} registered for course {request.TrainingCourseName}",
                UserInfo = request.UserDetails + " (from MediatR)"
            });
            _db.SaveChanges();
        }
    }
}