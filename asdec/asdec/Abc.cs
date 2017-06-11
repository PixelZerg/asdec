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

        public FileInfo f = null;
        public bool loaded = false;
        internal List<Token> tokens = new List<Token>();

        #region 
        public Abc() { }
        public Abc(string file)
        {
            f = new FileInfo(file);
        }
        public void Load(string file)
        {
            f = new FileInfo(file);
            Load();
        }

        public void Load()
        {
            if (DEBUG) Console.WriteLine("Loading: " + f.Name);
            Lex();
            loaded = true;
            if (DEBUG) Console.WriteLine("Loaded!");
        }

        public void Lex()
        {
            tokens = Pygmentize.File(f.FullName).WithLexer(new AVMLexer()).GetTokens().ToList();
            if (DEBUG) Console.WriteLine("Lexed: {0} tokens", tokens.Count);
        }
        #endregion

        public void Build()
        {
            if (!loaded)
            {
                if (f != null) Load();
                else throw new AbcException("Abc file not loaded!");
            }
        }
    }
}
