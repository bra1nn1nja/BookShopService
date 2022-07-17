using Xunit;
using Moq;
using BookShopService.API.Controllers;
using BookShopService.API.DTOs;
using BookShopService.API.Facades;
using Shouldly;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BookShopService.Tests.Controllers
{
    public class BooksControllerTests
    {
        [Fact]
        public void Create_Successful_BookCreated()
        {
            // Assemble
            var newBook = new CommonBook
            {
                Author = "Dennis E. Taylor",
                Title = "We are legion",
                Price = 5.99M
            };
            var newId = 4;
            var expectedContent = JsonSerializer.Serialize(new { id = newId });       
            var test = SetupCreateTest(newId);
            // Act
            var result = test.CreateBook(newBook);
            // Assert
            result.Result.ShouldBeOfType<CreatedAtRouteResult>();
            var createdResult = ((CreatedAtRouteResult)result.Result);
            createdResult.Value.ShouldNotBeNull();
            var content = createdResult.Value;
            JsonSerializer.Serialize(content).ShouldBe(expectedContent);
        }

        [Fact]
        public void Create_MissingTitle_ReturnsError()
        {
            // Assemble
            var newBook = new CommonBook();
            var newId = 4;
            var expectedContent = JsonSerializer.Serialize(new RequestErrors { errors = new string[]{ "Title is required" } });
            var test = SetupCreateTest(newId);
            test.ModelState.AddModelError("Title", "Title is required");
            // Act
            var result = test.CreateBook(newBook);
            // Assert
            result.Result.ShouldBeOfType<BadRequestObjectResult>();
            var badRequestResult = ((BadRequestObjectResult)result.Result);
            badRequestResult.Value.ShouldNotBeNull();
            var content = badRequestResult.Value;
            JsonSerializer.Serialize(content).ShouldBe(expectedContent);
        }

        [Fact]
        public void GetBooks_Successful_ReturnsBookList()
        {
            // Assemble
            SetupGetTest();
            var test = SetupGetTest();
            // Act
            var result = test.GetBooks(null);
            // Assert
            result.Result.ShouldBeOfType<OkObjectResult>();
            var okResult = ((OkObjectResult)result.Result);
            okResult.Value.ShouldNotBeNull();
            var content = ((List<UniqueBook>)okResult.Value);
            content.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public void GetBookById_Successful_ReturnsBook()
        {
            // Assemble
            var test = SetupGetTest(true);
            // Act
            var result = test.GetBookById(1);
            // Assert
            result.Result.ShouldBeOfType<OkObjectResult>();
            var okResult = ((OkObjectResult)result.Result);
            okResult.Value.ShouldNotBeNull();
            var content = ((CommonBook)okResult.Value);
            content.ShouldNotBeNull();
        }

        [Fact]
        public void GetBookById_NotFound_ReturnsNotFound()
        {
            // Assemble
            var test = SetupGetTest(false);
            // Act
            var result = test.GetBookById(1);
            // Assert
            result.Result.ShouldBeOfType<NotFoundResult>();
        }

        [Fact]
        public void PutBookById_Successful_ReturnsOk()
        {
            // Assemble
            var book = new CommonBook
            {
                Author = "Dennis E. Taylor",
                Title = "Bobiverse",
                Price = 5.99M
            };
            var test = SetupPutTest(true);
            // Act
            var result = test.PutBookById(1, book);
            // Assert
            result.ShouldBeOfType<OkResult>();
        }

        [Fact]
        public void PutBookById_MissingTitle_ReturnsError()
        {
            // Assemble
            var expectedContent = JsonSerializer.Serialize(
                new RequestErrors { errors = new string[] { "Title is required" } });
            var test = SetupPutTest(true);
            test.ModelState.AddModelError("Title", "Title is required");
            // Act
            var result = test.PutBookById(1, new CommonBook());
            // Assert
            result.ShouldBeOfType<BadRequestObjectResult>();
            var badRequestResult = ((BadRequestObjectResult)result);
            badRequestResult.Value.ShouldNotBeNull();
            var content = badRequestResult.Value;
            JsonSerializer.Serialize(content).ShouldBe(expectedContent);
        }

        [Fact]
        public void PutBookById_NotFound_ReturnsNotFound()
        {
            // Assemble
            var test = SetupPutTest(false);
            // Act
            var result = test.PutBookById(1, new CommonBook());
            // Assert
            result.ShouldBeOfType<NotFoundResult>();
        }

        [Fact]
        public void DeleteBookById_Successful_ReturnsOk()
        {
            // Assemble
            var test = SetupDeleteTest(true);
            // Act
            var result = test.DeleteBookById(1);
            // Assert
            result.ShouldBeOfType<OkResult>();
        }

        [Fact]
        public void DeleteBookById_NotFound_ReturnsNotFound()
        {
            // Assemble
            var test = SetupDeleteTest(false);
            // Act
            var result = test.DeleteBookById(1);
            // Assert
            result.ShouldBeOfType<NotFoundResult>();
        }

        private BooksController SetupCreateTest(int createdId)
        {
            var facade = new Mock<IBookShopFacade>();
            facade
                .Setup(x => x.Create(It.IsAny<CommonBook>()))
                .Returns(createdId);

            return new BooksController(facade.Object);
        }

        private BooksController SetupGetTest()
        {
            var facade = new Mock<IBookShopFacade>();
            facade
                .Setup(x => x.ReadBooks(It.IsAny<string>()))
                .Returns(
                    new List<UniqueBook>
                    {
                        new UniqueBook
                        {
                            Id = 1,
                            Author = "Dennis E. Taylor",
                            Title = "We are legion",
                            Price = 5.99M
                        }
                    }
                );

            return new BooksController(facade.Object);
        }

        private BooksController SetupGetTest(bool isFound)
        {
            var facade = new Mock<IBookShopFacade>();

            if (isFound)
            {
                var book =
                    new CommonBook
                    {
                        Author = "Dennis E. Taylor",
                        Title = "We are legion",
                        Price = 5.99M
                    };
                facade
                    .Setup(x => x.ReadBook(It.IsAny<Int64>()))
                    .Returns(book);
            }
            else
            {
                facade
                    .Setup(x => x.ReadBook(It.IsAny<Int64>()));
            }

            return new BooksController(facade.Object);
        }

        private BooksController SetupPutTest(bool isFound)
        {
            var facade = new Mock<IBookShopFacade>();

            if(isFound)
            {
                facade
                    .Setup(x => x.Update(It.IsAny<Int64>(), It.IsAny<CommonBook>()))
                    .Returns(true);
            }
            else
            {
                facade
                    .Setup(x => x.Update(It.IsAny<Int64>(), It.IsAny<CommonBook>()))
                    .Throws<KeyNotFoundException>();
            }

            return new BooksController(facade.Object);
        }

        private BooksController SetupDeleteTest(bool isFound)
        {
            var facade = new Mock<IBookShopFacade>();

            if(isFound)
            {
                facade
                    .Setup(x => x.Delete(It.IsAny<Int64>()))
                    .Returns(true);
            }
            else
            {
                facade
                    .Setup(x => x.Delete(It.IsAny<Int64>()))
                    .Throws<KeyNotFoundException>();
            }

            return new BooksController(facade.Object);
        }
    }
}