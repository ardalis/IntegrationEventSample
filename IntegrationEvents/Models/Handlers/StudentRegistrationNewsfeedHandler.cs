using IntegrationEvents.Models.Events;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace IntegrationEvents.Models.Handlers
{
    public class StudentRegistrationNewsfeedHandler : IHandle<NewStudentRegistrationEvent>
    {
        private readonly HttpContext _currentContext;
        private readonly ApplicationDbContext _db;

        public StudentRegistrationNewsfeedHandler(HttpContext currentContext,
            ApplicationDbContext db)
        {
            _currentContext = currentContext;
            _db = db;
        }

        public void Handle(NewStudentRegistrationEvent args)
        {
            var cookieString = _currentContext.Request.Cookies["UserCookie"].Value;
            //string user = cookieString.Split(':')[0];
            //DateTime dateLoggedIn = DateTime.Parse(cookieString.Split(':')[1]);

            // get or create newsfeed
            var newsfeed = _db.Newsfeeds
                .Include(n => n.Items)
                .FirstOrDefault(n => n.TrainingCourseId == args.Course.Id);
            if(newsfeed == null)
            {
                newsfeed = new Entities.Newsfeed() { TrainingCourseId = args.Course.Id };
                _db.SaveChanges();
            }

            // add item to newsfeed along with user info
            newsfeed.Items.Add(new Entities.NewsfeedItem()
            {
                Title = $"Student {args.Student.Name} registered for course {args.Course.Name}",
                UserInfo = cookieString
            });
            _db.SaveChanges();
        }
    }
}