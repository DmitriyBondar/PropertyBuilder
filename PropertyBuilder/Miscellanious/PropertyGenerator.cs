using System;
using System.Text;
using PropertyBuilder.Model;

namespace PropertyBuilder.Miscellanious
{
    public class PropertyGenerator
    {
        public PropertyGenerator(IProperty property)
        {
            _property = property;
        }

        private readonly IProperty _property;

        public string CreatePropertyBody()
        { 
            var propertyBody = new StringBuilder();

            propertyBody.Append(_property.PropertyType.SelectedAccessModifier + " ");

            if (!_property.PropertyType.WithBackingField && !_property.PropertyType.IsCollection &&
                !_property.PropertyType.WithNotifier)
                return CreateAutoProperty();

            if (_property.PropertyType.WithBackingField)
                propertyBody.AppendLine(CreateFieldNameFromPropertyName());

            return propertyBody.ToString();
        }

        public string CreateAutoProperty()
        {
            return $"{_property.PropertyType.SelectedAccessModifier} {_property.PropertyType.SelectedType} {_property.Name} {{ get; set;}} ";
        }

        private string CreateFieldNameFromPropertyName()
        {
            if (string.IsNullOrWhiteSpace(_property.Name))
                throw new InvalidOperationException("Property Name Can't be null or empty");

            return _property.PropertyType.BackingFieldPrefix + char.ToLower(_property.Name[0]) + _property.Name.Substring(1);
        }
    }
}
