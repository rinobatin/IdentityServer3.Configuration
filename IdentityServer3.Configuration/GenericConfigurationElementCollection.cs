using System.Collections.Generic;
using System.Configuration;

namespace IdentityServer3.Configuration
{
    internal class GenericConfigurationElementCollection<T> : ConfigurationElementCollection, IEnumerable<T> where T : ConfigurationElement, new()
    {
        readonly List<T> _elements = new List<T>();

        protected override ConfigurationElement CreateNewElement()
        {
            T newElement = new T();
            _elements.Add(newElement);
            return newElement;
        }

        protected override bool ThrowOnDuplicate { get { return true; } }

        protected override void BaseAdd(ConfigurationElement element)
        {
            base.BaseAdd(element, true);
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return _elements.Find(a => a.Equals(element));
        }

        public new IEnumerator<T> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }
    }
}