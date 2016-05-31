using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SignalRChat.Helper
{
    public static class Extensions
    {
        public static SelectList GetMvcSelectListFromIEnumarable(this IEnumerable<SignalRChat.Models.User> listToConvert)
        {
            return new SelectList(listToConvert, "Id", "Description");
        }
    }
}
