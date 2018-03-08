using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rote.Controls;
using Rote.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rote.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectDeckPage : ContentPage
	{
		public SelectDeckPage (int Type)
		{
            var viewModels = new SelectDeckVM(Type);
            viewModels.navigation = Navigation;
            BindingContext = viewModels;
            
            InitializeComponent ();
		}
    }
}
            