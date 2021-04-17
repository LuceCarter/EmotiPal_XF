using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EmotiPal.Views;

[assembly: ExportFont("WorkSans.ttf", Alias = "WorkSans")]
[assembly: ExportFont("RibeyeMarrow.ttf", Alias = "Ribeye")]
namespace EmotiPal
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new  MainPage());
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
