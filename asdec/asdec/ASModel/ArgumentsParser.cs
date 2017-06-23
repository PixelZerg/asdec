using asdec.ASModel.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static asdec.ABCUtil;

namespace asdec.ASModel
{
    public class ArgumentsParser
    {
        public OpcodeArgumentType[] types = null;
        public ArgumentsParser(OpcodeArgumentType[] types, string args)
        {
            this.types = types;
        }

        public List<Argument> Parse()
        {
            int i = 0;
            foreach (OpcodeArgumentType type in types)
            {
                switch (type)
                {
                    //todo
                }
                if (i < types.Length - 1) Expect(",");
                i++;
            }
        }
    }
}
