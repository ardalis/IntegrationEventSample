using System.Collections.Generic;

namespace IntegrationEvents.Models.Entities
{
    public class Newsfeed : BaseEntity
    {
        public int TrainingCourseId { get; set; }
        public List<NewsfeedItem> Items { get; set; } = new List<NewsfeedItem>();
    }
}