using System.Diagnostics.CodeAnalysis;
using Prism.Mvvm;

namespace Prism.Ioc
{
    /// <summary>
    /// <see cref="IContainerRegistry"/> extensions.
    /// </summary>
    public static class IContainerRegistryExtensions
    {
        /// <summary>
        /// Registers an object to be used as a dialog in the IDialogService.
        /// </summary>
        /// <typeparam name="TView">The Type of object to register as the dialog</typeparam>
        /// <param name="containerRegistry"></param>
        /// <param name="name">The unique name to register with the dialog.</param>
        public static IContainerRegistry RegisterDialog<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] TView>(this IContainerRegistry containerRegistry, string name = null) =>
            containerRegistry.RegisterForNavigation<TView>(name);

        /// <summary>
        /// Registers an object to be used as a dialog in the IDialogService.
        /// </summary>
        /// <typeparam name="TView">The Type of object to register as the dialog</typeparam>
        /// <typeparam name="TViewModel">The ViewModel to use as the DataContext for the dialog</typeparam>
        /// <param name="containerRegistry"></param>
        /// <param name="name">The unique name to register with the dialog.</param>
        public static IContainerRegistry RegisterDialog<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] TView, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] TViewModel>(this IContainerRegistry containerRegistry, string name = null) where TViewModel : Dialogs.IDialogAware =>
            containerRegistry.RegisterForNavigation<TView, TViewModel>(name);

        /// <summary>
        /// Registers an object that implements IDialogWindow to be used to host all dialogs in the IDialogService.
        /// </summary>
        /// <typeparam name="TWindow">The Type of the Window class that will be used to host dialogs in the IDialogService</typeparam>
        /// <param name="containerRegistry"></param>
        public static IContainerRegistry RegisterDialogWindow<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] TWindow>(this IContainerRegistry containerRegistry) where TWindow : Dialogs.IDialogWindow => 
            containerRegistry.Register(typeof(Dialogs.IDialogWindow), typeof(TWindow));

        /// <summary>
        /// Registers an object that implements IDialogWindow to be used to host all dialogs in the IDialogService.
        /// </summary>
        /// <typeparam name="TWindow">The Type of the Window class that will be used to host dialogs in the IDialogService</typeparam>
        /// <param name="containerRegistry"></param>
        /// <param name="name">The name of the dialog window</param>
        public static IContainerRegistry RegisterDialogWindow<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] TWindow>(this IContainerRegistry containerRegistry, string name) where TWindow : Dialogs.IDialogWindow =>
            containerRegistry.Register(typeof(Dialogs.IDialogWindow), typeof(TWindow), name);

        /// <summary>
        /// Registers an object for navigation
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <param name="type">The type of object to register</param>
        /// <param name="name">The unique name to register with the object.</param>
        public static IContainerRegistry RegisterForNavigation(this IContainerRegistry containerRegistry, Type type, string name) =>
            containerRegistry.Register(typeof(object), type, name);

        /// <summary>
        /// Registers an object for navigation.
        /// </summary>
        /// <typeparam name="T">The Type of the object to register as the view</typeparam>
        /// <param name="containerRegistry"></param>
        /// <param name="name">The unique name to register with the object.</param>
        public static IContainerRegistry RegisterForNavigation<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] T>(this IContainerRegistry containerRegistry, string name = null)
        {
            Type type = typeof(T);
            string viewName = string.IsNullOrWhiteSpace(name) ? type.Name : name;
            return containerRegistry.RegisterForNavigation(type, viewName);
        }

        /// <summary>
        /// Registers an object for navigation with the ViewModel type to be used as the DataContext.
        /// </summary>
        /// <typeparam name="TView">The Type of object to register as the view</typeparam>
        /// <typeparam name="TViewModel">The ViewModel to use as the DataContext for the view</typeparam>
        /// <param name="containerRegistry"></param>
        /// <param name="name">The unique name to register with the view</param>
        public static IContainerRegistry RegisterForNavigation<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] TView, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] TViewModel>(this IContainerRegistry containerRegistry, string name = null) =>
            containerRegistry.RegisterForNavigationWithViewModel<TViewModel>(typeof(TView), name);

        private static IContainerRegistry RegisterForNavigationWithViewModel<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] TViewModel>(this IContainerRegistry containerRegistry, Type viewType, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                name = viewType.Name;

            ViewModelLocationProvider.Register(viewType.ToString(), typeof(TViewModel));
            return containerRegistry.RegisterForNavigation(viewType, name);
        }
    }
}
