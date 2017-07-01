using asdec.ASModel.Nodes;
using asdec.Errors;
using PygmentSharp.Core.Tokens;
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

        public Asasm mostParent()
        {
            Node cur = this;
            while (cur.parent != null)
            {
                cur = cur.parent;
            }
            return (Asasm)cur;
        }

        public virtual void Process() {
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

        public bool Accept<T>(params object[] a) where T : Node
        {
            return Accept(Cr<T>(a));
        }

        public bool Expect<T>(params object[] a) where T : Node
        {
            return Expect(Cr<T>(a));
        }

        public Node Get(Node n) {
            if (Accept(n, false)) return n;
            else return null;
        }

        public Node GetExpect(Node n)
        {
            if (Accept(n, false)) return n;
            else throw new ExpectedNodeException(ts);
        }

        public bool Accept(string str, TokenNode.VerificationMode v = TokenNode.VerificationMode.Is)
        {
            return Accept(new TokenNode(ts, "", str, v));
        }

        public bool Expect(string str, TokenNode.VerificationMode v = TokenNode.VerificationMode.Is)
        {
            return Expect(new TokenNode(ts, "", str, v));
        }

        public bool Accept(Node n,bool addToChildren=true){
            try
            {
                Debug.Indent();
                SkipWhitespace();
                try
                {
                    try
                    {
                        n.Process();
                    }
                    catch
                    {
                        return false;
                    }
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
                throw new ExpectedNodeException(ts);
            }
            return true;
        }

        public void SkipWhitespace()
        {
            if (this.parent == null) { if (!(this is Asasm)) throw new ASParsingException("Parent null?!"); }
            //while true
            //  look next token
            //  if whitespace skip
            //  else if start with # handle preprocessor
            //  else return
            while (true)
            {
                Token t = this.ts.getCur();
                if (String.IsNullOrWhiteSpace(t.Value) || t.isType("Comment") || t.isType("Whitespace"))
                {
                    ts.increment();
                }
                else if (t.isType("Literal"))
                {
                    //handle compiler directive
                    Debug.WriteLine("compiler directive");
                }
                else
                {
                    return;
                }
            }
        }
    }
}
