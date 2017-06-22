using asdec.Errors;
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
        public Node parent = null;

        public Node lastAccepted = null;

        protected Node(TokenStream tokenStream)
        {
            this.ts = tokenStream;
            try
            {
                this.startIndex = this.ts.GetSave();
            }
            catch { }
        }

        public int OffAmount()
        {
            return this.ts.GetSave() - startIndex;
        }

        public Node mostParent()
        {
            Node cur = this;
            while (cur.parent != null)
            {
                cur = cur.parent;
            }
            return cur;
        }

        protected virtual void Process() {
        }

        public abstract Node Select();

        public Node Cr<T>(params object[] addArgs) where T : Node
        {
            List<object> args = new List<object>();
            args.Add(ts);
            if (addArgs.Length > 0)
            {
                args.AddRange(addArgs);
            }
            return (Node)Activator.CreateInstance(typeof(T), args.ToArray());
        }

        public Node Get<T>(params object[] a) where T : Node
        {
            return Get(Cr<T>(a));
        }

        public Node GetExpect<T>(params object[] a) where T : Node
        {
            return GetExpect(Cr<T>(a));
        }

        public Node Accept<T>(params object[] a) where T : Node
        {
            return Accept(Cr<T>(a));
        }

        public Node Get(Node n) {
            if (Accept(n, false)) return n;
            else return null;
        }

        public Node GetExpect(Node n)
        {
            if (Accept(n, false)) return n;
            else throw new ASParsingException(ts);
        }
        
        public bool Accept(Node n,bool addToChildren=true){
            try {
                Debug.Indent();
                SkipWhitespace();
                try
                {
                    n.Process();
                    SkipWhitespace();

                    n.parent = this;
                    Node seld = n.Select();
                    if (seld != null)
                    {
                        this.lastAccepted = seld;
                        if (Utils.DEBUG_PARSING)
                        {
                            Debug.WriteLine("[" + ts.index + "] " + this.Name + " node " + (addToChildren ? "accepted" : "got") + ": " + n.Name + " -- " + ts.look(-1).Value);
                        }
                        if (addToChildren) Children.Add(n);
                        // this.value += node.GetValue()+" ";
                        return true;
                    }
                }
                catch { throw; }
                n.ts.SetSave(n.startIndex);
                return false;
            }
            finally
            {
                SkipWhitespace();
                Debug.UnIndent();
            }
        }
        
        public bool Expect(Node n){
            if(!Accept(n)){
                throw new ASParsingException(ts);
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
