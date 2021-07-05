using EmotiPal.Views;
using Xamarin.Forms;

namespace EmotiPal
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("imageresult", typeof(AnalyseImageResultPage));
            Routing.RegisterRoute("sentiment", typeof(AnalyseTextPage));
            Routing.RegisterRoute("bodylanguage", typeof(AnalyseBodyLanguagePage));
        }
    }
}
