using asdec.ASModel.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asdec.ASModel
{
    public class Dir
    {
        public Dir Parent = null;
        public string Name = string.Empty;
        public Dictionary<string, Dir> subdirs { get; internal set; }
        public Dictionary<string, Asasm> files = new Dictionary<string, Asasm>();

        /// <summary>
        /// root dir constructor
        /// </summary>
        public Dir() 
        {
            subdirs=new Dictionary<string, Dir>();
        }

        public Dir(string name,Dir parent)
        {
            this.Parent = parent;
            this.Name = name;
            subdirs = new Dictionary<string, Dir>();
        }

        /// <summary>
        /// no leading nor trailing
        /// </summary>
        public Dir AddSubDir(string path)
        {
            int si = path.IndexOf('/');
            string sub = path;
            string el = null;
            if (si > -1)
            {
                sub = path.Substring(0, si);
                el = path.Remove(0, si + 1);
            }
            Dir ret = new Dir(sub,this);
            if (el != null)
            {
                ret.AddSubDir(el);
            }
            subdirs.Add(sub, ret);
            return ret;
        }

        public Asasm AddFile(string name, Asasm file)
        {
            files.Add(name, file);
            return file;
        }

        /// <summary>
        /// no leading nor trailing
        /// </summary>
        public Asasm GetFile(string path)
        {
            string[] p = path.Split('/');
            Dir cdir = this;
            for (int i = 0; i < p.Length-1; i++)
            {
                cdir = cdir.subdirs[p[i]];
            }
            return cdir.files[p[p.Length - 1]];
        }

        /// <summary>
        /// leading no trailing
        /// </summary>
        public string GetPath()
        {
            string ret = "";
            if (this.Parent != null)
            {
                ret += this.Parent.GetPath()+@"/";
            }
            ret += this.Name;
            return ret;
        }
    }
}
