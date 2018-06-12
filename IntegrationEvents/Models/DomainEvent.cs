using System;

namespace IntegrationEvents.Models
{
    public abstract class DomainEvent
    {
        public DateTime DateTimeEventOccurred { get; protected set; } = DateTime.Now;
    }
}