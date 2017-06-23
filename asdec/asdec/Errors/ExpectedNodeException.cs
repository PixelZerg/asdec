using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asdec.Errors
{
    public class ExpectedNodeException : ParsingException
    {

        public ExpectedNodeException(TokenStream ts) : base(ts)
        {
        }

        public ExpectedNodeException(string message) : base(message)
        {
        }

        public ExpectedNodeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
