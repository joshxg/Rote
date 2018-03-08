using Rote.Models;
using Rote.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rote.Views
{
   

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MultiChoicePage : ContentPage
	{

        public MultiChoicePage(Deck Deck)
        {
            BindingContext = new MultiChoiceViewModel(Deck);
            InitializeComponent();
        }
	}


}