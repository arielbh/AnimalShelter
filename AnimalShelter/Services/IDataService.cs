using System.Collections.Generic;
using System.Threading.Tasks;
using AnimalShelter.Model;

namespace AnimalShelter.Services
{
    internal interface IDataService
    {
        Task<Dog[]> GetDogs();
        Task<Shelter[]> GetShelters(IEnumerable<Dog> dogs);

    }
}