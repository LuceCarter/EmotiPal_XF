using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EmotiPal.Views;

[assembly: ExportFont("WorkSans.ttf", Alias = "WorkSans")]
namespace EmotiPal
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
