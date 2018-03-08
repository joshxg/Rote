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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Frame), typeof(Rote.Droid.CustomButtonRenderer))]
namespace Rote.Droid
{

    public class CustomButtonRenderer : FrameRenderer
    {

        public CustomButtonRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                ViewGroup.SetBackgroundResource(Resource.Drawable.shadows);

            }
        }
    }
}