namespace DiscordWebhookRemoteApp.Components.Partials.Views.MessagePreviewPopupViews.EmbedView;

public partial class EmbedView : ContentView
{
    public EmbedView()
    {
        InitializeComponent();
    }

    public void setupEmbed(Discord.Embed embed)
    {
        bvEmbedColor.Color = Color.Parse(embed.Color.Value.ToString());

        if (!embed.Author.HasValue || !embed.Author.HasValue)
            authorView.IsVisible = true;

        authorView.Avatar = embed.Author.Value.IconUrl;
        authorView.AuthorText = embed.Author.Value.Name;

        if (
            !string.IsNullOrWhiteSpace(embed.Title) || !string.IsNullOrWhiteSpace(embed.Description)
        )
            bodyView.IsVisible = true;

        bodyView.Title = embed.Title;
        bodyView.Description = embed.Description;

        if (embed.Fields.Length > 0)
            fieldsView.IsVisible = true;

        if (!embed.Footer.HasValue || !embed.Footer.HasValue)
            footerView.IsVisible = true;

        footerView.FooterIcon = embed.Footer.Value.IconUrl;
        footerView.FooterText = embed.Footer.Value.Text;

        if (embed.Timestamp.HasValue)
            footerView.Timestamp = embed.Timestamp.Value.ToString();
    }
}
