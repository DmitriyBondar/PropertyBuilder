using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
using PropertyBuilder.Annotations;
using PropertyBuilder.Command;
using PropertyBuilder.Model;

namespace PropertyBuilder.ViewModel
{
    public sealed class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Properties
        private PropertyTypeViewModel _defaultType;
        public PropertyTypeViewModel DefaultType => _defaultType ?? (_defaultType = DefaultPropertyType.Instance);

        private FlowDocument _resultText;
        public FlowDocument ResultText
        {
            get { return _resultText ?? (_resultText = new FlowDocument()); }
            set
            {
                _resultText = value; 
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Property> _properties;
        public ObservableCollection<Property> Properties
        {
            get { return _properties ?? (_properties = new ObservableCollection<Property>
            {
                new Property("...", DefaultType)
            }); }
            set
            {
                _properties = value; 
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands
        private DelagateCommand _clearFieldsCommand;
        public DelagateCommand ClearDefaultSettingsCommand => _clearFieldsCommand ?? (_clearFieldsCommand = new DelagateCommand(DefaultType.RestoreToDefault));

        private DelagateCommand _generatePropertiesCommand;
        public DelagateCommand GeneratePropertiesCommand => _generatePropertiesCommand ?? (_generatePropertiesCommand = new DelagateCommand(GenerateProperties, CanGenerateProperties));

        private bool CanGenerateProperties(object arg)
        {
            return Properties.Count > 0;
        }

        #endregion

        #region Methods

        private void GenerateProperties()
        {
            
        }

        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
