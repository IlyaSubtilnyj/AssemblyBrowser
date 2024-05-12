using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLib
{
    public class AssemblyTree
    {
        public NamespaceNode root;
        public AssemblyTree() 
        { 
            root = new NamespaceNode();
        }
    }
}
