namespace Hr_Vacancy_Managment.Helper
{
    public static class TimeHelper
    {
        public static string GetTimeAgo(DateTime dateTime)
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(dateTime);
            if(timeSpan.TotalDays >= 1)
            {
                return $"{(int)timeSpan.TotalDays} day ago";
            }
            if(timeSpan.TotalHours >= 1)
            {
                return $"{(int)timeSpan.TotalHours} hour ago";
            }
            if(timeSpan.TotalMinutes >= 1)
            {
                return $"{(int)timeSpan.TotalMinutes} minute ago";
            }
            return "Just Now";
        }
    }
}
