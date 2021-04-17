using EmotiPal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmotiPal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnalyseTextPage : ContentPage
    {
        public AnalyseTextPage()
        {
            InitializeComponent();
            BindingContext = new AnalyseTextPageViewModel();
        }
    }
}