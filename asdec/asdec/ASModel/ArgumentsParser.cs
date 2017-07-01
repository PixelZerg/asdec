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
        public Node Parent = null;
        public ArgumentsParser(Node parent, OpcodeArgumentType[] types, string args)
        {
            this.Parent = parent;
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
                    case OpcodeArgumentType.Multiname:
                        ret.Add(ReadMultiname());
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
                    id = Parent.mostParent().NamespaceLabels[s];
                }
                catch
                {
                    id = (uint)Parent.mostParent().NamespaceLabels.Count + 1;
                    Parent.mostParent().NamespaceLabels.Add(s, id);
                }
            } else id = 0;
            Expect(")");

            return new Namespace(kind, name, id);
        }

        public MultinameBase ReadMultiname()
        {
            string word = ReadWord();
            if (word == "null") return null;

            MultinameBase ret;
            ASType kind = (ASType)Enum.Parse(typeof(ASType), word);

            Expect("(");
            switch (kind)
            {
                case ASType.QName:
                case ASType.QNameA:
                    ret = new QName();
                    ((QName)ret).Ns = ReadNamespace();
                    Expect(",");
                    ((QName)ret).Name = ReadString();
                    break;
                case ASType.RTQName:
                case ASType.RTQNameA:
                    ret = new RTQName();
                    ((RTQName)ret).Name = ReadString();
                    break;
                case ASType.RTQNameL:
                case ASType.RTQNameLA:
                    ret = new MultinameBase();
                    break;
                case ASType.Multiname:
                case ASType.MultinameA:
                    ret = new Multiname();
                    ((Multiname)ret).Name = ReadString();
                    Expect(",");
                    ((Multiname)ret).NsSet =ReadNamespaceSet();
                    break;
                case ASType.MultinameL:
                case ASType.MultinameLA:
                    ret = new Multiname();
                    ((MultinameL)ret).NsSet = ReadNamespaceSet();
                    break;
                case ASType.TypeName:
                    ret = new TypeName();
                    ((TypeName)ret).Name = ReadMultiname();
                    ((TypeName)ret).Params = ReadList('<','>',ReadMultiname,false);
                    break;
                default:
                    throw new ASParsingException("Cannot handle multiname kind: " + kind.ToString());
            }
            ret.Kind = kind;
            Expect(")");
            return ret;
        }

        private List<T> ReadList<T>(char OPEN, char CLOSE, Func<T> READER, bool ALLOW_NULL)
        {
            if (ALLOW_NULL)
            {
                SkipWhitespace();
                if((Char)sr.Peek() != OPEN)
                {
                    Expect("null");
                    return null;
                }
            }

            Expect(""+OPEN);
            List<T> a = new List<T>();

            SkipWhitespace();
            if((Char)sr.Peek() == CLOSE)
            {
                sr.Read();//skipchar //CLOSE
                if (ALLOW_NULL)
                {
                    //FIXME?
                    return a;
                }
                else return null;
            }

            while (true)
            {
                a.Add(READER());
                char c = (Char)sr.Read();
                if (c == CLOSE) break;
                if (c != ',') throw new ASParsingException("Expected " + CLOSE + " or ,");
            }
            return a;
        }

        private List<Namespace> ReadNamespaceSet()
        {
            return ReadList<Namespace>('[', ']', ReadNamespace, true);
        }

        private bool Expect(string str)
        {
            if (!Accept(str)) throw new ASParsingException("Expected \""+str+"\" when parsing argument: " + this._raw);
            return true;
        }

        private bool Accept(string str)
        {
            return sr.Read(str.Length) == str;
        }
    }
}
