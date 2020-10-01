using FitnessAPI.Data;
using FitnessAPI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace FitnessAPI.Handlers
{
    
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly FitnessContext _context;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            FitnessContext context)
            : base(options, logger, encoder, clock)
        {
            _context = context;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Authorization header not found.");
            try
            {
                //Reads authorization header value from header, and saving into a var.
                var authenticationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                //converts to UTF8 charset
                var bytes = Convert.FromBase64String(authenticationHeaderValue.Parameter);
                //Splits the email:password string at the : char
                string[] credentials = Encoding.UTF8.GetString(bytes).Split(":");

                string emailAddress = credentials[0];
                string password = credentials[1];

                //Checks if the authentication passes or not
                User user = _context.Users.Where(user => user.Email == emailAddress && user.Password == password).FirstOrDefault();

                if (user == null) //No user with that name or passowrd
                {
                    return AuthenticateResult.Fail("Invalid username or password");
                } 
                else // Valid user, sets up successful authentication
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, user.Email) };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }
            } 
            catch (Exception)
            {   
                // Epic fail LOL
                return AuthenticateResult.Fail("Error occured");
            }
        }
    }
}