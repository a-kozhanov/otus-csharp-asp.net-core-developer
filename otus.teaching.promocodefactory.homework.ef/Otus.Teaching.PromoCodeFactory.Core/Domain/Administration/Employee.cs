using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain.Administration
{
    public class Employee : BaseEntity
    {
        // [MaxLength(50)] 
        public string FirstName { get; set; }

        // [MaxLength(50)] 
        public string LastName { get; set; }

        // [MaxLength(150)] 
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public int AppliedPromocodesCount { get; set; }
        public IList<PromoCode> PromoCodes { get; set; }
    }
}