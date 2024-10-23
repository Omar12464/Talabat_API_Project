using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat_Core.Order_Aggregate
{
    public class Address
    {
        public Address()
        {
            
        }
        public Address(string firstName, string lastName, string city, string state, string country)
        {
            FirstName = firstName;
            LastName = lastName;
            City = city;
            State = state;
            Country = country;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

    }
}
