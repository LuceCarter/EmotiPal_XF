using EmotiPal.Helpers;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EmotiPal.ViewModels
{
    public class AnalyseTextPageViewModel : BaseViewModel
    {
        private string _textForAnalysis;
        private string _sentimentAnalysisResult;
        private Color _sentimentResultColour;

        public AnalyseTextPageViewModel()
        {
            AnalyseTextCommand = new Command(async () => await AnalyseText(TextForAnalysis));
            SentimentResultColour = Color.Black;
        }

        public string TextForAnalysis
        {
            get => _textForAnalysis;
            set => SetProperty(ref _textForAnalysis, value);
        }

        public ICommand AnalyseTextCommand { get; }

        public string SentimentAnalysisResult
        {
            get => _sentimentAnalysisResult;
            set => SetProperty(ref _sentimentAnalysisResult, value);
        }

        public Color SentimentResultColour
        {
            get => _sentimentResultColour;
            set => SetProperty(ref _sentimentResultColour, value);
        }

        async Task AnalyseText(string textForAnalysis)
        {
            TextForAnalysis = "";
            var textAnalyser = new TextAnalyser();
            switch (await textAnalyser.AnalyseSentiment(textForAnalysis))
            {
                case "Unknown":
                    SentimentResultColour = Color.Blue;
                    SentimentAnalysisResult = "😕 Hmmm I can't seem to tell the sentiment!";
                    break;
                case "Normal":
                    SentimentAnalysisResult = "😐 This text seems neutral to me!";
                    break;
                case "Positive":
                    SentimentResultColour = Color.Green;
                    SentimentAnalysisResult = "😁 Yay! This text seems happy!";
                    break;
                case "Negative":
                    SentimentResultColour = Color.Red;
                    SentimentAnalysisResult = "😥 This text appears to be not happy";
                    break;
            }
        }

    }
}
