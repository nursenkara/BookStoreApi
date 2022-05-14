using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Book> BookList = new List<Book>()
            {
            new Book { Id = 1,
            Title = "Me Before You",
            GenreId = 1,//Love
            PageCount = 500,
            PublishedDate = new DateTime(2014,03,04)
            },
            new Book { Id = 2,
            Title = "Harry Potter",
            GenreId = 2,//Adventure
            PageCount = 250,
            PublishedDate = new DateTime(2000,01,05)
            },
            new Book { Id = 3,
            Title = "Lord of The Rings",
            GenreId = 2,//Adventure
            PageCount = 1200,
            PublishedDate = new DateTime(2003,07,25)
            }
            };
        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }
        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = BookList.Where(x => x.Id == id).SingleOrDefault();
            return book;
        }
        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book =  BookList.SingleOrDefault(x => x.Title == newBook.Title);
            if(book is not null)
            {
                return BadRequest();
            }
            BookList.Add(newBook);
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = BookList.SingleOrDefault(x => x.Id == id);
            if(book is null)
            {
                return BadRequest();
            }
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishedDate = updatedBook.PublishedDate != default ? updatedBook.PublishedDate :book.PublishedDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
            return Ok();

        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = BookList.SingleOrDefault(x => x.Id == id);
            if (book is null)
            {
                return BadRequest();
            }
            BookList.Remove(book);
            return Ok();
        }
        [HttpGet("Search")]
        public Book Search(string title)
        {
            var book = BookList.Where(x => x.Title == title).SingleOrDefault();

            return book;


            
        }

    }
}
