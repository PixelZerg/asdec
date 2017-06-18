using asdec.Errors;
using PygmentSharp.Core;
using PygmentSharp.Core.Tokens;
using System;
using System.Collections.Generic;
//using System.IO;
using DirectoryInfo = System.IO.DirectoryInfo;//e.g on how java-like directives could be implemented
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asdec
{
    public class Abc
    {
        public const bool DEBUG = true;

        public DirectoryInfo rootDir = null;


        #region 
        public Abc() { }
        public Abc(string rootdir)
        {
            rootDir = new DirectoryInfo(rootdir);
        }
        #endregion

        public void Build()
        {

        }
    }
}
