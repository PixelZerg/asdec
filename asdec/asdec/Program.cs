using asdec.ASModel;
using PygmentSharp.Core;
using PygmentSharp.Core.Formatting;
using PygmentSharp.Core.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace asdec
{
    internal static class Program
    {
        [STAThread]
        public static void Main()
        {
            //var lexed = Pygmentize.File(@"input.asasm").WithLexer(new AVMLexer());

            //if (File.Exists("output.html")) File.Delete("output.html");

            //foreach (Token t in lexed.GetTokens().ToList())
            //{
            //    Console.WriteLine(t.ToString());
            //}
            //lexed.WithFormatter(new HtmlFormatter(new HtmlFormatterOptions()
            //{
            //})).ToFile("output.html");
            //File.WriteAllText("output.html", File.ReadAllText("output.html").Insert(0,
            //    "<style>html{background-color:#272822}.c{color:#75715e}.err{color:#960050;background-color:#1e0010}.k{color:#66d9ef}.l{color:#ae81ff}.n{color:#f8f8f2}.o{color:#f92672}.p{color:#f8f8f2}.cm{color:#75715e}.cp{color:#75715e}.c1{color:#75715e}.cs{color:#75715e}.ge{font-style:italic}.gs{font-weight:700}.kc{color:#66d9ef}.kd{color:#66d9ef}.kn{color:#f92672}.kp{color:#66d9ef}.kr{color:#66d9ef}.kt{color:#66d9ef}.ld{color:#e6db74}.m{color:#ae81ff}.s{color:#e6db74}.na{color:#a6e22e}.nb{color:#f8f8f2}.nc{color:#a6e22e}.no{color:#66d9ef}.nd{color:#a6e22e}.ni{color:#f8f8f2}.ne{color:#a6e22e}.nf{color:#a6e22e}.nl{color:#f8f8f2}.nn{color:#f8f8f2}.nx{color:#a6e22e}.py{color:#f8f8f2}.nt{color:#f92672}.nv{color:#f8f8f2}.ow{color:#f92672}.w{color:#f8f8f2}.mf{color:#ae81ff}.mh{color:#ae81ff}.mi{color:#ae81ff}.mo{color:#ae81ff}.sb{color:#e6db74}.sc{color:#e6db74}.sd{color:#e6db74}.s2{color:#e6db74}.se{color:#ae81ff}.sh{color:#e6db74}.si{color:#e6db74}.sx{color:#e6db74}.sr{color:#e6db74}.s1{color:#e6db74}.ss{color:#e6db74}.bp{color:#f8f8f2}.vc{color:#f8f8f2}.vg{color:#f8f8f2}.vi{color:#f8f8f2}.il{color:#ae81ff}.gu{color:#75715e}.gd{color:#f92672}.gi{color:#a6e22e}</style>"
            //    ));
            //Process.Start("output.html");

            //new Abc("../../agc/AssembleeGameClient1495643123-0.main.asasm").Build();333

            //new Abc("../../agc").Build();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            //TODO: implement internal model. Ref: https://github.com/CyberShadow/RABCDAsm/blob/f7c28ac3407b47a9fd8707d0a9f1f3f326d39484/assembler.d#L967

        }
    }
}
