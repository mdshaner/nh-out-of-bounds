using System.Linq;
using NHibernate;

namespace OutOfBounds
{
    public class TestScenario
    {
        #region Constructors

        public TestScenario(ISessionFactory factory)
        {
            Factory = factory;
        }

        #endregion

        #region Properties

        private ISessionFactory Factory { get; }

        #endregion

        #region Public Methods

        public void Test()
        {
            CreateNew();
            DeleteAll();
        }

        #endregion

        #region Private Methods

        private void CreateNew()
        {
            var session = Factory.OpenSession();

            try
            {
                using var tx = session.BeginTransaction();
                var person = new Person
                {
                    Name = "Testing"
                };
                person.Address = new Address
                {
                    Person = person,
                    Street = "Mulberry"
                };
                session.Save(person);
                tx.Commit();
            }
            finally
            {
                session.Close();
            }
        }

        private void DeleteAll()
        {
            var session = Factory.OpenSession();

            try
            {
                using var tx = session.BeginTransaction();

                var persons = session.Query<Person>().ToList();

                foreach (var person in persons)
                {
                    session.Delete(person);
                }

                tx.Commit();
            }
            finally
            {
                session.Close();
            }
        }

        #endregion
    }
}