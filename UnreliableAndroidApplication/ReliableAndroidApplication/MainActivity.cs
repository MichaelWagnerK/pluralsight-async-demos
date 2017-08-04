using Android.App;
using Android.Widget;
using Android.OS;
using System.Net;
using System;
using System.Threading;

namespace ReliableAndroidApplication
{
    [Activity(Label = "ReliableAndroidApplication", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private int count = 1;
        private ProgressDialog progress;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            progress = new ProgressDialog(this);

            SetContentView(Resource.Layout.Main);

            var counterButton = FindViewById<Button>(Resource.Id.CounterButton);
            var rssButton = FindViewById<Button>(Resource.Id.RssButton);

            counterButton.Click += delegate {
                var counterTextView = FindViewById<TextView>(Resource.Id.CounterText);
                counterTextView.Text = $"Counter {count++}";
            };

            rssButton.Click += delegate {

                progress.SetMessage("Please wait, downloading RSS...");
                progress.SetCancelable(false);
                progress.Show();

                var client = new WebClient();
                client.DownloadStringAsync(new Uri("http://www.filipekberg.se/rss/"));
                client.DownloadStringCompleted += Client_DownloadStringCompleted;
            };
        }

        private void Client_DownloadStringCompleted(object sender, 
            DownloadStringCompletedEventArgs e)
        {
            var rssTextView = FindViewById<TextView>(Resource.Id.Rss);

            rssTextView.Text = e.Result;

            progress.Hide();
        }
    }
}

