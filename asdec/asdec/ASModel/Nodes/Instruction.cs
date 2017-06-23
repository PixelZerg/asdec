using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static asdec.ABCUtil;

namespace asdec.ASModel.Nodes
{
    public class Instruction : Node
    {

        protected Instruction(TokenStream tokenStream, OpcodeInfo opcode) : base(tokenStream)
        { 
        }

        public override Node Select()
        {
            Arguments//blahblah
        }
    }
}
