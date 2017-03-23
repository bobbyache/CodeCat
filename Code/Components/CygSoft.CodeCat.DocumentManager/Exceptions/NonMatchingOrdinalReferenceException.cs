using System;
using System.Runtime.Serialization;

namespace CygSoft.CodeCat.DocumentManager.Exceptions
{
    public sealed class NonMatchingOrdinalReferenceException : ApplicationException, ISerializable
    {
        public NonMatchingOrdinalReferenceException()
        {
        }
        public NonMatchingOrdinalReferenceException(string message)
            : base(message)
        {
        }
        public NonMatchingOrdinalReferenceException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public NonMatchingOrdinalReferenceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
