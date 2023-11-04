using LibraryAPI.Modules;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController:ControllerBase
    {
        public static ApplicationContext _context;

        public LoginController(ApplicationContext DB)
        {
            _context = DB;
        }


        [HttpPost]
        public IActionResult checkLogin(string password, string email)
        {
            User user=_context.users.Where(x=>x.UserPassword == password && x.UserEmail==email).FirstOrDefault();

            if(user != null)  //this method will create token if id nad email is correct.
            {

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));//this is KEY or SIGNATURE to encrypt user login data
                                                                                                                                      //(we can say this is key or way send to user to ask him to use this key to encrypt login data,
                                                                                                                                      //but this key i have to encrypt it to be unknown from third side, so i have to use algorithm to encrypt this key)

                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);//this is algorithm, will use to encrypt the key when I send the key to user to use that key to encrypt login data


                //here is details about users if i need and it will save in payload in token(algorithm,payload,key) for be know who is user enter in my system.
                var data = new List<Claim>();
                data.Add(new Claim("name: ", user.UserName));
                data.Add(new Claim("Email: ", user.UserEmail));    
                data.Add(new Claim("ID: ", user.UserId.ToString()));    


                //here the final to create Token(this token will send on header)
                var token = new JwtSecurityToken(
                 issuer: "Salim",        //name who create a token
                 audience: "TRA",       //this token must used by TRA
                 claims: data,          //payload(details for user)
                 expires: DateTime.Now.AddMinutes(120),      //this session will kill after some time
                 signingCredentials: credentials     //this key with algorithm

               );

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));//here will send final token to user
                
            }
            else
            {
                return Unauthorized("the user doesn't exist");
            }
        }
    }
}
