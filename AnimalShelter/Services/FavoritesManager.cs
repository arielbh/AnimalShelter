using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AnimalShelter.Model;

namespace AnimalShelter.Services
{
    internal interface IFavoritesManager
    {
        void Load();
        void Save();
        List<Dog> FavoriteDogs { get; set; }
    }
    [DataContract]
    class FavoritesManager : IFavoritesManager
    {
        public FavoritesManager()
        {
            FavoriteDogs = new List<Dog>();
        }

        public void Load()
        {
            var serializer = new DataContractSerializer(typeof(FavoritesManager));
            using (var stream = new FileStream(Environment.CurrentDirectory +@"\favorites.data", FileMode.OpenOrCreate))
            {
                if (stream.Length == 0) return;
                var data = (FavoritesManager)serializer.ReadObject(stream);
                this.FavoriteDogs = data.FavoriteDogs;
            }  
        }

        public void Save()
        {
            var serializer = new DataContractSerializer(typeof (FavoritesManager));
            using (var stream = new FileStream(Environment.CurrentDirectory + @"\favorites.data", FileMode.Create))
            {
                serializer.WriteObject(stream, this);
            }
        }

        [DataMember]
        public List<Dog> FavoriteDogs { get; set; }
   }
}
