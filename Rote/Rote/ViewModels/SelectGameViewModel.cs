using Rote.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Rote.ViewModels
{
    public class SelectGameViewModel
    {
        public ICommand MultiChoice { get; set; }
        public ICommand Random { get; set; }
        public ICommand ByScore { get; set; }

        public SelectGameViewModel()
        {
            MultiChoice = new Command(MultiChoiceSelected);
            Random = new Command(RandomSelected);
            ByScore = new Command(ByScoreSelected);
        }

        private void RandomSelected(object obj)
        {
            Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new SelectDeckPage(1));
        }

        private void MultiChoiceSelected(object obj)
        {
            Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new SelectDeckPage(2));
        }

        private void ByScoreSelected(object obj)
        {
            Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new SelectDeckPage(3));
        }
    }

}
