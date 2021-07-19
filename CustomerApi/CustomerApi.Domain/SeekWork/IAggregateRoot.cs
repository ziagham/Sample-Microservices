using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApi.Domain.SeekWork
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
        int Version { get; }
        DateTime CreatedUtc { get; }
    }
}
