using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asdec.Errors
{
    public class StopProcessException : Exception
    {
        public StopProcessException() : base()
        {
        }

        public StopProcessException(string message) : base(message)
        {
        }
    }
}
