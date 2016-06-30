using System;
using NUnit.Framework;
using PropertyBuilder.Miscellanious;
using PropertyBuilder.Model;

namespace PropertyBuilderTests
{
    [TestFixture]
    public class PropertyGeneratorTests
    {
        public PropertyGeneratorTests()
        {
            _propertyGenerator = new PropertyGenerator(new Property("TestName", DefaultPropertyType.Instance));
        }

        private PropertyGenerator _propertyGenerator;

        [Test]
        public void CreatePropertyBody()
        {
            var result = _propertyGenerator.CreatePropertyBody();

            Console.WriteLine(result);
        }

        [Test]
        public void CreateAutoProperty()
        {
            var result = _propertyGenerator.CreateAutoProperty();

            Console.WriteLine(result);
        }
    }
}
