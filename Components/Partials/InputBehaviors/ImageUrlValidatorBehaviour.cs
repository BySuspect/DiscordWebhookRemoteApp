using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DiscordWebhookRemoteApp.Components.Partials.InputBehaviors
{
    public class ImageUrlValidatorBehaviour : Behavior<Entry>
    {
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
                entry.Text = string.Empty;
        }

        private void Bindable_Completed(object sender, EventArgs e)
        {
            var entry = sender as Entry;
            if (!regexMatchs(entry.Text))
                entry.Text = string.Empty;
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            if (!string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                if (regexMatchs(e.NewTextValue))
                {
                    entry.TextColor = Colors.White;
                }
                else
                    entry.TextColor = Colors.Red;
            }
        }

        private bool regexMatchs(string text)
        {
            string pattern = @"^(http?|https?)://[^\s/$.?#].[^\s]*\.(?:png|jpg|gif)(?:\?.*)?$";
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
