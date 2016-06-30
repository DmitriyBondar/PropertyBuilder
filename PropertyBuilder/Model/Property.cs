using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using PropertyBuilder.Annotations;
using PropertyBuilder.Command;
using PropertyBuilder.ViewModel;

namespace PropertyBuilder.Model
{
    public interface IProperty
    {
        IPropertyTypeViewModel PropertyType { get; set; }
        string Name { get; set; }
    }

    public sealed class Property : INotifyPropertyChanged, IProperty
    {
        private Property() { }
        public Property(string name, IPropertyTypeViewModel propertyType)
        {
            _name = name;
            PropertyType = new PropertyTypeViewModel(propertyType);
            PropertyType.TypeChanged += PropertyTypeOnTypeChanged;

            TypeAbbreviature = PropertyType.IsCollection
                ? PropertyType.SelectedCollectionType?.FirstOrDefault().ToString()
                : PropertyType.SelectedType?.FirstOrDefault().ToString();
        }

        public IPropertyTypeViewModel PropertyType { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == "..." && _name != value)
                {
                    _name = String.Empty;
                    value = value.Replace(".", "");
                }
                    
                _name = value; 
                OnPropertyChanged();
            }
        }

        private string _typeAbbreviature;
        public string TypeAbbreviature
        {
            get { return _typeAbbreviature; }
            set
            {
                _typeAbbreviature = value;
                OnPropertyChanged();
            }
        }

        private DelagateCommand _closePropertyCommand;
        public DelagateCommand ClosePropertyCommand => _closePropertyCommand ?? (_closePropertyCommand = new DelagateCommand(CloseProperty));

        public event EventHandler PropertyClosed;

        private void CloseProperty()
        {
            PropertyClosed?.Invoke(this, EventArgs.Empty);
        }

        private void PropertyTypeOnTypeChanged(object sender, PropertyTypeViewModel.TypeChangedEventArgs typeChangedEventArgs)
        {
            TypeAbbreviature = typeChangedEventArgs.NewType.ToString().ToUpper();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
