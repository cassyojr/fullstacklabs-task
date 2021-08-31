using System;
using System.Globalization;

namespace Domain.Helper
{
    public static class DateTimeHelper
    {
        public static string GetFormattedDateTime(this DateTime dateTime) => string.Format(new CultureInfo("en-US"), "{0:MMM, d, yyyy}", dateTime);
    }
}
