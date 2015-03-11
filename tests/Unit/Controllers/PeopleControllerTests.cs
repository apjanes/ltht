using System.Collections.ObjectModel;
using System.Linq;
using FluentAssertions;
using Ltht.TechTest.Controllers;
using Ltht.TechTest.Entities;
using Ltht.TechTest.Repositories;
using NUnit.Framework;

namespace Ltht.TechTest.Tests.Unit.Controllers
{
    [TestFixture]
    public class PeopleControllerTests : UnitTestBase
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void Get_WhenRequestedWithoutParameters_PersonSummariesAreReturned()
        {
            // Arrange
            var sut = CreateSut();
            var people = CreatePeople();
            Container.GetMock<IPersonRepository>()
                     .Setup(x => x.Query())
                     .Returns(people);

            // Act
            var result = sut.Get();

            // Assert
            result.Should().HaveCount(people.Count());

        }

        private PeopleController CreateSut()
        {
            // Unfortunately the controller cannot be constructed directly using the controller
            // in the test because property injection is attempted and some properties cannot
            // be resolved in a non-web context.
            return new PeopleController(
                    Container.GetInstance<IColourRepository>(),
                    Container.GetInstance<IPersonRepository>());
        }

        private IQueryable<Person> CreatePeople()
        {
            var people = new[]
            {
                new Person
                {
                    PersonId = 1,
                    FirstName = "Willis",
                    LastName = "Tibbs",
                    IsAuthorised = true,
                    IsEnabled = false,
                    Colours = new Collection<Colour>
                    {
                        new Colour
                        {
                            ColourId = 1,
                            Name = "Red"
                        }
                    }
                },
                
                new Person
                {
                    PersonId = 1,
                    FirstName = "Sharon",
                    LastName = "Halt",
                    IsAuthorised = false,
                    IsEnabled = true,
                    Colours = new Collection<Colour>
                    {
                        new Colour
                        {
                            ColourId = 2,
                            Name = "Green"
                        }
                    }
                }
            };

            return people.AsQueryable();
        }
    }
}