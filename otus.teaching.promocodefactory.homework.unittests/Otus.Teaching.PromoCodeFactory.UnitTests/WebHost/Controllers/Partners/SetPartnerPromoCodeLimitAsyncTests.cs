using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.UnitTests.Builders;
using Otus.Teaching.PromoCodeFactory.WebHost.Controllers;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using Xunit;

namespace Otus.Teaching.PromoCodeFactory.UnitTests.WebHost.Controllers.Partners
{
    public class SetPartnerPromoCodeLimitAsyncTests
    {
        private readonly Mock<IRepository<Partner>> _partnersRepositoryMock;

        private readonly PartnersController _partnersController;
        // private readonly Fixture _fixture;

        public SetPartnerPromoCodeLimitAsyncTests()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            _partnersRepositoryMock = fixture.Freeze<Mock<IRepository<Partner>>>();
            _partnersController = fixture.Build<PartnersController>().OmitAutoProperties().Create();
        }

        private Partner GetBasePartner()
        {
            var partner = PartnerBuilder.CreateBasePartner();

            return partner;
        }

        [Fact]
        public async void SetPartnerPromoCodeLimitAsync_PartnerIsNotFound_ReturnsNotFound()
        {
            // Arrange
            var partnerId = Guid.Parse("def47943-7aaf-44a1-ae21-05aa4948b165");
            var request = new Fixture().Create<SetPartnerPromoCodeLimitRequest>();

            Partner partner = null;

            _partnersRepositoryMock.Setup(repo => repo.GetByIdAsync(partnerId)).ReturnsAsync(partner);

            // Act
            var result = await _partnersController.SetPartnerPromoCodeLimitAsync(partnerId, request);

            // Assert
            result.Should().BeAssignableTo<NotFoundResult>();
        }

        [Fact]
        public async void SetPartnerPromoCodeLimitAsync_PartnerIsNotActive_ReturnsBadRequest()
        {
            // Arrange
            var partnerId = Guid.Parse("def47943-7aaf-44a1-ae21-05aa4948b165");
            var request = new Fixture().Create<SetPartnerPromoCodeLimitRequest>();

            var partner = CreateBasePartner();
            partner.IsActive = false;

            _partnersRepositoryMock.Setup(repo => repo.GetByIdAsync(partnerId)).ReturnsAsync(partner);

            // Act
            var result = await _partnersController.SetPartnerPromoCodeLimitAsync(partnerId, request);

            // Assert
            result.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        private Partner CreateBasePartner()
        {
            var partner = new Partner()
            {
                Id = Guid.Parse("7d994823-8226-4273-b063-1a95f3cc1df8"),
                Name = "Суперигрушки",
                IsActive = true,
                PartnerLimits = new List<PartnerPromoCodeLimit>()
                {
                    new PartnerPromoCodeLimit()
                    {
                        Id = Guid.Parse("e00633a5-978a-420e-a7d6-3e1dab116393"),
                        CreateDate = new DateTime(2020, 07, 9),
                        EndDate = new DateTime(2020, 10, 9),
                        Limit = 100
                    }
                }
            };

            return partner;
        }

        [Fact]
        public async void SetPartnerPromoCodeLimitAsync_PartnerSetLimit_ZeroingNumberIssuedPromoCodes()
        {
            //Arrange
            var partner = GetBasePartner().WithActiveLimit();
            var partnerId = partner.Id;
            var request = new Fixture().Create<SetPartnerPromoCodeLimitRequest>();
            var expected = GetBasePartner().WithActiveLimit().ResetNumberIssuedPromoCodes();

            var mock = new Mock<IRepository<Partner>>();
            mock.Setup(x => x.GetByIdAsync(partnerId)).ReturnsAsync(partner);

            var mockCurrentDateTimeProvider = new Mock<ICurrentDateTimeProvider>();
            mockCurrentDateTimeProvider.Setup(x => x.CurrentDateTime)
                .Returns(DateTime.Now);

            var controller = new PartnersController(mock.Object, mockCurrentDateTimeProvider.Object);

            //Act
            await controller.SetPartnerPromoCodeLimitAsync(partnerId, request);

            //Assert
            partner.NumberIssuedPromoCodes.Should().Be(expected.NumberIssuedPromoCodes);
        }

        [Fact]
        public async void SetPartnerPromoCodeLimitAsync_PartnerSetLimit_NotZeroingNumberIssuedPromoCodes()
        {
            //Arrange
            var partner = GetBasePartner().WithNotActiveLimit();
            var partnerId = partner.Id;
            var request = new Fixture().Create<SetPartnerPromoCodeLimitRequest>();
            var expected = GetBasePartner().WithNotActiveLimit();

            var mock = new Mock<IRepository<Partner>>();
            mock.Setup(x => x.GetByIdAsync(partnerId)).ReturnsAsync(partner);
            var mockCurrentDateTimeProvider = new Mock<ICurrentDateTimeProvider>();
            mockCurrentDateTimeProvider.Setup(x => x.CurrentDateTime)
                .Returns(DateTime.Now);

            var controller = new PartnersController(mock.Object, mockCurrentDateTimeProvider.Object);

            //Act
            await controller.SetPartnerPromoCodeLimitAsync(partnerId, request);

            //Assert
            partner.NumberIssuedPromoCodes.Should().Be(expected.NumberIssuedPromoCodes);
        }

        [Fact]
        public async void SetPartnerPromoCodeLimitAsync_HasActiveLimit_ShouldSetCancelDateNow()
        {
            //Arrange
            var partner = GetBasePartner().WithActiveLimit();
            var targetLimit = partner.PartnerLimits.First();
            var partnerId = partner.Id;
            var request = new Fixture().Create<SetPartnerPromoCodeLimitRequest>();
            var now = new DateTime(2020, 10, 14);
            var expected =
                new PartnerPromoCodeLimit()
                {
                    Id = Guid.Parse("c9bef066-3c5a-4e5d-9cff-bd54479f075e"),
                    CreateDate = new DateTime(2020, 07, 9),
                    CancelDate = now,
                    EndDate = new DateTime(2020, 10, 9),
                    Limit = 100
                };

            var mock = new Mock<IRepository<Partner>>();
            mock.Setup(x => x.GetByIdAsync(partnerId)).ReturnsAsync(partner);
            var mockCurrentDateTimeProvider = new Mock<ICurrentDateTimeProvider>();
            mockCurrentDateTimeProvider.Setup(x => x.CurrentDateTime)
                .Returns(now);

            //Act
            var controller = new PartnersController(mock.Object, mockCurrentDateTimeProvider.Object);

            await controller.SetPartnerPromoCodeLimitAsync(partnerId, request);

            //Assert
            targetLimit.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async void SetPartnerPromoCodeLimitAsync_NegativeLimit_ReturnsBadRequest()
        {
            // Arrange
            var partnerId = Guid.Parse("def47943-7aaf-44a1-ae21-05aa4948b165");
            var request = new Fixture().Create<SetPartnerPromoCodeLimitRequest>();
            request.Limit = -20;

            var partner = CreateBasePartner();

            _partnersRepositoryMock.Setup(repo => repo.GetByIdAsync(partnerId)).ReturnsAsync(partner);

            // Act
            var result = await _partnersController.SetPartnerPromoCodeLimitAsync(partnerId, request);

            // Assert
            result.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        [Fact]
        public async void SetPartnerPromoCodeLimitAsync_ValidModel_SuccessUpdate()
        {
            //Arrange
            var partner = GetBasePartner();
            var partnerId = partner.Id;
            var request = new Fixture().Create<SetPartnerPromoCodeLimitRequest>();

            var mock = new Mock<IRepository<Partner>>();
            mock.Setup(x => x.GetByIdAsync(partnerId)).ReturnsAsync(partner);

            var mockCurrentDateTimeProvider = new Mock<ICurrentDateTimeProvider>();
            mockCurrentDateTimeProvider.Setup(x => x.CurrentDateTime)
                .Returns(DateTime.Now);

            var controller = new PartnersController(mock.Object, mockCurrentDateTimeProvider.Object);

            //Act
            await controller.SetPartnerPromoCodeLimitAsync(partnerId, request);

            //Assert
            mock.Verify(x => x.UpdateAsync(partner), Times.Once);
        }
    }
}