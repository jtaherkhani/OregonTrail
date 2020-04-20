using CurrieTechnologies.Razor.SweetAlert2;

namespace OregonTrail.UI.Client.Helpers
{
    /// <summary>
    /// Static helper class to assist in the creation of sweet alert messages.
    /// </summary>
    public static class AlertHelper
    {
        private const decimal ToastDisplayTime = 1600.0m;

        public static SweetAlertOptions ToastSuccess(string titleMessage)
        {
            return new SweetAlertOptions()
            {
                Position = SweetAlertPosition.TopEnd,
                Icon = SweetAlertIcon.Success,
                Title = titleMessage,
                ShowConfirmButton = false,
                Timer = ToastDisplayTime
            };
        }

        public static SweetAlertOptions ConfirmationWarning(string titleWarning)
        {
            return new SweetAlertOptions()
            {
                Title = titleWarning,
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true,
            };
        }
    }
}
