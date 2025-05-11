using System;

namespace Services
{
    public class EquipmentServiceException : Exception
    {
        public EquipmentServiceException(string message) : base(message) { }
        public EquipmentServiceException(string message, Exception inner) : base(message, inner) { }
    }
} 