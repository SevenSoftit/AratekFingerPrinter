using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using CN.Com.Aratek.FP;

namespace App2
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
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
            mFingerprintScanner = FingerprintScanner.GetInstance(this);
           g
            mSerialNumber = FindViewById(R.id.serial_number);
            mFirmwareVersion = FindViewById(R.id.firmware_version);
            mSpLfd = FindViewById(R.id.lfd_level);
            mSpLfd.setOnItemSelectedListener(this);
            mCaptureTime = FindViewById(R.id.captureTime);
            mExtractTime = FindViewById(R.id.extractTime);
            mGeneralizeTime = FindViewById(R.id.generalizeTime);
            mVerifyTime = FindViewById(R.id.verifyTime);
            mFingerprintImage = FindViewById(R.id.fingerimage);

            mBtnEnroll = FindViewById(R.id.bt_enroll);
            mBtnVerify = FindViewById(R.id.bt_verify);
            mBtnIdentify = FindViewById(R.id.bt_identify);
            mBtnClear = FindViewById(R.id.bt_clear);
            mBtnShow = FindViewById(R.id.bt_show);

            SetContentView(Resource.Layout.activity_main);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}