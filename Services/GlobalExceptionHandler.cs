using System;

namespace Services
{
    public class GlobalExceptionHandler : Exception
    {
        public GlobalExceptionHandler(string message) : base(message) { }
        public GlobalExceptionHandler(string message, Exception inner) : base(message, inner) { }
    }
} 