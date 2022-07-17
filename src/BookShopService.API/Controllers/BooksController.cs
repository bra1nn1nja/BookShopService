using Microsoft.AspNetCore.Mvc;
using BookShopService.API.DTOs;
using BookShopService.API.Helpers;
using BookShopService.API.Facades;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using BookShopService.API.Swagger.Annotations.ResponseExamples;

namespace BookShopService.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookShopFacade _facade;

        public BooksController(
            IBookShopFacade facade)
        {
            _facade = facade;
        }

        /// <summary>
        /// Creates a new book.
        /// </summary>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RequestErrors), 400)]
        [SwaggerResponse(201, "Created")]
        [SwaggerResponseExample(201, typeof(CreatedResponseExample))]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponseExample(400, typeof(BadRequestResponseExample))]
        public ActionResult<UniqueBook> CreateBook([FromBody, SwaggerRequestBody("A JSON object that represents a book.", Required = true)] CommonBook newBook)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ErrorHelper.ExtractFromState(ModelState));
            }

            try
            {
                var id = _facade.Create(newBook);

                return CreatedAtRoute(
                    nameof(GetBookById), 
                    new { id = id },
                    new { id = id });
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Returns a list of books. Sorted by title by default.
        /// </summary>
        /// <param name="sortby">Available values : title, author, price</param>
        [HttpGet]
        [Produces("application/json")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponseExample(200, typeof(ReadBookResponseExample))]
        public ActionResult<IEnumerable<UniqueBook>> GetBooks(string? sortby) => Ok(_facade.ReadBooks(sortby));

        /// <summary>
        /// Update an existing book.
        /// </summary>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request", typeof(RequestErrors))]
        [SwaggerResponseExample(400, typeof(BadRequestResponseExample))]
        [SwaggerResponse(404, "Book not found")]
        public ActionResult PutBookById(Int64 id, [FromBody, SwaggerRequestBody("A JSON object that represents a book.", Required = true)] CommonBook bookToUpdate)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ErrorHelper.ExtractFromState(ModelState));
            }

            try
            {
                _facade.Update(id, bookToUpdate);

                return Ok();
            }
            catch(KeyNotFoundException)
            {
                return NotFound();
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Gets a book by id.
        /// </summary>
        [HttpGet("{id}", Name = "GetBookById")]
        [Produces("application/json")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponseExample(200, typeof(UnreferencedBookResponseExample))]
        [SwaggerResponse(404, "Book not found")]
        public ActionResult<CommonBook> GetBookById(Int64 id)
        {
            var book = _facade.ReadBook(id);

            return book != null ? Ok(book) : NotFound();
        }

        /// <summary>
        /// Deletes a book by id.
        /// </summary>
        [HttpDelete("{id}")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(404, "Book not found")]
        public ActionResult DeleteBookById(Int64 id)
        {
            try
            {
                _facade.Delete(id);

                return Ok();
            }
            catch(KeyNotFoundException)
            {
                return NotFound();
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}