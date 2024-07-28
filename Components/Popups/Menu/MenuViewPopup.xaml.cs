using System.Text;

using CommunityToolkit.Maui.Views;

using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.SavedWebhooksView;
using DiscordWebhookRemoteApp.Components.Popups.Common;
using DiscordWebhookRemoteApp.Components.Popups.Embed;
using DiscordWebhookRemoteApp.Helpers;

using Newtonsoft.Json;

namespace DiscordWebhookRemoteApp.Components.Popups.Menu;

public partial class MessagePreviewPopupViews : Popup
{
    private readonly SavedWebhooksView savedWebhooksView;

    public MessagePreviewPopupViews(SavedWebhooksView? swView = null)
    {
        InitializeComponent();
        savedWebhooksView = swView;
    }

    private void Close_Tapped(object sender, TappedEventArgs e)
    {
        Close();
    }

    private void btnSavedImages_Clicked(object sender, EventArgs e)
    {
        ApplicationService.ShowPopup(new SavedImagesViewPopup("View"));
    }

    private void btnSavedEmbeds_Clicked(object sender, EventArgs e)
    {
        ApplicationService.ShowPopup(new SavedEmbedsViewPopup("View"));
    }

    private async void btnImportWebhooks_Clicked(object sender, EventArgs e)
    {
        this.Close();
        var webhooksData = await App.Current.MainPage.DisplayPromptAsync(
            title: "Import Webhooks",
            message: "Paste your webhooks data here",
            placeholder: "Webhooks Data"
        );
        if (webhooksData != null)
        {
            await WebhookService.ImportSavedWebhoks(webhooksData);
            await savedWebhooksView.RefreshList();
        }
    }

    private async void btnSendLogs_Clicked(object sender, EventArgs e)
    {
        btnSendLogs.IsEnabled = false;
        var resQ = await ApplicationService.ShowCustomAlertAsync(
            "Send Logs",
            "Are you sure you want to send logs to developer?",
            "Yes",
            "No"
        );
        if (resQ)
        {
            ApplicationService.ShowLoadingView();
            var logs = LoggingService.LogManager;
            if (logs is not null)
            {
                if (logs.Count > 0)
                {
                    var resDeviceDetailsPost = await createDeviceLogProfile();
                    if (resDeviceDetailsPost)
                    {
                        var client = new HttpClient();
                        var deviceDetails = new LoggingService.DeviceDetails();

                        var logsJson = JsonConvert.SerializeObject(logs);
                        var logsContent = new StringContent(
                            logsJson,
                            Encoding.UTF8,
                            "application/json"
                        );
                        var resLogs = await client.PostAsync(
                            $"{EncryptionHelper.Decrypt(StaticPropertiesService.LoggingApiUrl)}{deviceDetails.DeviceGuid}/Logs/.json?auth={EncryptionHelper.Decrypt(StaticPropertiesService.LoggingApiKey)}",
                            logsContent
                        );
                        if (resLogs.IsSuccessStatusCode)
                        {
                            ApplicationService.ShowCustomAlert(
                                "Success",
                                "Logs sent successfully",
                                "Ok"
                            );
                            LoggingService.LogManager = new List<LoggingService.LogMessageModel>();
                            ApplicationService.HideLoadingView();
                            Close();
                            return;
                        }
                        else
                            ApplicationService.ShowCustomAlert(
                                "Error",
                                "Failed to send logs, Please try later.",
                                "Ok"
                            );
                    }
                    else
                        ApplicationService.ShowCustomAlert(
                            "Error",
                            "Failed to send logs, Please try later.",
                            "Ok"
                        );
                }
                else
                    ApplicationService.ShowCustomAlert("Error", "No logs found to send.", "Ok");
            }
            else
                ApplicationService.ShowCustomAlert("Error", "No logs found to send.", "Ok");
        }
        ApplicationService.HideLoadingView();
        btnSendLogs.IsEnabled = true;
    }

    private async Task<bool> createDeviceLogProfile()
    {
        var client = new HttpClient();

        var deviceDetails = new LoggingService.DeviceDetails();

        var resDeviceDetailsGet = await client.GetAsync(
            $"{EncryptionHelper.Decrypt(StaticPropertiesService.LoggingApiUrl)}{deviceDetails.DeviceGuid}/.json?auth={EncryptionHelper.Decrypt(StaticPropertiesService.LoggingApiKey)}"
        );
        var res = await resDeviceDetailsGet.Content.ReadAsStringAsync();

        if (res is null || res is "null")
        {
            var deviceDetailsJson = JsonConvert.SerializeObject(deviceDetails);

            var deviceContent = new StringContent(
                deviceDetailsJson,
                Encoding.UTF8,
                "application/json"
            );
            var resDeviceDetailsPost = await client.PutAsync(
                $"{EncryptionHelper.Decrypt(StaticPropertiesService.LoggingApiUrl)}{deviceDetails.DeviceGuid}/.json?auth={EncryptionHelper.Decrypt(StaticPropertiesService.LoggingApiKey)}",
                deviceContent
            );

            return resDeviceDetailsPost.IsSuccessStatusCode;
        }
        else
            return true;
    }

    private async void btnExportWebhooks_Clicked(object sender, EventArgs e)
    {
        var webhooks = WebhookService.ExportSavedWebhoks();
        await Clipboard.SetTextAsync(webhooks);
        ApplicationService.ShowCustomAlert(
            "Success.",
            "Webhooks copied to clipboard. \nMake sure backup webhook data on somewhere.",
            "Ok"
        );
    }
}
