using PropertyBuilder.ViewModel;

namespace PropertyBuilder.Model
{
    public class DefaultPropertyType : PropertyTypeViewModel
    {
        static DefaultPropertyType() { }

        private DefaultPropertyType() { }

        private static DefaultPropertyType _instance;
        public static DefaultPropertyType Instance => _instance ?? (_instance = new DefaultPropertyType
        {
            IsCollection = true,
            SelectedCollectionType = "ObservableCollection<string>",
            SelectedAccessModifier = "public",
            SelectedType = "string",
            WithNotifier = true,
            BackingFieldPrefix = "_",
            NotifyMethodName = "OnPropertyChanged()",
            WithBackingField = true
        });
    }
}
