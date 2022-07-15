using Library.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;

namespace LibraryTest
{
    public class TestBase<T> where T : class
    {
        protected T? SUT;
        protected Mock<ILogger<T>> Logger;
        private readonly AutoMocker _container;

        public TestBase()
        {
            _container = new AutoMocker(Moq.MockBehavior.Default);
            SUT = _container.CreateInstance<T>();
            Logger = _container.GetMock<ILogger<T>>();
        }

        protected Mock<T1> MockFor<T1>() where T1 : class
            => _container.GetMock<T1>();

        protected LibraryModel LibraryModel
            => new()
            {
                Books = new List<BookModel> {
                    new BookModel{
                        Id = 1,
                        BookId = "bk101",
                        Author = "Gambardella, Matthew",
                        Title = "XML Developer's Guide"
                    },
                    new BookModel{
                        Id = 2,
                        BookId = "bk102",
                        Author = "Ralls, Kim",
                        Title = "Midnight Rain"
                    }
                }
            };

        protected LoginModel LoginModel
            => new() { UserId = "guest", Password = "guest" };
}
}
