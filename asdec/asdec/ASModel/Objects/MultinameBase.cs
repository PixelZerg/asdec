using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asdec.ASModel.Objects
{
    public class MultinameBase : Argument
    {
        public ABCUtil.ASType Kind = ABCUtil.ASType.Undefined;
    }

    public class QName : MultinameBase
    {
        public Namespace Ns;
        public string Name;
    }

    public class RTQName : MultinameBase
    {
        public string Name;
    }

    public class Multiname : MultinameBase
    {
        public string Name;
        public List<Namespace> NsSet; 
    }

    public class MultinameL:MultinameBase
    {
        public List<Namespace> NsSet;
    }

    public class TypeName : MultinameBase
    {
        public MultinameBase Name;
        public List<MultinameBase> Params;
    }
}
