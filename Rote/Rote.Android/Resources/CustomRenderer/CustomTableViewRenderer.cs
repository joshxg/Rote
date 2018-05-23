using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rote.Controls;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Rote.Droid.Resources.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomTableView), typeof(CustomTableViewRenderer))]
namespace Rote.Droid.Resources.CustomRenderer
{
    public class CustomTableViewRenderer : TableViewRenderer
    {
        protected override TableViewModelRenderer GetModelRenderer(Android.Widget.ListView listView, TableView view)
        {
            return new CustomTableViewModelRenderer(Context, listView, view);
        }
    }
}