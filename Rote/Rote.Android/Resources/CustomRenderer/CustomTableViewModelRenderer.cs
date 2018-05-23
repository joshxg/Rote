using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ListView = Android.Widget.ListView;
using View = Android.Views.View;

namespace Rote.Droid.Resources.CustomRenderer
{
    public class CustomTableViewModelRenderer : TableViewModelRenderer
    {
        public CustomTableViewModelRenderer(Context context, ListView listView, TableView view) : base(context, listView, view)
        {

        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = base.GetView(position, convertView, parent);

            if (GetCellForPosition(position).GetType() != typeof(TextCell))
            {
                return view;
            }
            
            var layout = (LinearLayout)view;
            var text = (TextView)((LinearLayout)((LinearLayout)layout.GetChildAt(0)).GetChildAt(1)).GetChildAt(0);
            text.SetTextColor(Color.FromHex("35c7f0").ToAndroid());
            var divider = layout.GetChildAt(1);
            divider.SetBackgroundColor(Color.FromHex("35c7f0").ToAndroid());



            return view;
        }
    }
}