using System.Collections.Generic;
using System.Text;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using XOProject.Api.Controller;
using XOProject.Api.Model;
using XOProject.Services.Exchange;
using XOProject.Api.Model.Analytics;
using XOProject.Repository.Domain;
using XOProject.Repository.Exchange;
using XOProject.Api.Tests.Helpers;


namespace XOProject.Api.Tests
{
    public class AnalyticsControllerTest
    {

        private readonly AnalyticsController _analyticsController;

        private readonly Mock<IShareRepository> _shareRepositoryMock = new Mock<IShareRepository>();

        private readonly AnalyticsService _analyticsService;

        public AnalyticsControllerTest()
        {
            _analyticsService = new AnalyticsService(_shareRepositoryMock.Object);

            _analyticsController = new AnalyticsController(_analyticsService);
        }

       

        [TearDown]
        public void Cleanup()
        {
            _shareRepositoryMock.Reset();
           
        }

        [Test]
        public async Task Daily()
        {
            // Arrange
            ArrangeRates();
            // Act
            var result = await _analyticsController.Daily("CBI", 2018, 8, 17);
            var price = new PriceModel {
                Open = 300.0M,
                High = 400.0M,
                Low = 300.0M,
                Close = 400.0M
            };
            // Assert
            Assert.NotNull(result);
            var createdObject = result as OkObjectResult;
            var value = createdObject.Value as DailyModel;
            Assert.AreEqual("CBI",value.Symbol);
            Assert.AreEqual(price.Open, value.Price.Open);
            Assert.AreEqual(price.Close, value.Price.Close);
            Assert.AreEqual(price.High, value.Price.High);
            Assert.AreEqual(price.Low, value.Price.Low);
            Assert.AreEqual(2018, value.Day.Year);
            Assert.AreEqual(8, value.Day.Month);
            Assert.AreEqual(17, value.Day.Day);
        }

        [Test]
        public async Task Weekly()
        {
            // Arrange
            ArrangeRates();
            // Act
            var result = await _analyticsController.Weekly("CBI", 2018, 33);
            var price = new PriceModel
            {
                Open = 320.0M,
                High = 400.0M,
                Low = 300.0M,
                Close = 400.0M
            };
            // Assert
            Assert.NotNull(result);
            var createdObject = result as OkObjectResult;
            var value = createdObject.Value as WeeklyModel;
            Assert.AreEqual("CBI", value.Symbol);
            Assert.AreEqual(price.Open, value.Price.Open);
            Assert.AreEqual(price.Close, value.Price.Close);
            Assert.AreEqual(price.High, value.Price.High);
            Assert.AreEqual(price.Low, value.Price.Low);
            Assert.AreEqual(2018, value.Year);
            Assert.AreEqual(33, value.Week);
        }

        [Test]
        public async Task Monthly()
        {
            // Arrange
            ArrangeRates();
            // Act
            var result = await _analyticsController.Monthly("IBM", 2018, 9);
            var price = new PriceModel
            {
                Open = 390.0M,
                High = 600.0M,
                Low = 390.0M,
                Close = 450.0M
            };
            // Assert
            Assert.NotNull(result);
            var createdObject = result as OkObjectResult;
            var value = createdObject.Value as MonthlyModel;
            Assert.AreEqual("IBM", value.Symbol);
            Assert.AreEqual(price.Open, value.Price.Open);
            Assert.AreEqual(price.Close, value.Price.Close);
            Assert.AreEqual(price.High, value.Price.High);
            Assert.AreEqual(price.Low, value.Price.Low);
            Assert.AreEqual(2018, value.Year);
            Assert.AreEqual(9, value.Month);
        }
        private void ArrangeRates()
        {
            var rates = new[]
            {
                new HourlyShareRate
                {
                    Id = 1,
                    Symbol = "CBI",
                    Rate = 310.0M,
                    TimeStamp = new DateTime(2017, 08, 17, 5, 0, 0)
                },
                new HourlyShareRate
                {
                    Id = 2,
                    Symbol = "CBI",
                    Rate = 320.0M,
                    TimeStamp = new DateTime(2018, 08, 16, 5, 0, 0)
                },
                new HourlyShareRate
                {
                    Id = 3,
                    Symbol = "REL",
                    Rate = 300.0M,
                    TimeStamp = new DateTime(2018, 08, 17, 5, 0, 0)
                },
                new HourlyShareRate
                {
                    Id = 4,
                    Symbol = "CBI",
                    Rate = 300.0M,
                    TimeStamp = new DateTime(2018, 08, 17, 5, 0, 0)
                },
                new HourlyShareRate
                {
                    Id = 5,
                    Symbol = "CBI",
                    Rate = 400.0M,
                    TimeStamp = new DateTime(2018, 08, 17, 6, 0, 0)
                },
                new HourlyShareRate
                {
                    Id = 6,
                    Symbol = "IBM",
                    Rate = 350.0M,
                    TimeStamp = new DateTime(2018, 08, 17, 5, 0, 0)
                },
                new HourlyShareRate
                {
                    Id = 7,
                    Symbol = "IBM",
                    Rate = 390.0M,
                    TimeStamp = new DateTime(2018, 09, 1, 5, 0, 0)
                },
                new HourlyShareRate
                {
                    Id = 8,
                    Symbol = "IBM",
                    Rate = 500.0M,
                    TimeStamp = new DateTime(2018, 09, 2, 6, 0, 0)
                },
                 new HourlyShareRate
                {
                    Id = 9,
                    Symbol = "IBM",
                    Rate = 600.0M,
                    TimeStamp = new DateTime(2018, 09, 3, 5, 0, 0)
                },
                new HourlyShareRate
                {
                    Id = 10,
                    Symbol = "IBM",
                    Rate = 450.0M,
                    TimeStamp = new DateTime(2018, 09, 4, 6, 0, 0)
                },
                new HourlyShareRate
                {
                    Id = 11,
                    Symbol = "IBM",
                    Rate = 450.0M,
                    TimeStamp = new DateTime(2018, 09, 4, 8, 0, 0)
                },
                new HourlyShareRate
                {
                    Id = 12,
                    Symbol = "CBI",
                    Rate = 850.0M,
                    TimeStamp = new DateTime(2018, 09, 4, 9, 0, 0)
                },
                new HourlyShareRate
                {
                    Id = 13,
                    Symbol = "REL",
                    Rate = 650.0M,
                    TimeStamp = new DateTime(2018, 09, 4, 6, 0, 0)
                },
                 new HourlyShareRate
                {
                    Id = 14,
                    Symbol = "REL",
                    Rate = 950.0M,
                    TimeStamp = new DateTime(2018, 12, 4, 6, 0, 0)
                },
                  new HourlyShareRate
                {
                    Id = 15,
                    Symbol = "REL",
                    Rate = 350.0M,
                    TimeStamp = new DateTime(2018, 12, 4, 8, 0, 0)
                },
                  new HourlyShareRate
                {
                    Id = 16,
                    Symbol = "REL",
                    Rate = 450.0M,
                    TimeStamp = new DateTime(2018, 12, 5, 8, 0, 0)
                },
                  new HourlyShareRate
                {
                    Id = 17,
                    Symbol = "REL",
                    Rate = 550.0M,
                    TimeStamp = new DateTime(2018, 12, 6, 8, 0, 0)
                }
            };
            //_analyticsServiceMock.Object._shareControlRepository = _shareRepositoryMock.Object;

            _shareRepositoryMock
                .Setup(mock => mock.Query())
                .Returns(new AsyncQueryResult<HourlyShareRate>(rates));
        }

    }
}
