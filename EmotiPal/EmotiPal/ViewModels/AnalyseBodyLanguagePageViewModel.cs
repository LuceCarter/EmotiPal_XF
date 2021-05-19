using EmotiPal.Helpers;
using EmotiPal.Views;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EmotiPal.ViewModels
{
    public class AnalyseBodyLanguagePageViewModel : BaseViewModel
    {
        private bool isBusy = false;
        private StreamImageSource imageForAnalysis;
        private FaceClient faceClient;

        public AsyncCommand TakePhotoCommand { get; }
        public AsyncCommand PickPhotoCommand { get; }

        public AnalyseBodyLanguagePageViewModel()
        {
            TakePhotoCommand = new AsyncCommand(TakePhotoAsync);
            PickPhotoCommand = new AsyncCommand(PickPhotoAsync);
            faceClient = new FaceClient(new ApiKeyServiceClientCredentials(AzureKeys.FaceApiKey))
            {
                Endpoint = AzureKeys.BaseUrl
            };
        }
        public string PhotoPath { get; set; }

        public StreamImageSource ImageForAnalysis
        {
            get => imageForAnalysis;
            set => SetProperty(ref imageForAnalysis, value);
        }

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        private async Task TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();

                if (photo != null)
                {
                    IsBusy = true;
                    var stream = await photo.OpenReadAsync();

                    ImageForAnalysis = (StreamImageSource)ImageSource.FromStream(() => stream);

                    IList<DetectedFace> faces;

                    var faceAttributeTypes = new List<FaceAttributeType> { FaceAttributeType.Emotion };
                    faces = await faceClient.Face.DetectWithStreamAsync(stream, true, true, faceAttributeTypes);

                    if(faces.Count > 0)
                    {
                        var photoForDisplay = (StreamImageSource)ImageSource.FromStream(() => stream);
                        await Application.Current.MainPage.Navigation.PushAsync(new AnalyseImageResultPage { BindingContext = new AnalyseImageResultPageViewModel(photoForDisplay, faces) });
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        private async Task PickPhotoAsync()
        {
            try
            {

                var photo = await MediaPicker.PickPhotoAsync();

                if (photo != null)
                {
                    IsBusy = true;
                    var stream = await photo.OpenReadAsync();
                    ImageForAnalysis = (StreamImageSource)ImageSource.FromStream(() => stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PickPhotoAsync THREW: {ex.Message}");
            }
        }
    }
}
