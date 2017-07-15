using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asdec.ASModel.Objects
{
    public class Refid : Argument
    {
        public string NameRaw = null;

        private string[] _name = null;
        public string[] Name
        {
            get
            {
                if (_name == null)
                {
                    _name = NameRaw.Split(':');
                }
                return _name;
            }
            set
            {
                NameRaw = string.Join(":", value);
                _name = null;
            }
        }

        public Refid(string name)
        {
            this.NameRaw = name;
        }
    }
}
