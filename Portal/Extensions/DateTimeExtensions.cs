namespace Portal.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsBefore(this DateTime value, DateTime other)
        {
            return value < other;
        }

        public static bool IsAfter(this DateTime value, DateTime other)
        {
            return value > other;
        }
    }
}
