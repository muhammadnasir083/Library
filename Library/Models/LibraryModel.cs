using System.Diagnostics.CodeAnalysis;

namespace Library.Models
{
    [ExcludeFromCodeCoverage]
    public class LibraryModel
    {
        public List<BookModel> Books { get; set; } = new List<BookModel>();
    }

    [ExcludeFromCodeCoverage]
    public class BookModel
    {
        public int Id { get; set; }
        public string BookId { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public double Price { get; set; }
        public DateTime PublishDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public string BorrowedBy { get; set; } = string.Empty;
        public bool IsBorrowed { get; set; }
    }
}
