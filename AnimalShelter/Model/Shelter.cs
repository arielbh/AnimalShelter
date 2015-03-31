using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AnimalShelter.Views;
using SuiteValue.UI.MVVM;

namespace AnimalShelter.Model
{
    [DataContract]
    class Shelter : NotifyObject
    {
        [DataMember]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        private bool _isFavorite;

        public bool IsFavorite
        {
            get { return _isFavorite; }
            set
            {
                if (value != _isFavorite)
                {
                    _isFavorite = value;
                    OnPropertyChanged(() => IsFavorite);
                }
            }
        }
        public ObservableCollection<Dog> Dogs { get; set; }
        public ShelterSpace[] Spaces { get; set; }
    }

    class ShelterSpace
    {
        public int Units { get; set; }
        public string Size { get; set; }
        public int Available { get; set; }
    }
}
