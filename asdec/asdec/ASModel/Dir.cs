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
        public Dictionary<string, Dir> subdirs { get; internal set; } = new Dictionary<string, Dir>();
        public Dictionary<string, Asasm> files = new Dictionary<string, Asasm>();

        /// <summary>
        /// root dir constructor
        /// </summary>
        public Dir() { }

        public Dir(string name,Dir parent)
        {
            this.Parent = parent;
            this.Name = name;
        }

        public Dir AddSubDir(string name)
        {
            Dir ret = new Dir(name,this);
            subdirs.Add(name, ret);
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
