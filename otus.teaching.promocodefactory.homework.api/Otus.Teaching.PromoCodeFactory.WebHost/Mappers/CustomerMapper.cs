﻿using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Mappers
{
    public class CustomerMapper
    {

        public static Customer MapFromModel(CreateOrEditCustomerRequest model, IEnumerable<Preference> preferences, Customer customer = null)
        {
            if(customer == null)
            {
                customer = new Customer
                {
                    Id = Guid.NewGuid()
                };
            }
            
            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.Email = model.Email;

            if (customer.Preferences != null)
                customer.Preferences = preferences.Select(x => new CustomerPreference()
                {
                    CustomerId = customer.Id,
                    Preference = x,
                    PreferenceId = x.Id
                }).ToList();

            return customer;
        }
    }
}
