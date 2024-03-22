namespace DiscordWebhookRemoteApp.Components.Partials.CustomItems;

public class CustomButton : Button
{
    public CustomButton()
        : base()
    {
        Pressed += async (s, e) =>
        {
            this.CancelAnimations();
            await this.ScaleTo(0.95, 50, Easing.CubicOut);
            await this.FadeTo(0.8, 50, Easing.CubicOut);
        };

        Released += async (s, e) =>
        {
            await this.ScaleTo(1, 50, Easing.CubicIn);
            await this.FadeTo(1, 50, Easing.CubicIn);
        };
    }
}
