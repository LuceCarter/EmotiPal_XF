using EmotiPal.Helpers;
using EmotiPal.Views;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Microsoft.WindowsAzure.Storage;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Plugin.Media.Abstractions;
using Plugin.Media;

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
            faceClient = new FaceClient(new ApiKeyServiceClientCredentials(APIKeys.FaceApiKey))
            {
                Endpoint = APIKeys.BaseUrl
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
                var photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    PhotoSize = PhotoSize.Small,
                    DefaultCamera = CameraDevice.Front
                });
             

                if (photo != null)
                {
                    IsBusy = true;

                    ImageForAnalysis = (StreamImageSource)ImageSource.FromStream(() => photo.GetStream());
                    await ProcessFacesFromImage(photo);                                 
                                                 
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        private async Task<string> UploadImageToBlobStorage(string imagePath)
        {
            var account = CloudStorageAccount.Parse(APIKeys.BlobStorageConnectionString);
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference("sentiment-container");
            await container.CreateIfNotExistsAsync();
            var name = DateTime.UtcNow.Date.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss");

            var blockBlob = container.GetBlockBlobReference($"{name}.png");

            var url = "";

            try
            {
                await blockBlob.UploadFromFileAsync(imagePath);

                url = blockBlob.Uri.OriginalString;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return url;
        }

        private async Task PickPhotoAsync()
        {
            try
            {
                var photo = await CrossMedia.Current.PickPhotoAsync();
                //var photo = await MediaPicker.PickPhotoAsync();

                if (photo != null)
                {
                    IsBusy = true;
                    
                    ImageForAnalysis = (StreamImageSource)ImageSource.FromStream(() => photo.GetStream());
                    await ProcessFacesFromImage(photo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PickPhotoAsync THREW: {ex.Message}");
            }
        }

        private async Task ProcessFacesFromImage(MediaFile photo)
        {
            using(var stream = photo.GetStreamWithImageRotatedForExternalStorage())
            {
                IList<DetectedFace> faces;

                List<FaceAttributeType> faceAttributeTypes = new List<FaceAttributeType> { FaceAttributeType.Emotion };

                faces = await faceClient.Face.DetectWithStreamAsync(stream, true, true, faceAttributeTypes);

                if (faces.Count > 0)
                {
                    await UploadImageToBlobStorage(photo.Path);
                    var photoForDisplay = (StreamImageSource)ImageSource.FromStream(() => stream);
                    await Application.Current.MainPage.Navigation.PushAsync(new AnalyseImageResultPage { BindingContext = new AnalyseImageResultPageViewModel(photo.Path, faces) });
                }
            }
        }
    }
}
