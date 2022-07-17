using AutoMapper;
using BookShopService.API.DTOs;
using BookShopService.Domain.Models;

namespace BookShopService.API.Facades
{
    public class BookShopFacade : IBookShopFacade
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _repository;
        public BookShopFacade(IMapper mapper, IBookRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public Int64 Create(CommonBook newBook)
        {
            var model = _mapper.Map<Book>(newBook);

            _repository.Create(model);
            _repository.SaveChanges();

            return model.Id;
        }

        public IEnumerable<UniqueBook> ReadBooks(string sortby)
        {
            var books = _repository.ReadAll(sortby);

            return _mapper.Map<IEnumerable<UniqueBook>>(books);
        }

        public CommonBook ReadBook(Int64 id)
        {
            var book = _repository.Read(id);

            return _mapper.Map<CommonBook>(book);
        }

        public bool Update(Int64 id, CommonBook bookToUpdate)
        {
            var existingBook = _repository.Read(id) ?? throw new KeyNotFoundException();

            var model = _mapper.Map(bookToUpdate, existingBook);
                _repository.Update(model);

            return _repository.SaveChanges();
        } 

        public bool Delete(Int64 id)
        {
            _ = _repository.Read(id) ?? throw new KeyNotFoundException();

            _repository.Delete(id);

            return _repository.SaveChanges();
        }
    }
}