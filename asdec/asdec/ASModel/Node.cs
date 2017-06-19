using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asdec.ASModel
{
    public abstract class Node
    {
        public string Name = string.Empty;
        public List<Node> Children = new List<Node>();
        
        public virtual void Process();
        
        public void SkipWhitespace(){
            //while true
            //  look next token
            //  if whitespace skip
            //  else if start with # handle preprocessor
            //  else return
        }
    }
}
