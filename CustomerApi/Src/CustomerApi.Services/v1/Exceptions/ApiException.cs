using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CustomerApi.Services.v1.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException() : base() { }

        public ApiException(string message) : base(message) { }

        public ApiException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}