using System;
using System.Collections.Generic;
using System.Text;

namespace OrderApi.Domain.SeekWork
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
        int Version { get; }
        DateTime CreatedUtc { get; }
    }
}
