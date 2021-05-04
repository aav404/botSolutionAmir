using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VkBot
{
    public static class Extensions
    {
        public static DateTime TicksToDateTime(this long ticks) => new DateTime(ticks);
    }
}
