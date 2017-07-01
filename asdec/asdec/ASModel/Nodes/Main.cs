using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asdec.ASModel.Nodes
{
    public class Main : Asasm
    {
        public Dictionary<string, uint> NamespaceLabels = new Dictionary<string, uint>();

        public Main(TokenStream ts) : base(ts)
        {
        }

        public override void Process()
        {

        }

        public override Node Select()
        {
            throw new NotImplementedException();//TODO
        }
    }
}
