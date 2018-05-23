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
using Rote.Controls;
using Rote.Droid.Resources.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(CustomSlider), typeof(CustomSliderRenderer))]
namespace Rote.Droid.Resources.CustomRenderer
{
    public class CustomSliderRenderer : SliderRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
        {
            base.OnElementChanged(e);
            
            if(e.NewElement != null)
            {
                Control.ProgressDrawable.SetColorFilter(new PorterDuffColorFilter(Xamarin.Forms.Color.FromHex("#35c7f0").ToAndroid(), PorterDuff.Mode.SrcIn));
                Control.Thumb.SetColorFilter(Xamarin.Forms.Color.FromHex("#358cf0").ToAndroid(), PorterDuff.Mode.SrcIn);
            }
        }
    }
}