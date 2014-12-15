using Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vkManageFinal
{
    public class SystemInfo
    {
        public static bool IsAuthorisedUser = false;//Признак авторизации пользователя

        public static long UserID_Search { get; set; }//ид пользователя для поиска

        public static string UserScreenName_Search { get; set; }//никнэйм пользователя для поиска

        public static long GroupID_Search { get; set; }//ид группы для поиска

        public static string GroupScreenName_Search{ get; set; }//никнэйм группы для поиска

        public static string FilePathToImportExportXml { get; set; }//Путь к файлу для импорта/экспорта

        public static Vkontakte vkPluginReference = null;//Ссылка на плагин

        public static Dictionary<int, string> Counties = new Dictionary<int, string>();//Страны

        public static string[] Cities { get; set; }//Города выбранной страны

    }
}
