using PygmentSharp.Core;
using PygmentSharp.Core.Lexing;
using PygmentSharp.Core.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asdec
{
    /// <summary>
    /// A lexer of ActionScript3
    /// </summary>
    [Lexer("avm", AlternateNames = "actionscriptvm,AVM,avm2,AVM2")]
    [LexerFileExtension("*.asasm")]
    public class AVMLexer : RegexLexer
    {
        protected override IDictionary<string, StateRule[]> GetStateRules()
        {
            var rules = new Dictionary<string, StateRule[]>();
            var builder = new StateRuleBuilder();

            rules["root"] = builder.NewRuleSet()
                
            ).Build();

            return rules;
        }
    }
}
