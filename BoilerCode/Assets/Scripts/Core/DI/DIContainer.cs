using System;
using System.Collections.Generic;

namespace Core.DI
{
    public class DIContainer
    {
        private readonly Dictionary<Type, Func<object>> _bindings = new();
        private readonly Dictionary<Type, object> _singletonInstances = new();
        private readonly Dictionary<string, Type> _namedBindings = new();

        // Basic Binding
        public void Bind<T>(Func<T> factory) => _bindings[typeof(T)] = () => factory();
        public void BindInstance<T>(T instance) => _bindings[typeof(T)] = () => instance;

        // Named Bindings
        public void BindNamed<T>(string name, Func<T> factory)
        {
            _namedBindings[name] = typeof(T);
            Bind(factory);
        }

        public void BindNamedInstance<T>(string name, T instance)
        {
            _namedBindings[name] = typeof(T);
            BindInstance(instance);
        }

        // Singleton Pattern Support
        public void BindSingleton<T>(Func<T> factory) where T : class
        {
            _bindings[typeof(T)] = () =>
            {
                if (!_singletonInstances.TryGetValue(typeof(T), out var instance))
                {
                    instance = factory();
                    _singletonInstances[typeof(T)] = instance;
                }
                return instance;
            };
        }

        // Resolution
        public T Resolve<T>() => (T)_bindings[typeof(T)]();
        public T ResolveNamed<T>(string name) => (T)_bindings[_namedBindings[name]]();

        // Conditional Resolution
        public T ResolveOrDefault<T>() where T : class => HasBinding<T>() ? Resolve<T>() : default(T);
        public bool TryResolve<T>(out T result) where T : class
        {
            if (HasBinding<T>())
            {
                result = Resolve<T>();
                return true;
            }
            result = default(T);
            return false;
        }

        // Binding Management
        public bool HasBinding<T>() => _bindings.ContainsKey(typeof(T));
        public bool HasNamedBinding(string name) => _namedBindings.ContainsKey(name);
        public void RemoveBinding<T>() => _bindings.Remove(typeof(T));
        public void RemoveNamedBinding(string name)
        {
            if (_namedBindings.ContainsKey(name))
            {
                _bindings.Remove(_namedBindings[name]);
                _namedBindings.Remove(name);
            }
        }
        public void ClearAllBindings()
        {
            _bindings.Clear();
            _singletonInstances.Clear();
            _namedBindings.Clear();
        }

        // Batch Operations
        public List<Type> GetAllBoundTypes() => new List<Type>(_bindings.Keys);
        public int BindingCount => _bindings.Count;
    }
}
