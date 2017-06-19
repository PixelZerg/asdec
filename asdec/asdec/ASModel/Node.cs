using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asdec.ASModel
{
    public class Node
    {
        public string Name = string.Empty;
        public List<Node> Children = new List<Node>();

        public virtual Node Process()
        {
            //add # lookup here
        }
    }
}
