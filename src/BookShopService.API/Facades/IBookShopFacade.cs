using BookShopService.API.DTOs;

namespace BookShopService.API.Facades
{
    public interface IBookShopFacade
    {
        public Int64 Create(CommonBook newBook);
        public IEnumerable<UniqueBook> ReadBooks(string sortby);
        public CommonBook ReadBook(Int64 id);
        public bool Update(Int64 id, CommonBook bookToUpdate);
        public bool Delete(Int64 id);
    }
}