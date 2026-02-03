using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class clsPeople
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ImageURL { get; set; }
        public bool IsMale { get; set; }
        public clsCountry CountryInfo { get; set; }

        public enum enMode { Add = 0, Update = 1 }
        public enMode Mode = enMode.Add;

        public clsPeople()
        {
            Id = -1;
            CountryId = -1;
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            Phone = "";
            Email = "";
            ImageURL = "";
            IsMale = false; // Default value
            CountryInfo = new clsCountry();
            Mode = enMode.Add;
        }

        public clsPeople(int id, int countryId, string firstName, string secondName,
                         string thirdName, string lastName, string phone, string email,
                         string imageURL, bool gender)
        {
            Id = id;
            CountryId = countryId;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            ImageURL = imageURL;
            IsMale = gender;
            CountryInfo = clsCountry.Find(countryId);
            Mode = enMode.Update;
        }

        // CRUD Operations
        public static clsPeople Find(int personId)
        {
            string firstName = "", secondName = "", thirdName = "", lastName = "", phone = "", email = "", imageURL = "";
            int countryId = -1;
            bool gender = false;

            if (clsPeopleData.getPersonById(personId, ref firstName, ref secondName,
                ref thirdName, ref lastName, ref phone, ref countryId, ref email, ref imageURL, ref gender))
            {
                return new clsPeople(personId, countryId, firstName, secondName,
                                     thirdName, lastName, phone, email, imageURL, gender);
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
                        return false;

                case enMode.Update:
                    return _Update();

                default:
                    return false;
            }
        }

        private bool _AddNew()
        {
            int newId = clsPeopleData.addNewPerson(FirstName, SecondName, ThirdName,
                                                   LastName, Phone, CountryId, Email, ImageURL, IsMale);

            if (newId > 0)
            {
                Id = newId;
                CountryInfo = clsCountry.Find(CountryId);
                return true;
            }
            return false;
        }

        private bool _Update()
        {
            bool updated = clsPeopleData.updatePerson(Id, FirstName, SecondName, ThirdName,
                                                      LastName, Phone, CountryId, Email, ImageURL, IsMale);

            if (updated)
            {
                CountryInfo = clsCountry.Find(CountryId);
            }

            return updated;
        }

        public bool Delete()
        {
            return clsPeopleData.deletePerson(Id);
        }

        public static bool Delete(int personId)
        {
            return clsPeopleData.deletePerson(personId);
        }

        public static DataTable GetAllPeople()
        {
            return clsPeopleData.getAllPeople();
        }

        public static List<clsPeople> GetPeopleList()
        {
            List<clsPeople> peopleList = new List<clsPeople>();
            DataTable dt = GetAllPeople();

            foreach (DataRow row in dt.Rows)
            {
                clsPeople person = new clsPeople(
                    (int)row["PersonID"],
                    (int)row["CountryID"],
                    (string)row["FirstName"],
                    (string)row["SecondName"],
                    (string)row["ThirdName"],
                    (string)row["LastName"],
                    (string)row["Phone"],
                    row["Email"] == DBNull.Value ? "" : (string)row["Email"],
                    row["ImageURL"] == DBNull.Value ? "" : (string)row["ImageURL"],
                    (bool)row["Gender"]
                );

                peopleList.Add(person);
            }

            return peopleList;
        }

        public static bool Exists(int personId)
        {
            return clsPeopleData.isPersonExist(personId);
        }

        public static bool Exists(string email)
        {
            return clsPeopleData.isPersonExist(email);
        }

        public bool IsNew
        {
            get { return Mode == enMode.Add; }
        }

        public string FullName
        {
            get
            {
                return $"{FirstName} {SecondName} {ThirdName} {LastName}";
            }
        }

        //public bool IsValid()
        //{
        //    if (string.IsNullOrWhiteSpace(FirstName)) return false;
        //    if (string.IsNullOrWhiteSpace(SecondName)) return false;
        //    if (string.IsNullOrWhiteSpace(ThirdName)) return false;
        //    if (string.IsNullOrWhiteSpace(LastName)) return false;
        //    if (string.IsNullOrWhiteSpace(Phone) || Phone.Length != 10) return false;
        //    if (CountryId <= 0) return false;
        //    if (!clsCountry.Exists(CountryId)) return false;
        //    if (!string.IsNullOrWhiteSpace(Email) && !IsValidEmail(Email)) return false;

        //    return true;
        //}

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ FirstName.GetHashCode() ^ LastName.GetHashCode();
        }

        public static List<clsPeople> GetPeopleByCountry(int countryId)
        {
            return GetPeopleList().Where(p => p.CountryId == countryId).ToList();
        }
    }
}
