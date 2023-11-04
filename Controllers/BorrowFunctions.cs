using LibraryAPI.Modules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace LibraryAPI.Controllers
{

    [Route("api/Borrow")]
    [ApiController]
    public class BorrowFunctions:ControllerBase
    {
        public static ApplicationContext _context;

        public static BookFunctions _bookFunctions;//create object from BookFunction

        HttpClient _httpClient;

        public BorrowFunctions(ApplicationContext DB)
        {
            _context = DB;
            _bookFunctions = new BookFunctions(DB);

            //to make http connection client to bank  system
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            _httpClient = new HttpClient(clientHandler);
            _httpClient.BaseAddress = new Uri("https://localhost:7111/");
        }


       // [Authorize]
        [HttpPost]
        public  async Task<ActionResult> borrowBook(int patronId, int bookId, int BankCustID, string Acc, double price )
        {
            Book book = _context.books.SingleOrDefault(x => x.bookId == bookId);
            if (book != null && book.available_status == "available")
            {

                DateTime dateTime = DateTime.Now;
                Borrow borrow = new Borrow();
                borrow.patronId = patronId;
                borrow.bookId = bookId;
                borrow.borrowDate = dateTime.ToString();
                borrow.returnDate = dateTime.AddDays(7).ToString();
                string Uri = $"api/Transaction/WithDraw?ID={BankCustID}&account={Acc}&price={price}";

                HttpResponseMessage httpResponse = _httpClient.PostAsJsonAsync(Uri,new {}).Result;//after we prepar the http client connection in constructor above.
                                                                                                  //then here we will send request to bank system
                

                if(httpResponse.IsSuccessStatusCode)
                {
                    _bookFunctions.updateToNotAvailable(bookId);
                    _context.borrowFunctions.Add(borrow);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }

                try
                {
                    _context.SaveChanges();
                    return Ok("Done");
                }

                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
                
            }
            else
            {
                return NotFound("Book Not Available");
            }


        }


        [Authorize]
        [HttpPut]
        public ActionResult ReturnBook(int bookId)
        {

            Borrow returnedBook = _context.borrowFunctions.SingleOrDefault(x => x.bookId == bookId);

            if (returnedBook != null )
            {
                DateTime dateTime = DateTime.Now;
                returnedBook.returnDate = dateTime.ToString();
                _bookFunctions.updateToAvailable(bookId);
                _context.borrowFunctions.Update(returnedBook);
                _context.SaveChanges();
                return Ok("Done");
            }
            else
            {
                return NotFound("Book Not Found");
            }
        }


        [Authorize]
        [HttpPut("update Return Date")]
        public ActionResult updateReturnDate(int bookId, int days)
        {
            Borrow book = _context.borrowFunctions.SingleOrDefault(x => x.bookId == bookId);

            if (book != null)
            {
                DateTime dateTime = DateTime.Now;
                book.returnDate =dateTime.AddDays(days).ToString();
                _context.borrowFunctions.Update(book);
                _context.SaveChanges();
                return Ok("return Date Updated");
            }
            else
            {

                return NotFound("Details Not Found");
            }
        }


        [Authorize]
        [HttpGet]
            public ActionResult getAllTRansForPatron(int patronId)
            {
                List<Borrow> ret = _context.borrowFunctions.Where(x => x.patronId == patronId).ToList();
                if (ret != null)
                {
                    return Ok(ret);
                }
                else
                {
                    return NotFound("Not Found");
                }

            }
        }


    }

