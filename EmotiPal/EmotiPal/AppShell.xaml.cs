using System;
using System.Collections.Generic;
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
        }
    }
}
