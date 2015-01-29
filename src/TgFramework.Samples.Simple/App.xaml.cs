using System.Windows;

namespace TgFramework.Samples.Simple
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DependencyResolver.SetResolver(new SimpleInjectorResolver());
        }
    }
}
