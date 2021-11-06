using System;

namespace Otus.Teaching.Pcf.GivingToCustomer.Core.Application.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(Guid entityId) : base($"Entity with id {entityId} not found")
        {
        }
        
        public EntityNotFoundException(string? message) : base(message)
        {
        }

        public EntityNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}