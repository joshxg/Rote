using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Rote.Controls
{
    public class CardLabel : Label
    {
        public static BindableProperty IDProperty = BindableProperty.Create(
        propertyName: "ID",
        returnType: typeof(int?),
        declaringType: typeof(CardLabel),
        defaultValue: -1,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanging: HandleIDPropertyChanged);

        public int? ID
        {
            get
            {
                return (int?)base.GetValue(IDProperty);
            }
            set
            {
                base.SetValue(IDProperty, value);
            }
        }

        private static void HandleIDPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var x = bindable as CardLabel;
            x.ID = (int?)newValue;
        }

        public static BindableProperty ScoreProperty = BindableProperty.Create(
            propertyName: "Score",
            returnType: typeof(int?),
            declaringType: typeof(CardLabel),
            defaultValue: -1,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: HandleScorePropertyChanged);

        public int? Score
        {
            get
            {
                return (int?)base.GetValue(ScoreProperty);
            }
            set
            {
                base.SetValue(ScoreProperty, value);
            }
        }

        private static void HandleScorePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var x = bindable as CardLabel;
            x.Score = (int?)newValue;
        }


    }
}
