using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SGSApp.Services
{
    public class DialogService
    {
        public async Task ShowMessage(string title, string message)
        {
            await Application.Current.MainPage.DisplayAlert(
                title,
                message,
                "Aceptar");
        }

        public async Task<bool> ShowConfirm(string title, string message)
        {
            return await Application.Current.MainPage.DisplayAlert(
                title,
                message,
                "Si",
                "No");
        }

        public async Task<string> ShowImageOptions()
        {
            return await Application.Current.MainPage.DisplayActionSheet(
                "Where do you take the image?",
                "Cancel",
                null,
                "From Gallery",
                "From Camera");
        }

        public async Task ShowMessageN(string message,
            string title,
            string buttonText,
            Action afterHideCallback)
        {
            await Application.Current.MainPage.DisplayAlert(
                title,
                message,
                buttonText);

            //afterHideCallback?.Invoke();
        }
    }
}