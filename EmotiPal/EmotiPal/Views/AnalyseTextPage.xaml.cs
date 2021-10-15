using EmotiPal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realms;
using Realms.Sync;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmotiPal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnalyseTextPage : ContentPage
    {
        AnalyseTextPageViewModel viewModel;        
        public AnalyseTextPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, true);
            viewModel = new AnalyseTextPageViewModel();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            await viewModel.InitialiseRealm();           
            
        }
    }
}