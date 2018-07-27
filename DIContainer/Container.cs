namespace DIContainer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// DI Container
    /// </summary>
    public class Container
    {
        /// <summary>
        /// Key: represents a registered type
        /// Value: delegate that creates the instance of the objec
        /// </summary>
        private readonly Dictionary<Type, Func<object>> _registrations = new Dictionary<Type, Func<object>>();

        /// <summary>
        /// Registers the type and the resolved instance
        /// </summary>
        /// <typeparam name="TService">Registered type</typeparam>
        /// <typeparam name="TImpl">Resolved instance</typeparam>
        public void Register<TService, TImpl>()
            where TImpl : TService
        {
            _registrations.Add(typeof(TService), () => GetInstance(typeof(TImpl)));
        }

        /// <summary>
        /// Registers the type and resolves itself
        /// </summary>
        /// <typeparam name="TService">Registered type</typeparam>
        /// <param name="instance">Resolved instance</param>
        public void RegisterSelf<TService>(TService instance)
        {
            _registrations.Add(typeof(TService), () => instance);
        }

        /// <summary>
        /// Returns an instance of the requested type from the container
        /// </summary>
        /// <param name="serviceType">Requested type</param>
        /// <returns>The retrieved object</returns>
        public object GetInstance(Type serviceType)
        {
            if (_registrations.TryGetValue(serviceType, out var creator))
            {
                return creator();
            }

            if (!serviceType.IsAbstract)
            {
                return CreateInstance(serviceType);
            }

            throw new InvalidOperationException("No registration for " + serviceType);
        }

        /// <summary>
        /// Creates an instance of requested type
        /// </summary>
        /// <param name="implementationType">Type of the retrieved object</param>
        /// <returns>The retrieved object</returns>
        private object CreateInstance(Type implementationType)
        {
            var ctor = implementationType.GetConstructors().Single();
            var parameterTypes = ctor.GetParameters().Select(p => p.ParameterType);
            var dependencies = parameterTypes.Select(GetInstance).ToArray();
            return Activator.CreateInstance(implementationType, dependencies);
        }
    }
}
