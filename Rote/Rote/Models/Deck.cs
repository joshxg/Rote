using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Rote.Models
{
    [Table("Deck")]
    public class Deck : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int ScheduleIndex { get; set; }
        public ObservableCollection<int> Schedule;
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged("Name"); }
        }


        public Deck() { }

        public Deck(String DeckName)
        {
            Name = DeckName;
            ScheduleIndex = 0;
        }



        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
