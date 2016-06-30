using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PropertyBuilder.Model;

namespace PropertyBuilder.Controls
{
    public class PropertyTagsBox : ListBox
    {
        public PropertyTagsBox()
        {
            if (ItemsSource == null)
            {
                ItemsSource = new ObservableCollection<Property>();
                AddNewTemplateItem("...");
            }

            this.MouseDoubleClick += OnMouseDoubleClick;
            this.KeyDown += OnKeyDown;
        }

        private void OnMouseDoubleClick(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var itemsSource = ItemsSource as ObservableCollection<Property>;

            if (itemsSource != null && (itemsSource.LastOrDefault()?.Name != "..." || itemsSource.Count == 1))
                AddNewTemplateItem("...");
        }

        private void AddNewTemplateItem(string propertyName)
        {
            var itemsSource = ItemsSource as ObservableCollection<Property>;
            if (itemsSource != null)
            {
                var newBlancProperty = new Property(propertyName, DefaultPropertyType.Instance);

                newBlancProperty.PropertyClosed += NewBlancPropertyOnPropertyClosed;

                itemsSource.Add(newBlancProperty);
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Key == Key.Tab)
            {

                var focusedItem = keyEventArgs.OriginalSource as FrameworkElement;

                var itemsSource = ItemsSource as ObservableCollection<Property>;

                if (itemsSource != null)
                {
                    var lastItem = itemsSource.Last();

                    if (focusedItem != null && focusedItem.DataContext.Equals(lastItem))
                    {
                        AddNewTemplateItem("...");
                    }
                }
            }
        }

        private void NewBlancPropertyOnPropertyClosed(object sender, EventArgs eventArgs)
        {
            var property = sender as Property;
            if (property == null)
                return;

            property.PropertyClosed -= NewBlancPropertyOnPropertyClosed;

            var itemsSource = ItemsSource as ObservableCollection<Property>;

            itemsSource?.Remove(property);

            if (itemsSource?.Count == 0)
                AddNewTemplateItem("...");
        }
    }
}
