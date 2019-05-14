namespace Veterinary.Web.Data
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Veterinary.Web.Data.Entities;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly UserManager<User> userManager;

        public SeedDb(DataContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();
            await this.CheckSuperUser();
            await this.CheckOwners();
            await this.CheckAgendas();
        }

        private async Task CheckAgendas()
        {
            if (!this.context.Agendas.Any())
            {
                var initialDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
                var finalDate = initialDate.AddYears(1);
                while (initialDate < finalDate)
                {
                    if (initialDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        var finalDate2 = initialDate.AddHours(10);
                        while (initialDate < finalDate2)
                        {
                            this.context.Agendas.Add(new Agenda
                            {
                                Date = initialDate,
                                IsAvailable = true
                            });

                            initialDate = initialDate.AddMinutes(30);
                        }

                        initialDate = initialDate.AddHours(14);
                    }
                    else
                    {
                        initialDate = initialDate.AddDays(1);
                    }
                }

                await this.context.SaveChangesAsync();
            }
        }

        private async Task CheckSuperUser()
        {
            var user = await this.userManager.FindByEmailAsync("lauraibata04@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Laura",
                    LastName = "Ibata",
                    Email = "lauraibata04@gmail.com",
                    UserName = "lauraibata04@gmail.com"
                };

                var result = await this.userManager.CreateAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }
        }

        private async Task CheckOwners()
        {
            if (!this.context.Owners.Any())
            {
                this.AddOwner("8989898", "Laura", "Ibata", "234 3232", "310 322 3221", "Calle Luna Calle Sol");
                this.AddOwner("7655544", "Jose", "Cardona", "343 3226", "300 322 3221", "Calle 77 #22 21");
                this.AddOwner("6565555", "Maria", "López", "450 4332", "350 322 3221", "Carrera 56 #22 21");
                await this.context.SaveChangesAsync();
            }
        }

        private void AddOwner(string document, string firstName, string lastName, string fixedPhone, string cellPhone, string address)
        {
            this.context.Owners.Add(new Owner
            {
                Address = address,
                CellPhone = cellPhone,
                Document = document,
                FirstName = firstName,
                FixedPhone = fixedPhone,
                LastName = lastName
            });
        }
    }
}
