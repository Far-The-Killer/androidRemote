using System;
using Android.App;
using Android.Bluetooth.LE;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Remote
{
    [Activity(Label = "Remote", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button playBtn;
        bool paused;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            playBtn = FindViewById<Button>(Resource.Id.playBtn);
            playBtn.Click += SendPlay;
        }

        public void SendPlay(Object sender ,EventArgs e)
        {
            paused = !paused;

            if(paused)
            {
                playBtn.Background = GetDrawable(Resource.Drawable.pauseBtn_Select);
                playBtn.Rotation = 0.0f;
            }
            else
            {
                playBtn.Background = GetDrawable(Resource.Drawable.playBtn_Select);
                playBtn.Rotation = 90.0f;
            }
        }
    }
}

