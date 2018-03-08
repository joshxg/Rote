using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Rote.Controls
{
    public class DeckLabel : Label
    {
        
        public static BindableProperty IDProperty = BindableProperty.Create(
            propertyName: "ID",
            returnType: typeof(int),
            declaringType: typeof(CardLabel),
            defaultValue: -1,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanging: HandleIDPropertyChanged);

        public int ID
        {
            get
            {
                return (int)base.GetValue(IDProperty);
            }
            set
            {
                base.SetValue(IDProperty, value);
            }
        }

        private static void HandleIDPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var x = bindable as DeckLabel;
            x.ID = (int)newValue;
        }

        public DeckLabel()
        {
            IsEnabled = false;
        }
    }

}
