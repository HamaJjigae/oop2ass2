using Microsoft.Maui.Controls;
using Traveless.Services;
namespace Traveless
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }
    }
}
