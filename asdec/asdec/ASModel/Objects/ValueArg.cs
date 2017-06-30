using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asdec.ASModel.Objects
{
    public class ValueArg : Argument
    {
        public SByte bytev;
        public Byte ubytev;
        public Int32 intv;
        public UInt32 uintv;
        public Double doublev;
        public String stringv;

        public ValueArg() { }

        public ValueArg(object val)
        {

            if (val.GetType() == typeof(SByte))
            {
                bytev = (SByte)val;
                return;
            }
            if (val.GetType() == typeof(Byte))
            {
                ubytev = (Byte)val;
                return;
            }
            if (val.GetType() == typeof(Int32))
            {
                intv = (Int32)val;
                return;
            }
            if (val.GetType() == typeof(UInt32))
            {
                uintv = (UInt32)val;
                return;
            }
            if (val.GetType() == typeof(Double))
            {
                doublev = (Double)val;
                return;
            }
            if (val.GetType() == typeof(String))
            {
                stringv = (String)val;
                return;
            }
        }
    }
}
