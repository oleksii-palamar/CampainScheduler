using CampainScheduler.Application.Context.Interfaces;
using CampainScheduler.Domain.Models;
using Moq;
using Moq.EntityFrameworkCore;

namespace CampainScheduler.Application.UnitTests
{
    public class BaseContextTest
    {
        internal Mock<ICampainSchedulerContext> campainSchedulerContextMock;

        public BaseContextTest()
        {
            campainSchedulerContextMock = new Mock<ICampainSchedulerContext>();

            campainSchedulerContextMock
                .Setup(c => c.Campains)
                .ReturnsDbSet(GetTestCampain());
            campainSchedulerContextMock
                .Setup(c => c.Customers)
                .ReturnsDbSet(GetTestCustomers());
            campainSchedulerContextMock
                .Setup(c => c.Templates)
                .ReturnsDbSet(GetTestTemplates());
        }

        private List<Campain> GetTestCampain() =>
            [
                new () 
                {
                    Id = 1,
                    CustomersCondition = "To all the Male customers",
                    Priority = 1,
                    ScheduledTimeUtc = new TimeSpan(10, 10, 0),
                    TemplateId = 1,
                },
                new ()
                {
                    Id = 2,
                    CustomersCondition = "To all customers above the age 45 ",
                    Priority = 2,
                    ScheduledTimeUtc = new TimeSpan(20, 20, 0),
                    TemplateId = 2,
                },
            ];

        private List<Customer> GetTestCustomers() =>
            [
                new ()
                {
                    Age = 50,
                    City = "New York",
                    Deposit = 101,
                    Gender = Domain.Enums.CustomerGender.Male,
                    IsNewCusomer = true,
                    Id = 1
                },
                new ()
                {
                    Age = 20,
                    City = "Lviv",
                    Deposit = 50,
                    Gender = Domain.Enums.CustomerGender.Female,
                    IsNewCusomer = false,
                    Id = 2
                },
                new ()
                {
                    Age = 30,
                    City = "New York",
                    Deposit = 101,
                    Gender = Domain.Enums.CustomerGender.Male,
                    IsNewCusomer = true,
                    Id = 3
                },
                new ()
                {
                    Age = 50,
                    City = "Lviv",
                    Deposit = 101,
                    Gender = Domain.Enums.CustomerGender.Male,
                    IsNewCusomer = true,
                    Id = 4
                },
                new ()
                {
                    Age = 50,
                    City = "New York",
                    Deposit = 50,
                    Gender = Domain.Enums.CustomerGender.Male,
                    IsNewCusomer = true,
                    Id = 5
                },
                new ()
                {
                    Age = 50,
                    City = "New York",
                    Deposit = 101,
                    Gender = Domain.Enums.CustomerGender.Female,
                    IsNewCusomer = true,
                    Id = 6
                },
                new ()
                {
                    Age = 50,
                    City = "New York",
                    Deposit = 101,
                    Gender = Domain.Enums.CustomerGender.Male,
                    IsNewCusomer = false,
                    Id = 7
                },
            ];

        private List<Template> GetTestTemplates() =>
            [
                new ()
                {
                    Id = 1,
                    TemplateBody = "Template Body 1"
                },
                new ()
                {
                    Id = 2,
                    TemplateBody = "Template Body 2"
                },
            ];
    }
}
