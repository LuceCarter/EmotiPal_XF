using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EmotiPal.Views;
using EmotiPal.Helpers;

[assembly: ExportFont("WorkSans.ttf", Alias = "WorkSans")]
[assembly: ExportFont("RibeyeMarrow.ttf", Alias = "Ribeye")]
namespace EmotiPal
{
    public partial class App : Application
    {
        public static Realms.Sync.App RealmApp;
       
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            RealmApp = Realms.Sync.App.Create(APIKeys.RealmAppId);
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
