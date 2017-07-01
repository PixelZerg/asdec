using asdec.ASModel.Objects;
using asdec.Errors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static asdec.ABCUtil;

namespace asdec.ASModel
{
    public class ArgumentsParser
    {
        public OpcodeArgumentType[] types = null;
        private string _raw = null;
        public StringReader sr = null;
        public ArgumentsParser(OpcodeArgumentType[] types, string args)
        {
            this.types = types;
            this.sr = new StringReader(args);
            this._raw = args;
        }

        public List<Argument> Parse()
        {
            List<Argument> ret = new List<Argument>();
            int i = 0;
            foreach (OpcodeArgumentType type in types)
            {
                switch (type)
                {
                    case OpcodeArgumentType.Unknown:
                        throw new ASParsingException("Unknown opcode argument type!");

                    case OpcodeArgumentType.ByteLiteral:
                        ret.Add(new ValueArg((SByte)ReadInt()));
                        break;
                    case OpcodeArgumentType.UByteLiteral:
                        ret.Add(new ValueArg((Byte)ReadUInt()));
                        break;
                    case OpcodeArgumentType.IntLiteral:
                    case OpcodeArgumentType.Int:
                        ret.Add(new ValueArg((Int32)ReadInt()));
                        break;
                    case OpcodeArgumentType.UIntLiteral:
                    case OpcodeArgumentType.UInt:
                        ret.Add(new ValueArg((UInt32)ReadUInt()));
                        break;
                    case OpcodeArgumentType.Double:
                        ret.Add(new ValueArg((Double)ReadDouble()));
                        break;
                    case OpcodeArgumentType.String:
                        ret.Add(new ValueArg((String)ReadString()));
                        break;

                    case OpcodeArgumentType.Namespace:
                        ret.Add(ReadNamespace());
                        break;
                        //TODO
                }
                if (i < types.Length - 1) Expect(",");
                i++;
            }

            return ret;
        }

        private string ReadWord()
        {
            SkipWhitespace();
            StringBuilder sb = new StringBuilder();
            if (!IsWordChar((Char)sr.Peek())) return null;
            while (true)
            {
                char c = (Char)sr.Peek();
                if (!IsWordChar(c)) break;
                sb.Append((Char)sr.Read());
            }
            return sb.ToString();
        }

        private void SkipWhitespace()
        {
            while (true)
            {
                char c = (Char)sr.Peek();
                if (c == ' ' || c == '\r' || c == '\n' || c == '\t') sr.Read();//skip char
                else return;
            }
        }

        private bool IsWordChar(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || c == '_' || c == '-' || c == '+' || c == '.';
        }

        private long ReadInt()
        {
            //https://github.com/CyberShadow/RABCDAsm/blob/master/assembler.d#L600
            string w = ReadWord();
            if (w == "null") return Utils.NULL_INT;
            var v = long.Parse(w);
            if (!(v >= Utils.MIN_INT && v <= Utils.MAX_INT))
            {
                throw new ASParsingException("Int out of bounds: " + v);
            }
            return v;
        }

        private ulong ReadUInt()
        {
            string w = ReadWord();
            if (w == "null") return Utils.NULL_UINT;
            var v = ulong.Parse(w);
            if (!(v <= Utils.MAX_UINT))
            {
                throw new ASParsingException("UInt out of bounds: " + v);
            }
            return v;
        }

        private double ReadDouble()
        {
            string w = ReadWord();
            if (w == "null") return Utils.NULL_DOUBLE;
            return Double.Parse(w);
        }

        private string ReadString()
        {
            SkipWhitespace();
            char c = (Char)sr.Read();
            if (c != '"')
            {
                if (ReadWord() == "ull") return null;
                else throw new ASParsingException("String literal expected!");
            }
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                switch (c = (Char)sr.Read())
                {
                    case '"':return sb.ToString();
                    case '\\':
                        switch (c = (Char)sr.Read())
                        {
                            case 'n':sb.Append('\n'); break;
                            case 'r':sb.Append('\r'); break;
                            case 'x':
                                sb.Append((Char)Int32.Parse(((Char)sr.Read() + (Char)sr.Read()).ToString(), System.Globalization.NumberStyles.HexNumber));
                                break;
                            default: sb.Append(c); break;
                        }
                        break;
                    case Char.MaxValue:throw new ASParsingException("Unexpected null/terminator");
                    default: sb.Append(c); break;
                }
            }
        }

        private Dictionary<string, uint> NamespaceLabels = new Dictionary<string, uint>();

        private Namespace ReadNamespace()
        {
            string word = ReadWord();
            if (word == "null") return null;
            ASType kind = (ASType)Enum.Parse(typeof(ASType), word);
            Expect("(");

            string name = ReadString();
            uint id;
            if ((Char)sr.Peek() == ',')
            {
                sr.Read();//skipchar
                string s = ReadString();
                try {
                    //id = NamespaceLabels.First(x => x.Key == s).Value;//s in namespacelabels
                    id = NamespaceLabels[s];
                }
                catch
                {
                    id = (uint)NamespaceLabels.Count + 1;
                    NamespaceLabels.Add(s, id);
                }
            } else id = 0;
            Expect(")");

            return new Namespace(kind, name, id);
        }

        private bool Expect(string str)
        {
            if (!Accept(str)) throw new ASParsingException("Error parsing argument: " + this._raw);
            return true;
        }

        private bool Accept(string str)
        {
            return sr.Read(str.Length) == str;
        }
    }
}
