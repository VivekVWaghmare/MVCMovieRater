using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.Models
{
    public class MemberShipType
    {
        public byte Id { get; set; }
        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }

        public string Name { get; set; }


        public static readonly byte Unknown = 0;
        public static readonly byte PayAsYouGo = 0;
        public static readonly byte Monthly = 0;
        public static readonly byte Quarterly = 0;
        public static readonly byte Yearly = 0;

    }
}
