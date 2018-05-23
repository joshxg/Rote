using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Rote.Droid.Resources.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Rote.Controls;

[assembly: ExportRenderer(typeof(CustomSwitch), typeof(CustomSwitchRenderer))]
namespace Rote.Droid.Resources.CustomRenderer
{
    public class CustomSwitchRenderer : SwitchRenderer
    {

        protected override void Dispose(bool disposing)
        {
            this.Control.CheckedChange -= this.OnCheckedChange;
            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Switch> e)
        {
            base.OnElementChanged(e);
            Element.IsToggled = Control.Checked;

            if (this.Control != null)
            {
                if (this.Control.Checked)
                {
                    this.Control.ThumbDrawable.SetColorFilter(Xamarin.Forms.Color.FromHex("#358cf0").ToAndroid(), PorterDuff.Mode.SrcAtop);
                    this.Control.TrackDrawable.SetColorFilter(Xamarin.Forms.Color.FromHex("#35c7f0").ToAndroid(), PorterDuff.Mode.SrcAtop);
                }

                else
                {
                    this.Control.ThumbDrawable.SetColorFilter(Xamarin.Forms.Color.Gray.ToAndroid(), PorterDuff.Mode.SrcAtop);
                    this.Control.TrackDrawable.SetColorFilter(Xamarin.Forms.Color.Gray.ToAndroid(), PorterDuff.Mode.SrcAtop);
                }

                this.Control.CheckedChange += this.OnCheckedChange;
            }
        }

        private void OnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            Element.IsToggled = Control.Checked;

            if (this.Control.Checked)
            {
                this.Control.ThumbDrawable.SetColorFilter(Xamarin.Forms.Color.FromHex("#358cf0").ToAndroid(), PorterDuff.Mode.SrcAtop);
                this.Control.TrackDrawable.SetColorFilter(Xamarin.Forms.Color.FromHex("#35c7f0").ToAndroid(), PorterDuff.Mode.SrcAtop);
            }

            else
            {
                this.Control.ThumbDrawable.SetColorFilter(Xamarin.Forms.Color.Gray.ToAndroid(), PorterDuff.Mode.SrcAtop);
                this.Control.TrackDrawable.SetColorFilter(Xamarin.Forms.Color.Gray.ToAndroid(), PorterDuff.Mode.SrcAtop);
            }

            
        }
    }
}