using System;
namespace AnimalShelter.Model
{
    public static class AgeConverter

    {
        public static double CalcAgeInDogYears(DateTime birthday)
        {
            return ((DateTime.Now - birthday).TotalDays % 365) * 7;
        }
    }
}