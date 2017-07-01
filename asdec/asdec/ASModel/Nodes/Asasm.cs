using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asdec.ASModel.Nodes
{
    public class Asasm : Node
    {
        public Asasm(TokenStream ts) : base(ts)
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
