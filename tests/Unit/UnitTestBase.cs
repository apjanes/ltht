using LightInject;
using LightInject.AutoMoq;
using NUnit.Framework;

namespace Ltht.TechTest.Tests.Unit
{
    public class UnitTestBase
    {
        private MockingServiceContainer _container;

        [SetUp]
        public void BaseSetUp()
        {
            _container = new MockingServiceContainer();
        }

        protected MockingServiceContainer Container
        {
            get { return _container; }
        }
    }
}