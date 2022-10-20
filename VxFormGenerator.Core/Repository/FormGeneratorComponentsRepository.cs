using System;
using System.Collections.Generic;

namespace VxFormGenerator.Core.Repository
{

    public class FormGeneratorComponentsRepository<TKey> : IFormGeneratorComponentsRepository
    {
        protected Dictionary<TKey, Type> ComponentDict = new Dictionary<TKey, Type>();

        public Type DefaultComponent { get; protected set; }

        public FormGeneratorComponentsRepository()
        {

        }

        public FormGeneratorComponentsRepository(Dictionary<TKey, Type> componentRegistrations)
        {
            ComponentDict = componentRegistrations;
        }
        public FormGeneratorComponentsRepository(Dictionary<TKey, Type> componentRegistrations, Type defaultComponent)
        {
            ComponentDict = componentRegistrations;
            DefaultComponent = defaultComponent;
        }


        protected void RegisterComponent(TKey key, Type component)
        {
            ComponentDict.Add(key, component);
        }

        protected void RemoveComponent(TKey key)
        {
            ComponentDict.Remove(key);
        }

        protected virtual Type GetComponent(TKey key)
        {
            var found = ComponentDict.TryGetValue(key, out Type outVar);

            return found ? outVar : DefaultComponent;
        }

        public void Clear()
        {
            ComponentDict.Clear();
        }

        public void RegisterComponent(object key, Type component)
        {
            RegisterComponent((TKey) key, component);
        }

        public void RemoveComponent(object key)
        {
            RemoveComponent((TKey) key);
        }

        public Type GetComponent(object key)
        {
            return GetComponent((TKey) key);
        }
    }


}
