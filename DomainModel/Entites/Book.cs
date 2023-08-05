
namespace DomainModelsLayer.Entites
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public Author author { get; set; }
    }
}

// Book M  author 1
