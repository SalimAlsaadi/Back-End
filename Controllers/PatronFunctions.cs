using LibraryAPI.Modules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Globalization;

namespace LibraryAPI.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UserFunctions:ControllerBase
    {

        public static ApplicationContext _context;

        public UserFunctions(ApplicationContext DB)
        {
            _context = DB;
        }

        //[Authorize]
        [HttpPost]
        public ActionResult addUser(string userName,string Email,string Password,string phoneNumber,int age,string bank_account,string user_type) { 
        
            User user=new User();
            user.UserName=userName;
            user.UserEmail = Email;
            user.UserPassword=Password;
            user.phoneNumber=phoneNumber;
            user.age=age;
            user.bank_account=bank_account;
            user.user_type=user_type;

            if (ModelState.IsValid)
            {
              
                _context.users.Add(user);

                try
                {
                    _context.SaveChanges();
                    Log.Information($"a new user {user.UserName} added successfull ");//to get this information in log file
                    return Ok("new user saved");
                }
                catch (Exception ex)
                {
                    Log.Error("an error occured when user try to add new patron "+ex.Message);//to get this information in log file
                    return BadRequest(ex.Message);
                }
                
            }
            else
            {
                return BadRequest("check data input");
            }
            
            
        }


       // [Authorize]
        [HttpGet]
        public ActionResult getusers()
        {

            List<User> users = _context.users.ToList();
            if (users != null)
            {
                return Ok(users);
            }
            else
            {
                return NotFound("No users in database");
            }
            
        }


        //[Authorize]
        [HttpGet("GetUserByName")]
        public ActionResult getUser(string name)
        {
            User user = _context.users.SingleOrDefault(x => x.UserName == name);
            if (user != null)
            {
                Log.Information($"Employee requsted user name {name}");//to get information to log file

                return Ok(user);
            }
            else
            {
                return NotFound("Not Found User");
            }
            
        }

        [HttpGet("getUesrByEmail")]
        public ActionResult getUserByID(string pass,String Email)
        {
            User user = _context.users.SingleOrDefault(x=>x.UserEmail == Email && x.UserPassword==pass);
            if(user != null)
            {
                return Ok(user);
            }
            else { return NotFound("Not Found User"); }

        }


       // [Authorize]
        [HttpGet("Get Number of Users")]
        public ActionResult getNumbersOfUsers()
        {
            int num=_context.users.Count();
            return Ok(num);
        }


        [Authorize]
        [HttpPut]
        public ActionResult updateUserContact(string name,string phoneNumer)
        {
            User user = _context.users.SingleOrDefault(x => x.UserName == name);
            if (user != null)
            {
                //Patron patronUpdater = new Patron();
                //Console.Write("Enter phone Number:");
                //patron.phoneNumber = Console.ReadLine();
                user.phoneNumber = phoneNumer;
                _context.users.Update(user);
                _context.SaveChanges();
                return Ok(user);
            }
            else
            {
                return NotFound("User Not Found");
            }
        }


        [Authorize]
        [HttpDelete]
        public ActionResult removeUser(string name)
        {
            User user = _context.users.SingleOrDefault(x => x.UserName == name);
            if (user != null)
            {
                _context.users.Remove(user);
                _context.SaveChanges();
                return Ok("Done");
            }
            else{

                return NotFound("User Not Found");
            }
        }
    }
}
