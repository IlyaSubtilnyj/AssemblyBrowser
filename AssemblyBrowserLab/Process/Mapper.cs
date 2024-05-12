using AssemblyBrowserLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using static AssemblyBrowserLib.TypeNode;
using static AssemblyBrowserLib.TypeNode.MethodData;

namespace AssemblyBrowserLab
{
    internal class Mapper
    {

        public Mapper() { }

        public List<TreeViewItem> Process(AssemblyTree model)
        {
            List<TreeViewItem> res = new List<TreeViewItem>();

            foreach (var node in model.root.namespaces)
            {
                var item = ProcessNamespace(node);
                res.Add(item);
            }

            return res;
        }

        public TreeViewItem ProcessNamespace(NamespaceNode node)
        {
            var res     = new TreeViewItem();
            res.Header  = node.name;

            foreach (var nestedNode in node.namespaces)
            {
                TreeViewItem nested = ProcessNamespace(nestedNode);
                res.Items.Add(nested);
            }

            foreach (var nestedNode in node.types)
            {
                TreeViewItem nested = ProcessType(nestedNode);
                res.Items.Add(nested);
            }

            return res;
        }

        public TreeViewItem ProcessType(TypeNode node)
        {
            var res = new TreeViewItem();

            res.Header = node.name;

            foreach (var field in node.fields)
            {
                res.Items.Add(ProcessField(field));
            }

            foreach (var propertyData in node.properties)
            {
                res.Items.Add(ProcessProperty(propertyData));
            }

            foreach (var methodData in node.methods)
            {
                res.Items.Add(ProcessMethod(methodData));
            }

            return res;
        }

        public TreeViewItem ProcessField(FieldData field) 
        {
            var res = new TreeViewItem();

            res.Header = $"field - {field.type} {field.name}";

            return res;
        }

        public TreeViewItem ProcessProperty(PropertyData property) 
        {
            var res = new TreeViewItem();

            res.Header = $"property - {property.type} {property.name}";

            return res;
        }

        public TreeViewItem ProcessMethod(MethodData method) 
        {
            var res = new TreeViewItem();

            res.Header = $"method - {method.returnType} {method.name}({GetParameterSignature(method.parameters!)})";

            return res;
        }

        // Вспомогательный метод для получения сигнатуры параметров метода
        static string GetParameterSignature(List<ParameterData> parameters)
        {
            return string.Join(", ", parameters.Select(p => $"{p.type} {p.name}"));
        }
    }
}
