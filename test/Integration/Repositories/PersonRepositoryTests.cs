using System.Linq;
using FluentAssertions;
using LightInject;
using Ltht.TechTest.Entities;
using Ltht.TechTest.Ioc;
using Ltht.TechTest.Repositories;
using NUnit.Framework;

namespace Ltht.TechTest.Tests.Repositories
{
    [TestFixture]
    public class PersonRepositoryTests : IntegrationTestBase
    {
        private const string OriginalFirstName = "Willis";
        private const int TestId = 1;

        [SetUp]
        public void SetUp()
        {
            Container.Register<PersonRepository>();
            Container.Register<TechTestEntities>();
        }

        [TearDown]
        public void TearDown()
        {
            using (var context = new TechTestEntities())
            {
                var willis = context.People.Single(x => x.PersonId == TestId);
                if (willis.FirstName == OriginalFirstName) return;
                willis.FirstName = OriginalFirstName;
                context.SaveChanges();
            }
        }

        [Test]
        public void Query_WhenCalledWithoutExpression_ReturnsAllData()
        {
            // Arrange
            var sut = Container.GetInstance<PersonRepository>();

            // Act
            var result = sut.Query().ToList();

            // Assert
            result.Should().HaveCount(11, "because 11 is the number of people according to the tech test specifications");
        }

        [Test]
        public void Query_WhenRestrictedByName_ReturnsPerson()
        {
            // Arrange
            const string firstName = "Patrick";
            const string lastName = "Kerr";
            var sut = Container.GetInstance<PersonRepository>();

            // Act
            var result = sut.Query().First(x => x.FirstName == firstName && x.LastName == lastName);

            // Assert
            result.Should().NotBeNull("because there should be a person called Patrick Kerr in the database");
            result.FirstName.Should().Be(firstName);
            result.LastName.Should().Be(lastName);
        }

        [Test]
        public void Save_WhenEntityIsChanged_PersistsEntity()
        {
            // Arrange
            const string expected = "Aaron";
            var sut = Container.GetInstance<PersonRepository>();
            var person = sut.Query().Single(x => x.PersonId == 1);
            var originalFirstName = person.FirstName;

            // Act
            person.FirstName = expected;
            sut.Save();

            // Assert
            sut = Container.GetInstance<PersonRepository>();
            var loaded = sut.Query().Single(x => x.PersonId == 1);
            loaded.FirstName.Should().Be(expected);

        }
    }

    public class IntegrationTestBase
    {
        private IServiceContainer _container;

        [SetUp]
        public void BaseSetUp()
        {
            _container = ContainerFactory.Create();
        }

        protected IServiceContainer Container
        {
            get { return _container; }
        }
    }
}