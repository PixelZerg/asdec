using asdec.ASModel.Objects;
using PygmentSharp.Core.Tokens;
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
        public OpcodeInfo opcode;
        public List<Argument> args = null;

        public Instruction(TokenStream tokenStream, OpcodeInfo opcode) : base(tokenStream)
        {
            this.opcode = opcode;
        }

        public override Node Select()
        {
            //todo redo w/internal tokenstream
            if (!Accept(new TokenNode(ts, TokenTypes.Keyword, opcode.name))) return null;
            TokenNode raw = new TokenNode(ts, TokenTypes.Text);
            Expect(raw);
            args = new ArgumentsParser(this,opcode.argumentTypes, raw.getvalue()).Parse();
            return null;
        }
    }
}

//https://github.com/CyberShadow/RABCDAsm/blob/master/assembler.d#L1137

