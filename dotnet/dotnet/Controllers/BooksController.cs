using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private static List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Borrowed = false },
            new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", Borrowed = false },
            new Book { Id = 3, Title = "1984", Author = "George Orwell", Borrowed = false },
        };

        // GET /books
        [HttpGet]
        public IEnumerable<Book> GetBooks()
        {
            return books;
        }

        // GET /books/1
        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        // PUT /books/1/borrow
        [HttpPut("{id}/borrow")]
        public IActionResult BorrowBook(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            if (book.Borrowed)
            {
                return BadRequest("Book is already borrowed");
            }
            book.Borrowed = true;
            return Ok("Book successfully borrowed");
        }

        // PUT /books/1/return
        [HttpPut("{id}/return")]
        public IActionResult ReturnBook(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            if (!book.Borrowed)
            {
                return BadRequest("Book is not currently borrowed");
            }
            book.Borrowed = false;
            return Ok("Book successfully returned");
        }

        // POST /books
        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            if (book == null || string.IsNullOrWhiteSpace(book.Title) || string.IsNullOrWhiteSpace(book.Author))
            {
                return BadRequest("Missing required fields");
            }
            book.Id = books.Max(b => b.Id) + 1;
            book.Borrowed = false;
            books.Add(book);
            return Ok("Book successfully added");
        }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool Borrowed { get; set; }
    }
}