﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Dto;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Models;

namespace Otus.Teaching.Pcf.GivingToCustomer.WebHost.Mappers
{
    public class CustomerMapper
    {

        public static Customer MapFromModel(CreateOrEditCustomerRequest model, IEnumerable<PreferenceDto> preferences, Customer customer = null)
        {
            if(customer == null)
            {
                customer = new Customer();
                customer.Id = Guid.NewGuid();
            }
            
            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.Email = model.Email;

            customer.PreferencesId = preferences.Select(p => p.Id).ToList();
            
            // customer.Preferences = preferences.Select(x => new CustomerPreference()
            // {
            //     CustomerId = customer.Id,
            //     Preference = x,
            //     PreferenceId = x.Id
            // }).ToList();
            
            return customer;
        }
    }
}
