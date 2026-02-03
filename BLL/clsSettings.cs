using DataAccessLayer.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class clsSettings
    {

       public byte DefualtBorrrowDays {  get; set; }
       public  byte DefualtFinePerDays { get; set; }

        //public clsSettings() { }

        private clsSettings (byte defualtBorrrowDays, byte defualtFinePerDays)
        {
            DefualtBorrrowDays = defualtBorrrowDays;
            DefualtFinePerDays = defualtFinePerDays;
        }

        public static clsSettings getSettings()
        {
            byte DefualtBorrrowDays = 0;
            byte DefualtFinePerDays = 0;

            if (clsSettingsData.getSettings(ref DefualtBorrrowDays, ref DefualtFinePerDays))
                return new clsSettings(DefualtBorrrowDays, DefualtFinePerDays);
            else
                return null;
        }

        public static bool addSettings(byte DefualtBorrrowDays, byte DefualtFinePerDays)
        {
            // If settings already exist, prevent duplicates
            if (clsSettingsData.isSettingsExist())
                return false;

            return clsSettingsData.addSettings(DefualtBorrrowDays, DefualtFinePerDays);
        }

        public static bool updateSettings(byte DefualtBorrrowDays, byte DefualtFinePerDays)
        {
            // Must exist to update
            if (clsSettingsData.isSettingsExist())
                return false;

            return clsSettingsData.updateSettings(DefualtBorrrowDays, DefualtFinePerDays);
        }

        public static bool deleteSettings()
        {
            // Must exist to delete
            if (clsSettingsData.isSettingsExist())
                return false;

            return clsSettingsData.deleteSettings();
        }
    }
}
