using System;
using System.Runtime.Serialization;

namespace CygSoft.CodeCat.DocumentManager.Exceptions
{
    public sealed class NonMatchingOrdinalReferenceException : ApplicationException, ISerializable
    {
        public NonMatchingOrdinalReferenceException()
        {
            // Add implementation.
        }
        public NonMatchingOrdinalReferenceException(string message)
            : base(message)
        {
            // Add implementation.
        }
        public NonMatchingOrdinalReferenceException(string message, Exception inner)
            : base(message, inner)
        {
            // Add implementation.
        }

        // This constructor is needed for serialization.
        public NonMatchingOrdinalReferenceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Add implementation.
        }
    }
}
