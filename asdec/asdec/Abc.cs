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
using asdec.ASModel;

namespace asdec
{
    public class Abc
    {
        public const bool DEBUG = true;

        public string rootPath = null;
        public Dir root = null;

        #region 
        public Abc() { }
        public Abc(string rootpath)
        {
            this.rootPath = rootpath;
        }
        #endregion

        public void Build()
        {
            root = new Dir();
        }

        private void BuildMain()
        {
            //find main
            //build it
        }
    }
}
