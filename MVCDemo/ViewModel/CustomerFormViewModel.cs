using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.ViewModel
{
    public class CustomerFormViewModel
    {
        public IEnumerable<MemberShipType> MembershipTypes { get; set; }
        public Customer Customer { get; set; }
    }
}
