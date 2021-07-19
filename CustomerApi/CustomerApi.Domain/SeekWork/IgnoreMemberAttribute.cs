using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApi.Domain.SeekWork
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class IgnoreMemberAttribute : Attribute
    {
    }
}
