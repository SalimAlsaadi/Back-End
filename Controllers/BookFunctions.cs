using LibraryAPI.Modules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LibraryAPI.Controllers
{


    [Route("api/book")]
    [ApiController]
    public class BookFunctions:ControllerBase
    {

        public static ApplicationContext _context;

        public BookFunctions(ApplicationContext DB)
        {
            _context = DB;
        }


        //[Authorize]
        [HttpPost]
        public ActionResult addBooks(Book book)
        {
            //Book books=new Book();
            if (ModelState.IsValid)
            {
                
                _context.books.Add(book);
                try
                {
                    _context.SaveChanges();
                    Log.Information("a new book is added " + book.bookName);//to get information in log file
                    return Ok("Book Saved");
                }
                catch (Exception ex) { 
                    Log.Error("an error occured "+ex.Message);//to get information in log file
                    return BadRequest(ex.Message);
                }
                
            }
            else
            {
                return BadRequest("Data not Valid");
            }
            
        }

        [HttpPut]
        public ActionResult updateBooks(int bookId,string BookName,string author,string pub_year,string status,int price )
        {
            Book book = _context.books.FirstOrDefault(x=>x.bookId == bookId);
            if (book != null)
            {

                book.bookName = BookName;
                book.author = author;
                book.pub_year = pub_year;
                book.available_status = status;
                book.price = price;
                _context.books.Update(book);
                _context.SaveChanges();
                return Ok("book updated");
            }
            else
            {
                return NotFound("Book not found");
            }
        }

       // [Authorize]
        [HttpPut("updateToNotAvailable")]
        public void updateToNotAvailable(int bookId)
        {
            Book book = _context.books.SingleOrDefault(x=>x.bookId==bookId);
            if (book != null)
            {
                book.available_status = "Not available";
                _context.books.Update(book);
                _context.SaveChanges();
            }
        }


        //[Authorize]
        [HttpPut("updateToAvailable")]
        public void updateToAvailable(int bookId)
        {
            Book book = _context.books.SingleOrDefault(x => x.bookId == bookId);
            if (book != null)
            {
                book.available_status = "available";
                _context.books.Update(book);
                _context.SaveChanges();
            }
        }

        

        //[Authorize]
        [HttpDelete]
        public ActionResult removeBook(int bookId)
        {
            Book book = _context.books.SingleOrDefault(x => x.bookId == bookId);
            if (book != null)
            {
                _context.books.Remove(book);
                _context.SaveChanges();
                return Ok("Book Removed");
            }
            else
            {
                return NotFound("Book Not Found");
            }
            
        }


       // [Authorize]
        [HttpGet]
        public ActionResult getBooks()
        {
            List<Book> books = _context.books.Where(x => x.available_status == "available").ToList();
            if (books != null)
            {
                return Ok(books);
            }
            else
            {
                return NotFound("No Book Found Available");
            }
            
        }


        //[Authorize]
        [HttpGet("GetBookByID")]
        public ActionResult getBookName(int bookId)
        {
            Book book = _context.books.SingleOrDefault(x => x.bookId == bookId);
            if (book != null)
            {
                return Ok(book);
            }
            else
            {
                return NotFound("Book Not Found");
            }
            
        }

        //[Authorize]
        [HttpGet("GetBookByAuthor")]
        public ActionResult getBookByAuthor(string author)
        {
            List<Book> book = _context.books.Where(x => x.author == author).ToList();
            if(book != null)
            {
                return Ok(book);
            }
            else
            {
                return NotFound("No Books Found By Author");
            }
            
        }

       // [Authorize]
        [HttpGet("Get Number Of Book Done By Author")]
        public ActionResult getBookNumbers(string auther)
        {
            int bookNum=_context.books.Where(x=>x.author == auther).Count();
            if(bookNum > 0)
            {
                return Ok(bookNum);

            }
            else
            {
                return NotFound("Author has No Book Or Author Name not Accurate");
            }
        }

        [HttpGet("getBooksContainingLetter")]
        public ActionResult getBooksContainingLetter(string letter)
        {
            List<Book> books = _context.books.Where(x => x.bookName.StartsWith(letter)).ToList();   
            if (books != null && books.Count > 0)
            {
                return Ok(books);
            }
            else
            {
                return NotFound("No books containing the specified letter found");
            }
        }

    }
}
