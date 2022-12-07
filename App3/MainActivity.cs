using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using CN.Com.Aratek.FP;
using Android.Widget;


namespace App3
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private FingerprintScanner mFingerprintScanner;
        private const string FP_DB_PATH = "/sdcard/fp.db";
        private const int MSG_UPDATE_FIRMWARE_VERSION = 100;
        private const int MSG_UPDATE_SERIAL_NUMBER = 101;
        private const int MSG_UPDATE_FINGERPRINT = 102;
        private const int MSG_UPDATE_TIME_INFORMATIONS = 103;
        private const int MSG_ENABLE_BUTTONS = 104;
        private TextView mSerialNumber;
        private TextView mFirmwareVersion;
        private Spinner mSpLfd;
        private ImageView mFingerprintImage;
        private TextView mCaptureTime;
        private TextView mExtractTime;
        private TextView mGeneralizeTime;
        private TextView mVerifyTime;
        private Button mBtnEnroll;
        private Button mBtnVerify;
        private Button mBtnIdentify;
        private Button mBtnClear;
        private Button mBtnShow;
        private int mId;
        private int mLfdLevel = FingerprintScanner.LfdLevelOff;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            mFingerprintScanner = FingerprintScanner.GetInstance(this);
            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            //mSerialNumber = FindViewById(Resource.Id.serial_number);
            //mFirmwareVersion = FindViewById(Resource.Id.firmware_version);
            //mSpLfd = FindViewById(Resource.Id.lfd_level);
            //mSpLfd.setOnItemSelectedListener(this);
            //mCaptureTime = FindViewById(Resource.Id.captureTime);
            //mExtractTime = FindViewById(Resource.Id.extractTime);
            //mGeneralizeTime = FindViewById(Resource.Id.generalizeTime);
            //mVerifyTime = FindViewById(Resource.Id.verifyTime);
            //mFingerprintImage = FindViewById(Resource.Id.fingerimage);

            //mBtnEnroll = FindViewById(Resource.Id.bt_enroll);
            //mBtnVerify = FindViewById(Resource.Id.bt_verify);
            //mBtnIdentify = FindViewById(Resource.Id.bt_identify);
            //mBtnClear = FindViewById(Resource.Id.bt_clear);
            //mBtnShow = FindViewById(Resource.Id.bt_show);


            //FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            //fab.Click += FabOnClick;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}
