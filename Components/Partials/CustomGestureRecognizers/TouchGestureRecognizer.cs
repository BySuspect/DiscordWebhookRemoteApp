namespace DiscordWebhookRemoteApp.Components.Partials.CustomGestureRecognizers
{
    public class CustomGestureRecognizer
    {
        private readonly TimeSpan holdThreshold = TimeSpan.FromSeconds(1);
        private bool isHeld = false;
        private bool isClicked = false;
        private DateTime startTime;

        public event EventHandler ClickStarted;
        public event EventHandler ClickEnded;
        public event EventHandler ClickHeld;

        public async Task ProcessClick()
        {
            isClicked = true;
            startTime = DateTime.Now;

            OnClickStarted();

            await Task.Delay(holdThreshold);

            if (isClicked && !isHeld)
            {
                isHeld = true;
                OnClickHeld();
            }
        }

        public void EndClick()
        {
            isClicked = false;
            if (isHeld)
            {
                isHeld = false;
                OnClickEnded();
            }
        }

        protected virtual void OnClickStarted()
        {
            ClickStarted?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnClickEnded()
        {
            ClickEnded?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnClickHeld()
        {
            ClickHeld?.Invoke(this, EventArgs.Empty);
        }
    }
}
