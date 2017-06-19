using PygmentSharp.Core;
using PygmentSharp.Core.Lexing;
using PygmentSharp.Core.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace asdec
{
    /// <summary>
    /// A lexer of ActionScript3
    /// </summary>
    [Lexer("avm", AlternateNames = "actionscriptvm,AVM,avm2,AVM2")]
    [LexerFileExtension("*.asasm")]
    public class AVMLexer : AVMLexerBase
    {
        protected override IDictionary<string, StateRule[]> GetStateRules()
        {
            var rules = new Dictionary<string, StateRule[]>();
            var builder = new StateRuleBuilder();

            var ruleset = builder.NewRuleSet();

            //add all opcodes and crate rgx for non-opcodes
            string non = @"\b(?!.*((";
            string ya = "";
            foreach (ABCUtil.OpcodeInfo op in ABCUtil.opcodeInfo)
            {
                //ruleset.Add(op.name+@"\b", TokenTypes.Keyword);
                ya += @"\b" + op.name + @"\b|";
                non += op.name + "|";
            }
            ya = ya.TrimEnd('|');
            non = non.TrimEnd('|') + @")\s))[^\n]+";

            ruleset.Add(ya, TokenTypes.Keyword);//yas
            //ruleset.Add(non, TokenTypes.Name);//nons

            ruleset.Add(@"\s+\s", TokenTypes.Whitespace);



            ruleset.Add(@"[\s]#[^\n]+", TokenTypes.Literal);//compiler directive
            ruleset.Add(@";[^\n]+", TokenTypes.Comment);
            ruleset.Add(@"\bL\d+:", TokenTypes.Literal.Number);//label

            rules["root"] = ruleset.Build();
            return rules;
        }
    }


    public abstract class AVMLexerBase : Lexer
    {
        /// <summary>
        /// When overridden in a child class, gets all the <see cref="Token"/>s for the given string
        /// </summary>
        /// <param name="text">The string to tokenize</param>
        /// <returns>A sequence of <see cref="Token"/> structs</returns>
        protected override IEnumerable<Token> GetTokensUnprocessed(string text)
        {
            var rules = GetStateRules();
            int pos = 0;
            var stateStack = new Stack<string>(50);
            stateStack.Push("root");
            var currentStateRules = rules[stateStack.Peek()];

            string nf = "";
            int nfpos = -1;
            while (true)
            {
                bool found = false;
                foreach (var rule in currentStateRules)
                {
                    var m = rule.Regex.Match(text, pos);
                    if (m.Success)
                    {
                        if (nfpos != -1&&(!String.IsNullOrEmpty(nf.Trim())))
                        {
                            yield return new Token(nfpos, TokenTypes.Token, nf.ToString());
                            nfpos = -1;
                            nf = "";
                        }

                        var context = new RegexLexerContext(pos, m, stateStack, rule.TokenType);
                        //Debug.Assert(m.Index == pos, $"Regex \"{rule.Regex}\" should have matched at position {pos} but matched at {m.Index}");

                        var tokens = rule.Action.Execute(context);

                        foreach (var token in tokens)
                            yield return token;

                        pos = context.Position;
                        currentStateRules = rules[stateStack.Peek()];
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    if (pos >= text.Length)
                        break;

                    if (text[pos] == '\n')
                    {
                        stateStack.Clear();
                        stateStack.Push("root");
                        currentStateRules = rules["root"];
                        yield return new Token(pos, TokenTypes.Text, "\n");
                        pos++;
                        continue;
                    }

                    //yield return new Token(pos, TokenTypes.Error, text[pos].ToString());
                    nf += text[pos].ToString();
                    if (nfpos == -1) nfpos = pos;
                    pos++;
                }
            }


        }

        /// <summary>
        /// Gets the state transition rules for the lexer. Each time a regex is matched,
        /// the internal state machine can be bumped to a new state which determines what
        /// regexes become valid again
        /// </summary>
        /// <returns></returns>
        protected abstract IDictionary<string, StateRule[]> GetStateRules();
    }
}
