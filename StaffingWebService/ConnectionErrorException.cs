using System;

namespace Cellenza.Service
{
    class ConnectionErrorException : Exception
    {
        public ConnectionErrorException(string message) : base(message)
        {
        }
    }
}
