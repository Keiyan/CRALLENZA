using System;

namespace Cellenza.Service
{
    public class ConnectionErrorException : Exception
    {
        public ConnectionErrorException(string message) : base(message)
        {
        }
    }
}
