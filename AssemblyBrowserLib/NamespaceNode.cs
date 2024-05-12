using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLib
{
    public class NamespaceNode
    {
        public string name;
        public List<NamespaceNode> namespaces;
        public List<TypeNode> types;
        public NamespaceNode(string name = "root") 
        {
            this.name       = name;
            this.namespaces = new List<NamespaceNode>();
            this.types      = new List<TypeNode>(); 
        }
    }
}
