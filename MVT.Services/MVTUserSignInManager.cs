using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using MVT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
 

namespace MVT.Services
{
     
  
        public class MVTSignInManager : SignInManager<MVTUsers, string>
        {
            public MVTSignInManager(MVTUserManager userManager, IAuthenticationManager authenticationManager)
                : base(userManager, authenticationManager)
            {
            }
          
            public override Task<ClaimsIdentity> CreateUserIdentityAsync(MVTUsers user) 
            {
                return user.GenerateUserIdentityAsync((MVTUserManager)UserManager);
            }

            public static MVTSignInManager Create(IdentityFactoryOptions<MVTSignInManager> options, IOwinContext context)
            {
                return new MVTSignInManager(context.GetUserManager<MVTUserManager>(), context.Authentication);
            }
        }
    }

