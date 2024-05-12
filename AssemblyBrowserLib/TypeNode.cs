using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLib
{

    public class TypeNode
    {

        public string name;

        public List<MethodData> methods;

        public List<PropertyData> properties;

        public List<FieldData> fields;
  
        public TypeNode(string name = "_dummy") 
        {
            this.name       = name;
            this.methods    = new List<MethodData>();
            this.properties = new List<PropertyData>();
            this.fields     = new List<FieldData>();
        }

        public class MethodData
        {
            public string? name;
            public Type? returnType;
            public List<ParameterData>? parameters;

            public MethodData() { }

            public class ParameterData
            {
                public string? name;
                public Type? type;
                public ParameterData() { }
            }
        }

        public class PropertyData
        {
            public Type? type;
            public string? name;
            public PropertyData() { }
        }

        public class FieldData
        {
            public Type? type;
            public string? name;
            public FieldData() { }
        }
    }
}
