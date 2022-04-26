using Microsoft.AspNetCore.Identity;
using RenginiaiWebApp.Data.Enum;
using RenginiaiWebApp.Models;

namespace RenginiaiWebApp.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Clubs.Any())
                {
                    context.Clubs.AddRange(new List<Club>()
                    {
                        new Club()
                        {
                            Title = "Music fans club",
                            Image = "https://images.unsplash.com/photo-1587731556938-38755b4803a6?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1178&q=80",
                            Description = "24/7 everyday join now",
                            ClubCategory = ClubCategory.Music,
                            Address = new Address()
                            {
                                Street = "Example g. 123",
                                City = "Paris"
                            }
                         },
                        new Club()
                        {
                            Title = "REACT",
                            Image = "https://images.unsplash.com/photo-1633356122544-f134324a6cee?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80",
                            Description = "REACT AND MORE",
                            ClubCategory = ClubCategory.Programming,
                            Address = new Address()
                            {
                                Street = "Example g. 123",
                                City = "Paris"
                            }
                        },
                        new Club()
                        {
                            Title = "Volleyball lovers!",
                            Image = "https://images.unsplash.com/photo-1588492069485-d05b56b2831d?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1171&q=80",
                            Description = "Join volleyball club!!",
                            ClubCategory = ClubCategory.Sports,
                            Address = new Address()
                            {
                                Street = "Example g. 123",
                                City = "Paris"
                            }
                        },
                        new Club()
                        {
                            Title = "Basketball club",
                            Image = "https://images.unsplash.com/photo-1628779238951-be2c9f2a59f4?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=687&q=80",
                            Description = "Ballers",
                            ClubCategory = ClubCategory.Sports,
                            Address = new Address()
                            {
                                Street = "Example g. 123",
                                City = "Paris"
                            }
                        }
                    });
                    context.SaveChanges();
                }
                //Events
                if (!context.Events.Any())
                {
                    context.Events.AddRange(new List<Event>()
                    {
                        new Event()
                        {
                            Title = "Electronic music event",
                            Image = "https://images.unsplash.com/photo-1459749411175-04bf5292ceea?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80",
                            Description = "100% crazy",
                            EventCategory = EventCategory.ThreeHours,
                            Address = new Address()
                            {
                                Street = "Example g. 123",
                                City = "Paris"
                            }
                        },
                        new Event()
                        {
                            Title = "Cycling event",
                            Image = "https://images.unsplash.com/photo-1600403477955-2b8c2cfab221?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80",
                            Description = "50 km event",
                            EventCategory = EventCategory.ThreeHours,
                            AddressId = 5,
                            Address = new Address()
                            {
                                Street = "Example g. 123",
                                City = "Paris"
                            }

                        }
                    });
                    context.SaveChanges();
                }
            }

        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "admin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Paris"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "admin");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "user@gmail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Paris"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "user");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
