using Microsoft.Maui.Controls;

namespace EasyTravel
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
