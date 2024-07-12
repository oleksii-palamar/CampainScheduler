using CampainScheduler.Application.CampainService.Interfaces;
using CampainScheduler.Application.Context.Interfaces;
using CampainScheduler.Application.Sender;
using CampainScheduler.Application.Sender.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;

namespace CampainScheduler.Application.UnitTests.Services
{
    public class ToFileCampainSenderTest : BaseContextTest
    {
        private readonly Mock<ICampainService> _campainServiceMock;
        private readonly Mock<ICampainSchedulerContext> _campainSchedulerContextMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IFileWritter> _fileWritterMock;

        private readonly ICampainSender _campainSender;

        public ToFileCampainSenderTest()
        {
            _campainServiceMock = new Mock<ICampainService>();
            _configurationMock = new Mock<IConfiguration>();
            _campainSchedulerContextMock = new Mock<ICampainSchedulerContext>();
            _fileWritterMock = new Mock<IFileWritter>();

            _campainSender = new ToFileCampainSender(
                _campainServiceMock.Object,
                campainSchedulerContextMock.Object,
                _configurationMock.Object,
                _fileWritterMock.Object);
        }

        [Fact]
        public async Task SendCampainsByIdsAsync_NoCampainIdsDoesNothing()
        {
            // arrange
            var campainIds = new List<int>();

            // act
            await _campainSender.SendCampainsByIdsAsync(campainIds);

            // assert
            _campainSchedulerContextMock.Verify(context => context.Campains, 
                Times.Never);
            _campainServiceMock.Verify(service => service.GetCampainCustomersAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()), 
                Times.Never);
            _fileWritterMock.Verify(writter => writter.WriteAllLinesToFileAsync(
                    It.IsAny<string>(),
                    It.IsAny<List<string>>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task SendCampainsByIdsAsync_NoCustomersWritesNoDataToFile()
        {
            // arrange
            var campainIds = new List<int>() { 1 };

            _campainServiceMock
                .Setup(c => c.GetCampainCustomersAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync([]);

            // act
            await _campainSender.SendCampainsByIdsAsync(campainIds);

            // assert
            _campainServiceMock.Verify(service => service.GetCampainCustomersAsync(
                    1,
                    It.IsAny<CancellationToken>()),
                Times.Once);
            _fileWritterMock.Verify(writter => writter.WriteAllLinesToFileAsync(
                    It.IsAny<string>(),
                    It.IsAny<List<string>>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task SendCampainsByIdsAsync_SendsToCorrectFolder()
        {
            // arrange
            var campainIds = new List<int>() { 1 };
            var outputFileName = "sends.txt";
            var expectedFileName = @"../CampainScheduler/sends.txt";

            _campainServiceMock
                .Setup(c => c.GetCampainCustomersAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Domain.Models.Customer>
                {
                    new()
                    {
                        Id = 1
                    }
                });
            _configurationMock
                .Setup(c => c["CampainSender:OutputFile"])
                .Returns(outputFileName);

            // act
            await _campainSender.SendCampainsByIdsAsync(campainIds);

            // assert

            _fileWritterMock.Verify(writter => writter.WriteAllLinesToFileAsync(
                    expectedFileName,
                    It.IsAny<List<string>>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task SendCampainsByIdsAsync_SendsCorrectListOfCustomers()
        {
            // arrange
            var campainIds = new List<int>() { 1, 2 };
            var expectedLines = new List<string>
            {
                $"Customer with Id 1 received template with id 1",
                $"Customer with Id 2 received template with id 1",
                $"Customer with Id 1 received template with id 2",
                $"Customer with Id 2 received template with id 2",
            };

            _campainServiceMock
                .Setup(c => c.GetCampainCustomersAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Domain.Models.Customer>
                {
                    new()
                    {
                        Id = 1,
                    },
                    new()
                    {
                        Id = 2,
                    },
                });
            _configurationMock
                .Setup(c => c["CampainSender:OutputFile"])
                .Returns("");

            // act
            await _campainSender.SendCampainsByIdsAsync(campainIds);

            // assert

            _fileWritterMock.Verify(writter => writter.WriteAllLinesToFileAsync(
                    It.IsAny<string>(),
                    It.Is<List<string>>(list => 
                        list.Count == expectedLines.Count
                        && list[0] == expectedLines[0]
                        && list[1] == expectedLines[1]
                        && list[2] == expectedLines[2]
                        && list[3] == expectedLines[3]),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
