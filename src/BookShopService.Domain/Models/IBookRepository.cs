namespace BookShopService.Domain.Models
{
    public interface IBookRepository
    {
        public void Create(Book book);
        public Book? Read(Int64 id);
        public IEnumerable<Book> ReadAll(string sortBy);
        public void Update(Book bookToUpdate);
        public void Delete(Int64 id);
        public bool SaveChanges();
    }
}