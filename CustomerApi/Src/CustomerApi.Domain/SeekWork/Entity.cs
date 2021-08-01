using System;
using System.Collections.Generic;

namespace CustomerApi.Domain.SeekWork
{
    public abstract class Entity: IAggregateRoot
    {
        public Guid Id { get; set; }
        public DateTime CreatedUtc { get; set; }

        public override string ToString()
        {
            return GetType().Name + " [Id=" + Id + "]";
        }
    }
}
