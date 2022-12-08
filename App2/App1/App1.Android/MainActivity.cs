using System;

using Xamarin.Forms.Core;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using CN.Com.Aratek;
using CN.Com.Aratek.FP;
using CN.Com.Aratek.Qrc;
using Java.Lang;
using Android.Widget;
using CN.Com.Aratek.Dev;
using System.Reflection;
using Android.Content.Res;
using System.IO;

namespace App1.Droid
{
    [Activity(Label = "App1", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        private FingerprintScanner mFingerprintScanner;
        private string FP_DB_PATH = "/sdcard/fp.db";
        private int MSG_UPDATE_FIRMWARE_VERSION = 100;
        private int  MSG_UPDATE_SERIAL_NUMBER = 101;
        private int  MSG_UPDATE_FINGERPRINT = 102;
        private int  MSG_UPDATE_TIME_INFORMATIONS = 103;
        private int  MSG_ENABLE_BUTTONS = 104;
        private CodeScanner mCodeScanner;
        private ImageView mFingerprintImage;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
          //  mFingerprintImage = FindViewById(Resource.Id.fingerimage);
            try
            {
                JavaSystem.LoadLibrary("xml2");
                JavaSystem.LoadLibrary("usb-1.0");
                JavaSystem.LoadLibrary("WSQ_library_android");
                JavaSystem.LoadLibrary("opencv_java3");
                JavaSystem.LoadLibrary("ncnn");
                JavaSystem.LoadLibrary("ftrScanAPI");
                JavaSystem.LoadLibrary("AraBMApiQrc");
                JavaSystem.LoadLibrary("AraBMApiFpAlgorithm");
                JavaSystem.LoadLibrary("AraBMApiFp");
                JavaSystem.LoadLibrary("AraBMApiDev");
                JavaSystem.LoadLibrary("AraBione");
                string content;
                AssetManager assets = this.Assets;
                StreamReader sr = new StreamReader(assets.Open("terminal.xml"));
                var bytes = default(byte[]);
                using (var memstream = new MemoryStream())
                {
                    sr.BaseStream.CopyTo(memstream);
                    bytes = memstream.ToArray();
                }

                Terminal.LoadSettings(bytes);
                //var assembly = IntrospectionExtensions.GetTypeInfo(typeof()).Assembly;
                //Stream stream = assembly.GetManifestResourceStream("WorkingWithFiles.LibTextResource.txt");
                //string text = "";
                //Terminal.LoadSettings()
                
                //JavaSystem.LoadLibrary("glib-2.0");
                //JavaSystem.LoadLibrary("gthread-2.0");
                //JavaSystem.LoadLibrary("fluidsynth");
                //JavaSystem.LoadLibrary("sdl_mixer");
                //JavaSystem.LoadLibrary("initmixer");
            }
            catch (UnsatisfiedLinkError e)
            {
                //return e.Message;
            }
            var btn = view.FindViewById<Button>(Resource.Id.buttonId);
            btn.Tag = position;
            btn.SetOnClickListener(this);
            LoadApplication(new App());
            App.Current.MainPage.DisplayAlert(error.ToString(),"bad" , "OK");
            ImageView translatedPhoneWord = (ImageView)FindViewById(877998989);
            TextView textlop = (TextView)FindViewById(Resource.Id.text);
            //textlop.Text = "Texto Cambiado";
        }
       
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}