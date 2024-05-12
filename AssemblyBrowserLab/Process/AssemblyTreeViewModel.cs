using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using AssemblyBrowserLib;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace AssemblyBrowserLab
{
    internal class AssemblyTreeViewModel: INotifyPropertyChanged
    {

        private AssemblyCrawler _crawler;
        private Mapper _mapper;
        private List<TreeViewItem> _treeNodes;

        public AssemblyTreeViewModel() 
        {
            _crawler    = new AssemblyCrawler();
            _mapper     = new Mapper();
            _treeNodes  = new List<TreeViewItem>();
        }
        public List<TreeViewItem> TreeNodes
        {
            get { return _treeNodes; }
            set
            {
                _treeNodes = value;
                OnPropertyChanged();
            }
        }

        private string? _assemblyName;
        private AssemblyTree? _model;

        public string? SourceName
        {
            get { return _assemblyName; }
            set
            {
                if (value == null)
                    return;

                try
                {
                    _crawler.Process(value);
                }
                catch (Exception)
                {
                    MessageBox.Show("Can't process this file",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }

                _assemblyName = value;
                TreeNodes = _mapper.Process(_crawler.Model()!);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
