using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace asdec.Errors
{
    public class AbcException : Exception
    {
        public AbcException()
        {
        }

        public AbcException(string message) : base(message)
        {
        }

        public AbcException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AbcException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
