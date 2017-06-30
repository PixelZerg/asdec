using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using asdec;

namespace asdec.ASModel.Objects
{
    public class Namespace
    {
        public ABCUtil.ASType Kind;
        public string Name;
        public uint Id;

        public Namespace(ABCUtil.ASType kind, string name, uint id)
        {
            this.Kind = kind;
            this.Name = name;
            this.Id = id;
        }
    }
}
