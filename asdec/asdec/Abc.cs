using asdec.Errors;
using PygmentSharp.Core;
using PygmentSharp.Core.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asdec
{
    public class Abc
    {
        public const bool DEBUG = true;

        public DirectoryInfo d = null;
        public bool loaded = false;
        internal List<Token> tokens = new List<Token>();

        #region 
        public Abc() { }
        public Abc(string dir)
        {
            d = new DirectoryInfo(dir);
        }
        public void Load(string dir)
        {
            d = new DirectoryInfo(dir);
            Load();
        }

        public void Load()
        {
            if (DEBUG) Console.WriteLine("Loading: " + d.Name);
            Lex();
            loaded = true;
            if (DEBUG) Console.WriteLine("Loaded!");
        }

        public void Lex()
        {
            tokens = Pygmentize.File(d.FullName).WithLexer(new AVMLexer()).GetTokens().ToList();
            if (DEBUG) Console.WriteLine("Lexed: {0} tokens", tokens.Count);
        }
        #endregion

        public void Build()
        {
            if (!loaded)
            {
                if (d != null) Load();
                else throw new AbcException("Abc file not loaded!");
            }
        }
    }
}
