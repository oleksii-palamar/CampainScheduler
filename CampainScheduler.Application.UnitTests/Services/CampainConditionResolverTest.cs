using CampainScheduler.Application.CampainService;
using CampainScheduler.Domain.Models;

namespace CampainScheduler.Application.UnitTests.Services
{
    public class CampainConditionResolverTest : BaseContextTest
    {
        private readonly CampainConditionResolver _conditionResolver;

        public CampainConditionResolverTest()
        {
            _conditionResolver = new CampainConditionResolver(
                campainSchedulerContextMock.Object);
        }

        [Theory]
        [InlineData("To all the Male customers", 0)]
        [InlineData("To all customers above the age 45 ", 1)]
        [InlineData("To all the customers from New York", 2)]
        [InlineData("To all the customers that deposit more than 100 $", 3)]
        [InlineData("To all the customers that marked as new Customers ", 4)]
        [InlineData("Request that is not handled", 5)]
        public async Task GetCusomtersByCampainConditionAsync_BaseCondition(
            string condition,
            int expectedResultNumber)
        {
            // arrange
            var expected = GetExpectedResults()[expectedResultNumber];

            // act
            var actual = await _conditionResolver
                .GetCustomersByCampainConditionAsync(condition, []);

            // assert
            Assert.Equal(expected.Count, actual.Count);

            foreach (var expectedCustomer in expected)
            {
                Assert.NotNull(actual
                    .FirstOrDefault(a => a.Id == expectedCustomer.Id));
            }
        }

        [Fact]
        public async Task GetCusomtersByCampainConditionAsync_CustomersToIgnore()
        {
            // arrage
            var condition = "To all the Male customers";
            var conditionExpectedNumber = 0;

            var baseExpectedCustomers = GetExpectedResults()[conditionExpectedNumber];
            var customerIdsToIgnore = new List<int> { 1, 3 };
            var expected = baseExpectedCustomers
                .Where(c => !customerIdsToIgnore.Contains(c.Id))
                .ToList();

            // act
            var actual = await _conditionResolver
                .GetCustomersByCampainConditionAsync(condition, customerIdsToIgnore);

            // assert
            Assert.Equal(expected.Count, actual.Count);
        }

        #region Expected Data

        private List<Customer>[] GetExpectedResults() =>
            [
                new List<Customer>{
                    new() {
                        Id = 1,
                    },
                    new(){
                        Id = 3,
                    },
                    new() {
                        Id = 4,
                    },
                    new(){
                        Id = 5,
                    },
                    new(){
                        Id = 7,
                    },
                },
                new List<Customer>{
                    new() {
                        Id = 1,
                    },
                    new(){
                        Id = 4,
                    },
                    new() {
                        Id = 5,
                    },
                    new(){
                        Id = 6,
                    },
                    new(){
                        Id = 7,
                    },
                },
                new List<Customer> {
                    new() {
                        Id = 1,
                    },
                    new(){
                        Id = 3,
                    },
                    new() {
                        Id = 5,
                    },
                    new(){
                        Id = 6,
                    },
                    new(){
                        Id = 7,
                    }
                },
                new List<Customer>{
                    new() {
                        Id = 1,
                    },
                    new(){
                        Id = 3,
                    },
                    new() {
                        Id = 4,
                    },
                    new(){
                        Id = 6,
                    },
                    new(){
                        Id = 7,
                    },
                },
                new List<Customer> {
                    new() {
                        Id = 1,
                    },
                    new(){
                        Id = 3,
                    },
                    new() {
                        Id = 4,
                    },
                    new(){
                        Id = 5,
                    },
                    new(){
                        Id = 6,
                    },
                },
                new List<Customer> {
                    new() {
                        Id = 1,
                    },
                    new() {
                        Id = 2,
                    },
                    new(){
                        Id = 3,
                    },
                    new() {
                        Id = 4,
                    },
                    new(){
                        Id = 5,
                    },
                    new(){
                        Id = 6,
                    },
                    new(){
                        Id = 7,
                    },
                },
            ];
    }

    #endregion

}