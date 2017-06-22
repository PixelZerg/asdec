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
        
        public TokenStream ts = null;
        public int startIndex = 0;
        
        protected Node(TokenStream tokenStream)
        {
            this.ts = tokenStream;
            try
            {
                this.startIndex = this.ts.GetSave();
            }
            catch { }
        }
        
        protected virtual void Process(){
        }
        
        public abstract Node Select();
        
        public Node Get(Node n){
            SkipWhitespace();
            n.Process();
            SkipWhitespace();
            Node ret=null;
            try{
                ret= n.Select();//n select //dont add to childre
                ret.
            }catch{
            }
            SkipWhitespace();
            return ret;//or null
        }
        
        //getexpect
        
        public bool Accept(Node n){
            SkipWhitespace();
            n.Process();//try catch whatever
            SkipWhitespace();
            //n.select //add to children
            SkipWhitespace();
        }
        
        public bool Expect(Node n){
            if(!Accept(n)){
                //throw
            }
            return true;
        }
        
        public void SkipWhitespace(){
            //while true
            //  look next token
            //  if whitespace skip
            //  else if start with # handle preprocessor
            //  else return
        }
    }
}
