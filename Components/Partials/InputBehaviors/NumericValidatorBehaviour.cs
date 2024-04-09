using System.Text.RegularExpressions;

namespace DiscordWebhookRemoteApp.Components.Partials.InputBehaviors
{
    public class NumericValidatorBehaviour : Behavior<Entry>
    {
        public int? MinimumValue { get; set; }
        public int? MaximumValue { get; set; }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);

            bindable.TextChanged += Bindable_TextChanged;
            bindable.Completed += Bindable_Completed;
            bindable.Unfocused += Bindable_Unfocused;
        }

        private void Bindable_Unfocused(object sender, FocusEventArgs e)
        {
            var entry = sender as Entry;
            if (!regexMatchs(entry.Text))
                entry.Text = MinimumValue.ToString();
        }

        private void Bindable_Completed(object sender, EventArgs e)
        {
            var entry = sender as Entry;
            if (!regexMatchs(entry.Text))
                entry.Text = MinimumValue.ToString();
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            if (!string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                if (e.NewTextValue.StartsWith("0") && e.NewTextValue.Length > 1)
                {
                    entry.Text = e.NewTextValue.TrimStart('0');
                }
                if (MinimumValue <= 0)
                {
                    if (e.NewTextValue.Contains('-'))
                    {
                        entry.Text = e.NewTextValue.Replace("-", string.Empty);
                        return;
                    }
                }
                if (regexMatchs(e.NewTextValue))
                {
                    entry.TextColor = AppThemeColors.TextColor;
                }
                else
                {
                    entry.TextColor = Colors.Red;
                    return;
                }
                if ((int.Parse(e.NewTextValue) > MaximumValue) && MaximumValue != null)
                {
                    entry.Text = MaximumValue.ToString();
                }
                else if ((int.Parse(e.NewTextValue) < MinimumValue) && MinimumValue != null)
                {
                    entry.Text = MinimumValue.ToString();
                }
            }
        }

        private bool regexMatchs(string text)
        {
            string pattern = @"^-?\d+(\.\d+)?$";
            return Regex.IsMatch(text, pattern, RegexOptions.IgnoreCase);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= Bindable_TextChanged;
            bindable.Completed -= Bindable_Completed;
            bindable.Unfocused -= Bindable_Unfocused;
        }
    }
}
