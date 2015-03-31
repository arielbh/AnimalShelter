using System;
namespace AnimalShelter.Model
{
    public static class AgeConverter

    {
        public static double CalcAgeInDogYears(DateTime birthday)
        {
            return ((int)(DateTime.Now - birthday).TotalDays % 365) * 7;
        }
    }
}