using asdec.Errors;
using PygmentSharp.Core.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static asdec.ABCUtil;

namespace asdec.ASModel.Nodes
{
    public class Code : Node
    {
        public Code(TokenStream tokenStream) : base(tokenStream)
        {
        }

        public override Node Select()
        {
            if (!Accept("code")) return null;//NB these not split like "refid". Maybe split?
            while (true)
            {
                if (Accept("end")) break;
                //todo accept label
                Token t = ts.getCur();
                OpcodeInfo? op = OpcodeByName(t.Value);
                if (!op.HasValue) throw new ASParsingException("Unknown opcode: " + t.Value);
                Expect(new Instruction(ts, op.Value));//reduntant double check
            }
            return this;
        }
    }
}
