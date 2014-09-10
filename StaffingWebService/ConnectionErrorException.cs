using System;

namespace StaffingWebService
{
    class ConnectionErrorException : Exception
    {
        public ConnectionErrorException(string message) : base(message)
        {
        }
    }
}
