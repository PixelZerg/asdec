using PygmentSharp.Core;
using PygmentSharp.Core.Lexing;
using PygmentSharp.Core.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static asdec.ABCUtil;

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

            var ruleset = builder.NewRuleSet();

            //add all opcodes and crate rgx for non-opcodes
            string non = @"\b(?!.*((";
            foreach (OpcodeInfo op in opcodeInfo)
            {
                ruleset.Add(op.name+@"\b", TokenTypes.Keyword);
                non += op.name + "|";
            }
            non = non.TrimEnd('|')+ @")\s))[^\n]+";


            ruleset.Add(non, TokenTypes.Name);//nons
            ruleset.Add(@"\s+\s", TokenTypes.Whitespace);

            ruleset.Add(@"#[^\n]+", TokenTypes.Literal);//compiler directive
            ruleset.Add(@";[^\n]+", TokenTypes.Comment);//not working

            rules["root"] = ruleset.Build();
            return rules;
        }
    }
}
