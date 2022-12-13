using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using CN.Com.Aratek.Dev;
using CN.Com.Aratek.FP;
using Java.Lang;
using Android.Content.Res;
using System.IO;
using System.Drawing;
using Android.Graphics;
using Android.Widget;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;
using Android.Preferences;
using System.Text;
using Java.Security;
using Android1serialport1api;

namespace XAMARINTEST
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private FingerprintScanner mFingerprintScanner;
        public SerialPortFinder mSerialPortFinder = new SerialPortFinder();
        private SerialPort mSerialPort = null;
        public System.Boolean isset = false;
        private string FP_DB_PATH = "/sdcard/fp.db";
        ImageView ImageView1;
       
        protected System.IO.Stream mOutputStream;
        private System.IO.Stream mInputStream;
        protected Application mApplication;
        //private ReadThread mReadThread;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            JavaSystem.LoadLibrary("xml2");
            //JavaSystem.LoadLibrary("serial_port");
            //JavaSystem.LoadLibrary("android_serial_port");
            
            AssetManager assets = this.Assets;
            StreamReader sr = new StreamReader(assets.Open("terminal.xml"));
            ImageView1 = (ImageView)FindViewById(Resource.Id.imageView1);
           

            var bytes = default(byte[]);
            using (var memstream = new MemoryStream())
            {
                sr.BaseStream.CopyTo(memstream);
                bytes = memstream.ToArray();
            }

            Terminal.LoadSettings(bytes);
            mFingerprintScanner = FingerprintScanner.GetInstance(this);
            var btn = FindViewById<Button>(Resource.Id.button1);
            //btn.Tag = position;
            btn.Click += btnOnClick;

            var btn2 = FindViewById<Button>(Resource.Id.button2);
            //btn.Tag = position;
            btn2.Click += btnOnClick2;

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;
        }

        private void AddPreferencesFromResource(Resource.Xml xml)
        {
            throw new NotImplementedException();
        }

        private void btnOnClick(object sender, EventArgs eventArgs)
        {
            mFingerprintScanner.PowerOn(); // ignore power on errors
            //mFingerprintScanner.
            var error = mFingerprintScanner.Open(); // => AQUI DA EL ERROR.

            if (error == FingerprintScanner.ResultOk)
            {


                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                Android.App.AlertDialog alert = dialog.Create();
                alert.SetTitle("EXITOSO POWER ON");
                alert.SetMessage(error.ToString());

                alert.Show();
                error = Bione.Initialize(this, FP_DB_PATH);
                mFingerprintScanner.Prepare();
                FingerprintImage fi;
                do
                {
                    //startTime = System.currentTimeMillis();
                    var res = mFingerprintScanner.Capture();
                    //captureTime = System.currentTimeMillis() - startTime;

                    fi = (FingerprintImage)res.Data;
                    if (fi != null)
                    {
                        break;
                    }

                    if (res.Error != FingerprintScanner.NoFinger)

                    {
                        break;
                    }
                } while (true);
                mFingerprintScanner.Finish();
                if (fi != null)
                {
                    updateFingerprintImage(fi);
                }
            }
            else
            {
                mFingerprintScanner.PowerOff();
                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                Android.App.AlertDialog alert = dialog.Create();
                alert.SetTitle("FALLIDO POWER ON");
                alert.SetMessage(error.ToString());
                alert.SetButton("GOOD", (c, ev) => {
                    // Ok button click task  
                });
                alert.Show();
            }
            error = mFingerprintScanner.Close();
            if (error == FingerprintScanner.ResultOk)
            {

            }
            else
            {

            }
            
            
        }
        private void btnOnClick2(object sender, EventArgs eventArgs)
        {
            JavaSystem.LoadLibrary("serial_port");
            mSerialPort = null;

            try
            {
                string path = "/dev/ttysWK1";
                int baudrate = 115200;

                /* Check parameters */
                if ((path.Length == 0) || (baudrate == -1))
                {
                    //throw new InvalidParameterException();
                    /*use default value.    Nirvana 0710*/
                    path = "/dev/ttysWK1";
                    baudrate = 115200;
                }

                /* Open the serial port */
                var odevice =  new Java.IO.File(path);
                mSerialPort = new SerialPort(odevice, baudrate, 0);
                mOutputStream = mSerialPort.OutputStream;
                mInputStream = mSerialPort.InputStream;
                string oPruebaImpresion = "Prueba impresion";
                byte[] bytes = Encoding.ASCII.GetBytes(oPruebaImpresion);
                mOutputStream.Write(bytes);
                

            }
            catch (SecurityException e)
            {
                
            }
            catch (IOException e)
            {
                
            }
            catch (InvalidParameterException e)
            {
                
            }


        }

        private Application getApplication()
        {
            throw new NotImplementedException();
        }
        
        private void updateFingerprintImage(FingerprintImage fi)
        {
            byte[] fpBmp = null;
            Android.Graphics.Bitmap bitmap;
            fpBmp = fi.Convert2Bmp();
            bitmap = BitmapFactory.DecodeByteArray(fpBmp, 0, fpBmp.Length);
            ImageView1.SetImageBitmap(bitmap);
            mFingerprintScanner.PowerOff(); // ignore power off
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
