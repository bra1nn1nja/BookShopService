using BookShopService.Domain.Models;

namespace BookShopService.Infrastructure.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;
        public BookRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Create(Book book)
        {
            _ = book ?? throw new ArgumentNullException(nameof(book));

            _context.Books?.Add(book);
        }
        public Book? Read(Int64 id)
        {
            return _context?.Books?.FirstOrDefault(p => p.Id == id);
        }
        public IEnumerable<Book> ReadAll(string sortBy)
        {
            return sortByField(sortBy, _context?.Books?.ToList());
        }
        public void Update(Book bookToUpdate)
        {
            if(bookToUpdate != null)
            {
                _context?.Update(bookToUpdate);
            }
        }
        public void Delete(Int64 id)
        {
            var bookToRemove = _context?.Books?.FirstOrDefault(p => p.Id == id);
            if(bookToRemove != null)
            {
                _context.Remove(bookToRemove);
            }       
        } 
        
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        private List<Book> sortByField(string field, IEnumerable<Book> unsorted)
        {
            if(unsorted != null)
            {
                switch(field)
                {
                    case "price":
                        return unsorted.OrderBy(x => x.Price).ToList();
                    case "author":
                        return unsorted.OrderBy(x => x.Author).ToList();
                    default:
                        return unsorted.OrderBy(x => x.Title).ToList();

                }
            }

            return new List<Book>();
        } 
    }
}