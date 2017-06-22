using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace asdec.Errors
{
    public class ParsingException : Exception
    {
        public ParsingException()
        {
        }

        public ParsingException(string message) : base(message)
        {
        }

        public ParsingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ParsingException(TokenStream ts) : base(Utils.TokenStreamLoc(ts))
        {
        }
    }
}
