using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.DocumentManager.Infrastructure
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
