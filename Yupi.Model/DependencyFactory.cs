using Yupi.Model.Repository;

namespace Yupi.Model
{
    /// <summary>
    ///     Simple wrapper for unity resolution.
    /// </summary>
    public class DependencyFactory
    {
        /// <summary>
        ///     Static constructor for DependencyFactory which will
        ///     initialize the unity container.
        /// </summary>
        static DependencyFactory()
        {
            Container = new UnityContainer();
            Container.RegisterType(typeof(IRepository<>), typeof(Repository<>));

            Container.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.ContainerControlled);
        }

        /// <summary>
        ///     Public reference to the unity container which will
        ///     allow the ability to register instrances or take
        ///     other actions on the container.
        /// </summary>
        public static IUnityContainer Container { get; }

        public static void RegisterInstance<T>(T instance)
        {
            Container.RegisterInstance(instance);
        }

        /// <summary>
        ///     Resolves the type parameter T to an instance of the appropriate type.
        /// </summary>
        /// <typeparam name="T">Type of object to return</typeparam>
        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}