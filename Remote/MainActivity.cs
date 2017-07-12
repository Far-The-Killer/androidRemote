using System;
using Android.App;
using Android.Bluetooth;
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
        const int START_BLT_ACTION_NUM = 189;

        //BluetoothManager blManager;
        Button playBtn;
        Button forwardSkipBtn;
        Button backSkipBtn;
        bool paused;
        MPCSocketInterface mpcInterface;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //if (!PackageManager.HasSystemFeature("android.hardware.bluetooth_le"))
            //{
            //    Toast.MakeText(this, Resource.String.no_ble, ToastLength.Long).Show();         
            //}
            //else
            //{
            //    SetupBluetoothLE();
            //}

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            mpcInterface = new MPCSocketInterface("192.168.0.40", 13579);
            playBtn = FindViewById<Button>(Resource.Id.playBtn);
            forwardSkipBtn = FindViewById<Button>(Resource.Id.fSkipBtn);
            backSkipBtn = FindViewById<Button>(Resource.Id.bSkipBtn);
            playBtn.Click += SendPlay;
            forwardSkipBtn.Click += SendSkipForward;
            backSkipBtn.Click += SendSkipBackward;
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            //exit application if user denies bluetooth connection
            //if(requestCode == START_BLT_ACTION_NUM)
            //{
            //    System.Diagnostics.Debug.WriteLine(resultCode);
            //    if (resultCode == Result.Canceled) Finish();
            //}
        }

        //private void SetupBluetoothLE()
        //{
        //    blManager = (BluetoothManager) GetSystemService(BluetoothService);

        //    if (blManager.Adapter == null || !blManager.Adapter.IsEnabled)
        //    {
        //        //launch prompt for user to connect to bluetooth
        //        StartActivityForResult(new Intent(BluetoothAdapter.ActionRequestEnable), START_BLT_ACTION_NUM);
        //    }
        //}

        public void SendPlay(Object sender ,EventArgs e)
        {
            paused = !paused;
            if(paused)
            {
                playBtn.Background = GetDrawable(Resource.Drawable.arrowBtn_Select);
                playBtn.Rotation = 90.0f;
            }
            else
            {
                playBtn.Background = GetDrawable(Resource.Drawable.pauseBtn_Select);
                playBtn.Rotation = 0.0f;
            }
            mpcInterface.SendCommand("889");
        }

        public void SendSkipForward(Object sender, EventArgs e)
        {
            mpcInterface.SendCommand("922");
        }

        public void SendSkipBackward(Object sender, EventArgs e)
        {
            mpcInterface.SendCommand("921");
        }
    }
}

