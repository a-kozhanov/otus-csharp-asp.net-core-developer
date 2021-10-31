using System;
using System.Collections.Generic;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Dto;

namespace Otus.Teaching.Pcf.GivingToCustomer.Core.Domain
{
    public class Customer
        :BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string Email { get; set; }

        public List<Guid> PreferencesId { get; set; }
        
        public List<Guid> PromoCodesId { get; set; }

        public Customer()
        {
            PreferencesId = new List<Guid>();
            PromoCodesId = new List<Guid>();
        }
    }
}