using System;
using System.Collections.Generic;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Data
{
    public static class FakeDataFactory
    {
        public static IEnumerable<Employee> Employees => new List<Employee>()
        {
            new Employee()
            {
                Id = Guid.Parse("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"),
                Email = "owner@somemail.ru",
                FirstName = "Иван",
                LastName = "Сергеев",
                RoleId = Guid.Parse("53729686-a368-4eeb-8bfa-cc69b6050d02"),
                AppliedPromocodesCount = 5
            },
            new Employee()
            {
                Id = Guid.Parse("f766e2bf-340a-46ea-bff3-f1700b435895"),
                Email = "andreev@somemail.ru",
                FirstName = "Петр",
                LastName = "Андреев",
                RoleId = Guid.Parse("b0ae7aac-5493-45cd-ad16-87426a5e7665"),
                AppliedPromocodesCount = 10
            },
        };

        public static IEnumerable<Role> Roles => new List<Role>()
        {
            new Role()
            {
                Id = Guid.Parse("53729686-a368-4eeb-8bfa-cc69b6050d02"),
                Name = "Admin",
                Description = "Администратор",
            },
            new Role()
            {
                Id = Guid.Parse("b0ae7aac-5493-45cd-ad16-87426a5e7665"),
                Name = "PartnerManager",
                Description = "Партнерский менеджер"
            }
        };

        public static IEnumerable<Preference> Preferences => new List<Preference>()
        {
            new Preference()
            {
                Id = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c"),
                Name = "Театр",
            },
            new Preference()
            {
                Id = Guid.Parse("c4bda62e-fc74-4256-a956-4760b3858cbd"),
                Name = "Семья",
            },
            new Preference()
            {
                Id = Guid.Parse("76324c47-68d2-472d-abb8-33cfa8cc0c84"),
                Name = "Дети",
            }
        };

        public static IEnumerable<Customer> Customers
        {
            get
            {
                var customers = new List<Customer>()
                {
                    new Customer()
                    {
                        Id = Guid.Parse("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"),
                        Email = "ivan_sergeev@mail.ru",
                        FirstName = "Иван",
                        LastName = "Петров",
                    },
                    new Customer()
                    {
                        Id = Guid.Parse("33126f1f-4869-4432-a0a2-0ae8bddaa0da"),
                        Email = "some1@email",
                        FirstName = "Kirill",
                        LastName = "Ivanov",
                    },
                    new Customer()
                    {
                        Id = Guid.Parse("a94695eb-1db9-4996-87fc-603c4a254b5a"),
                        Email = "some2@email",
                        FirstName = "Sergey",
                        LastName = "Kolov",
                    }
                };

                return customers;
            }
        }

        public static IEnumerable<CustomerPreference> CustomerPreferences
        {
            get
            {
                var customerPreference = new List<CustomerPreference>()
                {
                    new CustomerPreference()
                    {
                        CustomerId = Guid.Parse("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"),
                        PreferenceId = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c")
                    },
                    new CustomerPreference()
                    {
                        CustomerId = Guid.Parse("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"),
                        PreferenceId = Guid.Parse("c4bda62e-fc74-4256-a956-4760b3858cbd")
                    },
                    new CustomerPreference()
                    {
                        CustomerId = Guid.Parse("33126f1f-4869-4432-a0a2-0ae8bddaa0da"),
                        PreferenceId = Guid.Parse("76324c47-68d2-472d-abb8-33cfa8cc0c84")
                    },
                    new CustomerPreference()
                    {
                        CustomerId = Guid.Parse("a94695eb-1db9-4996-87fc-603c4a254b5a"),
                        PreferenceId = Guid.Parse("c4bda62e-fc74-4256-a956-4760b3858cbd")
                    },
                    new CustomerPreference()
                    {
                        CustomerId = Guid.Parse("a94695eb-1db9-4996-87fc-603c4a254b5a"),
                        PreferenceId = Guid.Parse("76324c47-68d2-472d-abb8-33cfa8cc0c84")
                    },
                };

                return customerPreference;
            }
        }

        public static IEnumerable<PromoCode> PromoCodes
        {
            get
            {
                var promoCodes = new List<PromoCode>()
                {
                    new PromoCode()
                    {
                        Id = Guid.Parse("8f237e72-f68b-4f97-84a6-ddca90256c6a"),
                        BeginDate = DateTime.UtcNow,
                        Code = "123",
                        EndDate = DateTime.UtcNow.AddMonths(1),
                        ServiceInfo = "some service info 1",
                        PartnerName = "ABC",
                        CustomerId = Guid.Parse("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"),
                        PartnerManagerId = Guid.Parse("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"),
                        PreferenceId = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c")
                    },
                    new PromoCode()
                    {
                        Id = Guid.Parse("ec5b6c60-94a8-417e-ad1a-597fd3656d0f"),
                        BeginDate = DateTime.UtcNow,
                        Code = "345",
                        EndDate = DateTime.UtcNow.AddMonths(1),
                        ServiceInfo = "some service info 2",
                        PartnerName = "DEF",
                        CustomerId = Guid.Parse("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"),
                        PartnerManagerId = Guid.Parse("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"),
                        PreferenceId = Guid.Parse("c4bda62e-fc74-4256-a956-4760b3858cbd")
                    },
                    new PromoCode()
                    {
                        Id = Guid.Parse("b8cc7c55-4e52-475c-92c9-18183751e152"),
                        BeginDate = DateTime.UtcNow,
                        Code = "678",
                        EndDate = DateTime.UtcNow.AddMonths(1),
                        ServiceInfo = "some service info 3",
                        PartnerName = "GHI",
                        PartnerManagerId = Guid.Parse("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"),
                        PreferenceId = Guid.Parse("76324c47-68d2-472d-abb8-33cfa8cc0c84")
                    },
                };

                return promoCodes;
            }
        }
    }
}