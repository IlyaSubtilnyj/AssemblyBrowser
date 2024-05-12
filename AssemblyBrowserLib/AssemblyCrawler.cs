using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using static AssemblyBrowserLib.TypeNode.MethodData;

namespace AssemblyBrowserLib
{
    public class AssemblyCrawler
    {

        private AssemblyTree? _tree;

        public AssemblyTree? Model() { return _tree; }

        public void Process(string filePath)
        {
            _tree               = new AssemblyTree();
            Assembly assembly   = Assembly.LoadFile(filePath);

            Array.ForEach(assembly.GetTypes(), Add);
        }

        public void Add(Type t)
        {

            if (_tree is null)
            {
                throw new InvalidOperationException();
            }

            string? domain = t.FullName;
            if (domain is null)
            {
                return;
            }

            NamespaceNode currNode = _tree.root;

            var subDomains = domain.Split('.');
            foreach (var subDomain in subDomains.Take(subDomains.Length - 1).ToArray())
            {
                NamespaceNode? node = currNode.namespaces.FirstOrDefault(item => item.name == subDomain);

                if (node is null)
                {
                    currNode.namespaces.Add(currNode = new NamespaceNode(subDomain));
                }
                else
                {
                    currNode = node;
                }
            }

            var typeNode = ComposeTypeNodeFromType(t);
            currNode.types.Add(typeNode);
        }

        private TypeNode ComposeTypeNodeFromType(Type t)
        {
            var name = t.Name;

            var methods = (from method in t.GetRuntimeMethods() where !method.IsDefined(typeof(ExtensionAttribute)) select method).Select(methodInfo => {
                               var method           = new TypeNode.MethodData();
                               method.name          = methodInfo.Name;
                               method.returnType    = methodInfo.ReturnType;
                               method.parameters    = methodInfo.GetParameters().Select(parameterInfo => 
                                    {
                                        var parameter   = new ParameterData();
                                        parameter.name  = parameterInfo.Name;
                                        parameter.type  = parameterInfo.ParameterType;
                                        return parameter;
                                    }).ToList();
                               return method;
                           }).ToList();

            var properties = t.GetRuntimeProperties().Select(propertyInfo =>
                {
                    var property    = new TypeNode.PropertyData();
                    property.type   = propertyInfo.PropertyType;
                    property.name   = propertyInfo.Name;
                    return property;
                }).ToList();

            var fields = t.GetRuntimeFields().Select(fieldInfo => 
                {
                    var field   = new TypeNode.FieldData();
                    field.type  = fieldInfo.FieldType;
                    field.name  = fieldInfo.Name;
                    return field;
                })
                .ToList();

            var typeNode        = new TypeNode();
            typeNode.name       = name;
            typeNode.methods    = methods;
            typeNode.properties = properties;
            typeNode.fields     = fields;
            return typeNode;
        }
    }
}