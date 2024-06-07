using
/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-windows10.0.19041.0)'
Before:
using System.Threading.Tasks;
using CommunityToolkit.Maui.Core.Extensions;
using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView;
using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView.FieldsView;
using Newtonsoft.Json;
After:
using System.Threading.Tasks;

using CommunityToolkit.Maui.Core.Extensions;

using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView;
using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView.FieldsView;

using Newtonsoft.Json;
*/

/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-maccatalyst)'
Before:
using System.Threading.Tasks;
using CommunityToolkit.Maui.Core.Extensions;
using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView;
using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView.FieldsView;
using Newtonsoft.Json;
After:
using System.Threading.Tasks;

using CommunityToolkit.Maui.Core.Extensions;

using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView;
using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView.FieldsView;

using Newtonsoft.Json;
*/

/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-ios)'
Before:
using System.Threading.Tasks;
using CommunityToolkit.Maui.Core.Extensions;
using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView;
using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView.FieldsView;
using Newtonsoft.Json;
After:
using System.Threading.Tasks;

using CommunityToolkit.Maui.Core.Extensions;

using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView;
using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView.FieldsView;

using Newtonsoft.Json;
*/
Newtonsoft.Json;

namespace DiscordWebhookRemoteApp.Services
{
    public static class SavedEmbedsService
    {
        public static List<SavedEmbedsItems> SavedEmbeds
        {
            get
            {
                return JsonConvert.DeserializeObject<List<SavedEmbedsItems>>(
                    Preferences.Get("SavedEmbeds", "[]")
                );
            }
            set
            {
                string json = JsonConvert.SerializeObject(value);
                Preferences.Set("SavedEmbeds", json);
            }
        }
    }

    public class SavedEmbedsItems
    {
        public required int ID { get; set; }
        public required string Title { get; set; }
        public string? AuthorIcon { get; set; }
        public string? AuthorName { get; set; }
        public string? AuthorUrl { get; set; }
        public string? BodyTitle { get; set; }
        public string? BodyContent { get; set; }
        public string? BodyUrl { get; set; }
        public Color? BodyColor { get; set; }
        public List<SavedFieldsItems>? Fields { get; set; }
        public string? ImagesImageUrl { get; set; }
        public string? ImagesThumbnailUrl { get; set; }
        public string? FooterIcon { get; set; }
        public string? FooterTitle { get; set; }
        public bool FooterTimestamp { get; set; }
        public required bool IsEmpty { get; set; }
    }

    public class SavedFieldsItems
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public bool InLine { get; set; }
        public required bool IsEmpty { get; set; }
    }
}
