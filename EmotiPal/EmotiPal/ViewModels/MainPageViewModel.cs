using System.Windows.Input;
using MvvmHelpers;
using Xamarin.Forms;

namespace EmotiPal.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel()
        {
            AnalyseTextCommand = new Command(() =>
            {
                Shell.Current.GoToAsync("sentiment");
            });
            AnalyseBodyLanguageCommand = new Command(() =>
            {
                Shell.Current.GoToAsync("bodylanguage");
            });
        }

        public ICommand AnalyseTextCommand { get; }

        public ICommand AnalyseBodyLanguageCommand { get; }
    }
}
