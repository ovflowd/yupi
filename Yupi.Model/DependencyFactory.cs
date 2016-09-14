namespace Yupi.Model
{
    using System;
    using System.Reflection;

    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Configuration;

    using Yupi.Model.Repository;

    /// <summary>
    /// Simple wrapper for unity resolution.
    /// </summary>
    public class DependencyFactory
    {
        #region Fields

        private static IUnityContainer _container;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Public reference to the unity container which will 
        /// allow the ability to register instrances or take 
        /// other actions on the container.
        /// </summary>
        public static IUnityContainer Container
        {
            get { return _container; }
            private set { _container = value; }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Static constructor for DependencyFactory which will 
        /// initialize the unity container.
        /// </summary>
        static DependencyFactory()
        {
            _container = new UnityContainer();
            _container.RegisterType(typeof(IRepository<>), typeof(Repository<>));

            _container.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.ContainerControlled);
        }

        #endregion Constructors

        #region Methods

        public static void RegisterInstance<T>(T instance)
        {
            _container.RegisterInstance(instance);
        }

        /// <summary>
        /// Resolves the type parameter T to an instance of the appropriate type.
        /// </summary>
        /// <typeparam name="T">Type of object to return</typeparam>
        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        #endregion Methods
    }
}