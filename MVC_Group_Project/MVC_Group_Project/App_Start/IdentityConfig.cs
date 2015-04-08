﻿using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using MVC_Group_Project.Models;
using System.Data.Entity;
using System.Web;

namespace MVC_Group_Project
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    //public class ApplicationUserManager : UserManager<ApplicationUser>
    //{
    //    public ApplicationUserManager(IUserStore<ApplicationUser> store)
    //        : base(store)
    //    {
    //    }

    //    public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
    //    {
    //        var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
    //        // Configure validation logic for usernames
    //        manager.UserValidator = new UserValidator<ApplicationUser>(manager)
    //        {
    //            AllowOnlyAlphanumericUserNames = false,
    //            RequireUniqueEmail = true
    //        };
    //        // Configure validation logic for passwords
    //        manager.PasswordValidator = new PasswordValidator
    //        {
    //            RequiredLength = 6,
    //            RequireNonLetterOrDigit = true,
    //            RequireDigit = true,
    //            RequireLowercase = true,
    //            RequireUppercase = true,
    //        };
    //        // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
    //        // You can write your own provider and plug in here.
    //        manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<ApplicationUser>
    //        {
    //            MessageFormat = "Your security code is: {0}"
    //        });
    //        manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<ApplicationUser>
    //        {
    //            Subject = "Security Code",
    //            BodyFormat = "Your security code is: {0}"
    //        });
    //        manager.EmailService = new EmailService();
    //        manager.SmsService = new SmsService();
    //        var dataProtectionProvider = options.DataProtectionProvider;
    //        if (dataProtectionProvider != null)
    //        {
    //            manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
    //        }
    //        return manager;
    //    }
    //}

    //public class EmailService : IIdentityMessageService
    //{
    //    public Task SendAsync(IdentityMessage message)
    //    {
    //        // Plug in your email service here to send an email.
    //        return Task.FromResult(0);
    //    }
    //}

    //public class SmsService : IIdentityMessageService
    //{
    //    public Task SendAsync(IdentityMessage message)
    //    {
    //        // Plug in your sms service here to send a text message.
    //        return Task.FromResult(0);
    //    }
    //}

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

    }

    // Configure the RoleManager used in the application. RoleManager is defined in the ASP.NET Identity core assembly
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>()));
        }
    }

    // This creates a new database if the Model changes
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeData(context);
            InitializeIdentity(context);
            base.Seed(context);
        }

        public static void InitializeData(ApplicationDbContext context)
        {
        }

        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        public static void InitializeIdentity(ApplicationDbContext db)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();

            var firstName = "Wesley";
            var lastName = "Gibson";
            var address = "123 Madra Street";
            var email = "admin@robertsoncollege.net";
            var password = "123456As!";
            var roleName = "Admin";

            CreateUserAndRole(firstName, lastName, address, email, password, roleName, userManager, roleManager);

            firstName = "Mark";
            lastName = "Jones";
            address = "145 Less Street";
            email = "member@robertsoncollege.net";
            password = "123456As#";
            roleName = "Member";
            CreateUserAndRole(firstName, lastName, address, email, password, roleName, userManager, roleManager);
        }

        private static void CreateUserAndRole(string firstname, string lastname, string address, string email, string password, string roleName, ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            //Create Role if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            var user = userManager.FindByName(email);
            if (user == null)
            {
                user = new ApplicationUser { UserName = email, Email = email };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user to Role if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }

    }

}
