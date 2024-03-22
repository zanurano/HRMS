using CoreApp.Model.Employee;

namespace CoreApp.Lib.Helper
{
    public class GenerateAny
    {
        public static string GenerateIdByDateTime()
        {
            return DateTime.Now.Ticks.ToString();
        }
        public static string GenerateIdByDateTime(string id)
        {
            string res = "";
            if (string.IsNullOrEmpty(id))
            {
                res = DateTime.Now.Ticks.ToString();
                return res;
            }
            return id;
        }
    }
}
