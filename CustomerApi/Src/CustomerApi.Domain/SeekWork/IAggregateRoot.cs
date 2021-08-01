using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApi.Domain.SeekWork
{
    public interface IAggregateRoot
    {
        Guid Id { get; set;}
        DateTime CreatedUtc { get; }
    }
}
