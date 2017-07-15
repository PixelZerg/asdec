using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asdec.ASModel.Objects
{
    public class Label : Argument
    {
        public string Name;

        public Label(string name)
        {
            this.Name = name;
        }

        public static implicit operator String(Label l)
        {
            return l.Name;
        }

    }
}
