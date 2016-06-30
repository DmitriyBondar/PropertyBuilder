using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using PropertyBuilder.Annotations;
using PropertyBuilder.Miscellanious;

namespace PropertyBuilder.ViewModel
{
    public interface IPropertyTypeViewModel
    {
        ObservableCollection<string> PrimitiveTypes { get; set; }
        string SelectedType { get; set; }
        ObservableCollection<string> GenericCollections { get; set; }
        string SelectedCollectionType { get; set; }
        bool IsCollection { get; set; }
        ObservableCollection<string> AccessModifiers { get; set; }
        string SelectedAccessModifier { get; set; }
        bool WithBackingField { get; set; }
        string BackingFieldPrefix { get; set; }
        bool WithNotifier { get; set; }
        string NotifyMethodName { get; set; }

        /// <summary>
        /// <remarks> Later review </remarks>
        /// </summary>
        void RestoreToDefault();

        event EventHandler<PropertyTypeViewModel.TypeChangedEventArgs> TypeChanged;
        event PropertyChangedEventHandler PropertyChanged;
    }

    public class PropertyTypeViewModel : INotifyPropertyChanged, IPropertyTypeViewModel
    {
        protected PropertyTypeViewModel() { }

        /// <summary>
        /// Copy constructor
        /// </summary>
        public PropertyTypeViewModel(IPropertyTypeViewModel copyPropertyType) : this()
        {
            IsCollection = copyPropertyType.IsCollection;
            SelectedCollectionType = copyPropertyType.SelectedCollectionType;
            SelectedType = copyPropertyType.SelectedType;
            SelectedAccessModifier = copyPropertyType.SelectedAccessModifier;
            WithBackingField = copyPropertyType.WithBackingField;
            BackingFieldPrefix = copyPropertyType.BackingFieldPrefix;
            WithNotifier = copyPropertyType.WithNotifier;
            NotifyMethodName = copyPropertyType.NotifyMethodName;
        }


        public ObservableCollection<string> PrimitiveTypes { get; set; } = new ObservableCollection<string>
        {
            "string",
            "int",
            "bool",
            "DateTime",
            "short",
            "long",
            "double",
            "float",
            "char",
            "object",
            ""
        };

        private string _selectedType;
        public string SelectedType
        {
            get { return _selectedType; }
            set
            {
                if (value != _selectedType)
                {
                    _selectedType = value;
                    OnPropertyChanged();

                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        var s = GenericCollections.IndexOf(SelectedCollectionType);
                        GenericCollections.ApplyType(value);

                        if (s >= 0)
                            SelectedCollectionType = GenericCollections[s];
                    }
                        
                    var t = IsCollection ? SelectedCollectionType.FirstOrDefault() : SelectedType.FirstOrDefault();
                    
                    TypeChanged?.Invoke(this, new TypeChangedEventArgs(t));
                }
            }
        }

        public ObservableCollection<string> GenericCollections { get; set; } = new ObservableCollection<string>
        {
            "ObservableCollection<T>",
            "List<T>",
            "Dictionary<T, T>",
            "HashSet<T>",
            "LinkedList<T>",
            "Queue<T>",
            "Stack<T>",
            ""
        };

        private string _selectedCollectionType;
        public string SelectedCollectionType
        {
            get { return _selectedCollectionType; }
            set
            {
                _selectedCollectionType = value;
                OnPropertyChanged();

                var t = value?.FirstOrDefault();
                if (t != null)
                    TypeChanged?.Invoke(this, new TypeChangedEventArgs((char)t));
            }
        }

        private bool _isCollection;
        public bool IsCollection
        {
            get { return _isCollection; }
            set
            {
                _isCollection = value;
                OnPropertyChanged();

                var t = value ? SelectedCollectionType?.FirstOrDefault() : SelectedType?.FirstOrDefault();
                if (t != null)
                    TypeChanged?.Invoke(this, new TypeChangedEventArgs((char)t));
            }
        }

        public ObservableCollection<string> AccessModifiers { get; set; } = new ObservableCollection<string>
        {
            "public",
            "protected",
            "internal",
            "private",
            ""
        };

        private string _selectedAccessModifier;
        public string SelectedAccessModifier
        {
            get { return _selectedAccessModifier; }
            set
            {
                _selectedAccessModifier = value;
                OnPropertyChanged();
            }
        }

        private bool _withBackingField;
        public bool WithBackingField
        {
            get { return _withBackingField; }
            set
            {
                _withBackingField = value;
                OnPropertyChanged();
            }
        }

        private string _backingFieldPrefix;
        public string BackingFieldPrefix
        {
            get { return _backingFieldPrefix; }
            set
            {
                _backingFieldPrefix = value;
                OnPropertyChanged();
            }
        }

        private bool _withNotifier;
        public bool WithNotifier
        {
            get { return _withNotifier; }
            set
            {
                _withNotifier = value;
                OnPropertyChanged();
            }
        }

        private string _notifyMethodName;
        public string NotifyMethodName
        {
            get { return _notifyMethodName; }
            set
            {
                _notifyMethodName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// <remarks> Later review </remarks>
        /// </summary>
        public void RestoreToDefault()
        {
            SelectedType = String.Empty;
            IsCollection = false;
            SelectedCollectionType = String.Empty;
            SelectedAccessModifier = String.Empty;
            WithBackingField = false;
            BackingFieldPrefix = String.Empty;
            WithNotifier = false;
            NotifyMethodName = String.Empty;
            GenericCollections.ApplyType("T");
        }

        public event EventHandler<TypeChangedEventArgs> TypeChanged;

        public sealed class TypeChangedEventArgs : EventArgs
        {
            public TypeChangedEventArgs(char newType)
            {
                NewType = newType;
            }

            public char NewType { get; }
        }


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
