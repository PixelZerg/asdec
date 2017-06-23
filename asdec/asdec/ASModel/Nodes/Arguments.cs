using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asdec.ASModel.Nodes
{
    public class Arguments : Node
    {
        protected Arguments(TokenStream tokenStream) : base(tokenStream)
        {
        }

        public override Node Select()
        {
            throw new NotImplementedException();
        }
    }
}
