namespace DiscordWebhookRemoteApp.Components.Partials.Views.MessagePreviewPopupViews.EmbedView;

public partial class EmbedView : ContentView
{
    public EmbedView()
    {
        InitializeComponent();
    }

    public void setupEmbed(Discord.Embed embed)
    {
        bvEmbedColor.Color = Color.Parse(
            (embed.Color.HasValue) ? embed.Color.Value.ToString() : "#000000"
        );

        if (embed.Author.HasValue)
        {
            if (
                !string.IsNullOrWhiteSpace(embed.Author.Value.IconUrl)
                || !string.IsNullOrWhiteSpace(embed.Author.Value.Name)
            )
                authorView.IsVisible = true;

            if (embed.Author.HasValue && !string.IsNullOrWhiteSpace(embed.Author.Value.IconUrl))
                authorView.Avatar = embed.Author.Value.IconUrl;
            if (embed.Author.HasValue && !string.IsNullOrWhiteSpace(embed.Author.Value.Name))
                authorView.AuthorText = embed.Author.Value.Name;
        }
        if (
            !string.IsNullOrWhiteSpace(embed.Title) || !string.IsNullOrWhiteSpace(embed.Description)
        )
            bodyView.IsVisible = true;

        if (!string.IsNullOrWhiteSpace(embed.Title))
            bodyView.Title = embed.Title;
        if (!string.IsNullOrWhiteSpace(embed.Description))
            bodyView.Description = embed.Description;

        if (embed.Fields.Length > 0)
            fieldsView.IsVisible = true;

        if (embed.Footer.HasValue)
        {
            if (
                !string.IsNullOrWhiteSpace(embed.Footer.Value.IconUrl)
                || !string.IsNullOrWhiteSpace(embed.Footer.Value.Text)
            )
                footerView.IsVisible = true;

            if (embed.Footer.HasValue && !string.IsNullOrWhiteSpace(embed.Footer.Value.IconUrl))
                footerView.FooterIcon = embed.Footer.Value.IconUrl;
            if (embed.Footer.HasValue && !string.IsNullOrWhiteSpace(embed.Footer.Value.Text))
                footerView.FooterText = embed.Footer.Value.Text;

            if (embed.Timestamp.HasValue)
                footerView.Timestamp = embed.Timestamp.Value.ToString();
        }
    }
}
