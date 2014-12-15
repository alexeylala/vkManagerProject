using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vkManageFinal
{
    
    public  class SearchInfo
    {
        public static bool ManGender = true;

        public static int YearFrom = -1;
        public static int YearTo = -1;

        public static int YearBirthdayMonth = -1;
        public static int YearBirthdayYear = -1;

        public static string SelectedCountry = String.Empty;
        public static string SelectedCity = String.Empty;

        public static List<MyUser> FilteredUsers { get; set; }//Результивное множество

    }
}
