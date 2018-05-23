using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using CarouselView.FormsPlugin.Android;
using Android.Content;

namespace Rote.Droid
{
    [Activity(Label = "Rote", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Window.SetStatusBarColor(new Android.Graphics.Color(53, 199, 240));
            CarouselViewRenderer.Init();
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            Rg.Plugins.Popup.Popup.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            if (Intent.HasExtra("ID"))
            {
                var ID = Intent.GetIntExtra("ID", -1);
                var Game = Intent.GetIntExtra("Game", -1);
                LoadApplication(new App(ID, Game));

            }
            else
            {
                LoadApplication(new App());
            }

            
            
            
        }

        protected override void OnNewIntent(Intent intent)
        {
            if (intent.HasExtra("ID"))
            {
                var ID = intent.GetIntExtra("ID", -1);
                var Game = intent.GetIntExtra("Game", -1);
                LoadApplication(new App(ID, Game));

            }
            else
            {
                LoadApplication(new App());
            }
        }



        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // Do something if there are some pages in the `PopupStack`
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }
    }

}

