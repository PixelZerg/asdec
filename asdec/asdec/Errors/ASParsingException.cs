using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace asdec.Errors
{
    public class ASParsingException : ParsingException
    {
        public ASParsingException()
        {
        }

        public ASParsingException(TokenStream ts) : base(ts)
        {
        }

        public ASParsingException(string message) : base(message)
        {
        }

        public ASParsingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
