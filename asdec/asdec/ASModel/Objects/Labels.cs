using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asdec.ASModel.Objects
{
    public class Labels : Argument
    {
        public List<string> Names;

        public Labels(params string[] names)
        {
            this.Names = names.ToList();
        }

        public Labels(List<string> names)
        {
            this.Names = names;
        }

        public static implicit operator List<String>(Labels l)
        {
            return l.Names;
        }

    }
}
