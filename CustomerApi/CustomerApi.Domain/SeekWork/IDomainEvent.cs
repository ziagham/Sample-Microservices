using System;
using MediatR;

namespace CustomerApi.Domain.SeekWork
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}