using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Gateways;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using Otus.Teaching.Pcf.GivingToCustomer.DataAccess.Repositories;
using Otus.Teaching.Pcf.GivingToCustomer.Integration;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Controllers;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Models;
using Xunit;

namespace Otus.Teaching.Pcf.GivingToCustomer.IntegrationTests.Components.WebHost.Controllers
{
    [Collection(EfDatabaseCollection.DbCollection)]
    public class CustomersControllerTests: IClassFixture<EfDatabaseFixture>
    {
        private readonly CustomersController _customersController;
        private readonly EfRepository<Customer> _customerRepository;
        private readonly EfRepository<Preference> _preferenceRepository;
        private readonly EfRepository<PromoCode> _promocodeRepository;
        private readonly IPreferencesGateway _preferencesGateway;
        
        public CustomersControllerTests(EfDatabaseFixture efDatabaseFixture)
        {
            _customerRepository = new EfRepository<Customer>(efDatabaseFixture.DbContext);
            _preferenceRepository = new EfRepository<Preference>(efDatabaseFixture.DbContext);
            _promocodeRepository = new EfRepository<PromoCode>(efDatabaseFixture.DbContext);
            _preferencesGateway = new PreferencesGateway(new HttpClient());
            _customersController = new CustomersController(
                _customerRepository,
                _promocodeRepository,
                _preferencesGateway);
        }
        
        [Fact]
        public async Task CreateCustomerAsync_CanCreateCustomer_ShouldCreateExpectedCustomer()
        {
            //Arrange 
            var preferenceId = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c");
            var request = new CreateOrEditCustomerRequest()
            {
                Email = "some@mail.ru",
                FirstName = "Иван",
                LastName = "Петров",
                PreferenceIds = new List<Guid>()
                {
                    preferenceId
                }
            };

            //Act
            var result = await _customersController.CreateCustomerAsync(request);
            var actionResult = result.Result as CreatedAtActionResult;
            var id = (Guid)actionResult.Value;
            
            //Assert
            var actual = await _customerRepository.GetByIdAsync(id);
            
            actual.Email.Should().Be(request.Email);
            actual.FirstName.Should().Be(request.FirstName);
            actual.LastName.Should().Be(request.LastName);
            
            // actual.Preferences.Should()
            //     .ContainSingle()
            //     .And
            //     .Contain(x => x.PreferenceId == preferenceId);
        }
    }
}