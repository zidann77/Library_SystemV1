using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogicLayer
{
    public class clsCountry
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public enum enMode { Add, Update };
        public enMode Mode { get; set; }

        public clsCountry()
        {
            Id = -1;
            Name = "";
            Mode = enMode.Add;
        }
       
        public clsCountry(int id, string name)
        {
            Id = id;
            Name = name;
            Mode = enMode.Update;
        }

        // Business Logic Methods

        public static clsCountry Find(int countryId)
        {
            string countryName = "";


            if (clsCountryData.getCountryById(countryId, ref countryName))
            {
                return new clsCountry(countryId, countryName);
            }
            else
            {
                return null;
            }
        }

        public static clsCountry Find(string countryName)
        {
            int ID = -1;

            if (clsCountryData.getCountryByName(ref ID, countryName))
            {
                return new clsCountry(ID, countryName);
            }
            else
            {
                return null;
            }


        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    if (_AddNew())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _Update();

                default:
                    return false;
            }
        }

        private bool _AddNew()
        {
            int newId = clsCountryData.addNewCountry(Name);

            if (newId > 0)
            {
                Id = newId;
                return true;
            }
            return false;
        }

        private bool _Update()
        {
            return clsCountryData.updateCountry(Id, Name);
        }

        public bool Delete()
        {
            return clsCountryData.deleteCountry(Id);
        }

        public static bool Delete(int countryId)
        {
            return clsCountryData.deleteCountry(countryId);
        }

        public static DataTable GetAllCountries()
        {
            return clsCountryData.getAllCountries();
        }

    
        public static bool Exists(int countryId)
        {
            return clsCountryData.isCountryExist(countryId);
        }

        public static bool Exists(string countryName)
        {
            return clsCountryData.isCountryExist(countryName);
        }

        // Additional Business Logic Properties/Methods

        public bool IsNew
        {
            get { return Mode == enMode.Add; }
        }

      

      
    }
}
