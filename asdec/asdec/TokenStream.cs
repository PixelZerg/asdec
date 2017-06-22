using PygmentSharp.Core.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asdec
{
    public class TokenStream
    {
        public List<Token> tokens = null;
        public int index { get; private set; } =0;
        
        public TokenStream(List<Token> tokens){
            this.tokens = tokens;
            //maybe impl remv whitespace here
        }
    }
}