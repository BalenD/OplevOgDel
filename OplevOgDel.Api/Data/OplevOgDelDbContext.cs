﻿using Microsoft.EntityFrameworkCore;
using OplevOgDel.Api.Data.Models;
using System;

namespace OplevOgDel.Api.Data
{
    public class OplevOgDelDbContext : DbContext
    {
        public DbSet<User>  Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ListOfExps> ListOfExperiences { get; set; }
        public DbSet<ExpCategory> Categories { get; set; }

        public OplevOgDelDbContext(DbContextOptions<OplevOgDelDbContext> options) : base(options)
        {

        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API
            // Many-to-Many between ListOfExp and Experience
            modelBuilder.Entity<ListOfExpsExperience>()
                .HasOne(le => le.ListOfExps)
                .WithMany(l => l.ListOfExpsExperiences)
                .HasForeignKey(le => le.ListOfExpsId);
            modelBuilder.Entity<ListOfExpsExperience>()
                .HasOne(le => le.Experience)
                .WithMany(l => l.ListOfExpsExperiences)
                .HasForeignKey(le => le.ExperienceId);

            // One-to-many between Profile and ListOfExp
            modelBuilder.Entity<ListOfExps>()
                .HasOne(l => l.Creator)
                .WithMany(p => p.ListOfExps)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-many between Profile and Rating
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Creator)
                .WithMany(p => p.Ratings)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-many between Profile and Review
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Creator)
                .WithMany(p => p.Reviews)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-many between Profile and Report
            modelBuilder.Entity<Report>()
                .HasOne(r => r.Creator)
                .WithMany(p => p.Reports)
                .OnDelete(DeleteBehavior.Restrict);

            // SEED DATA

            // ADMIN SEED DATA
            modelBuilder.Entity<Administrator>().HasData(
                new Administrator()
                {
                    Id = Guid.Parse("85416f64-0f0e-4b8d-8687-4f3b9cc6b40f"),
                    Username = "admin",
                    Password = "password",
                    Email = "admin@admin.dk"
                });

            // USER SEED DATA
            modelBuilder.Entity<User>().HasData(
                // USER 1
                new User()
                {
                    Id = Guid.Parse("5376bf6f-3b8c-443c-8c17-28071e8fd1ed"),
                    Username = "user1",
                    Password = "password",
                    Email = "user1@user.dk"
                },
                // USER 2
                new User()
                {
                    Id = Guid.Parse("fa303d07-af85-41c7-8455-29fd9ae6bc9e"),
                    Username = "user2",
                    Password = "password",
                    Email = "user2@user.dk"
                },
                // USER 3
                new User()
                {
                    Id = Guid.Parse("53e881e9-1b7a-461f-a286-48476deb343d"),
                    Username = "user3",
                    Password = "password",
                    Email = "user3@user.dk"
                });

            // PROFILE SEED DATA
            modelBuilder.Entity<Profile>().HasData(
                // USER 1
                new Profile()
                {
                    Id = Guid.Parse("9600bf95-bf37-4e6d-aeed-53d84a96a205"),
                    FirstName = "Jens",
                    LastName = "Olesen",
                    Gender = Models.Enums.Gender.Male,
                    Birthday = DateTime.Parse("15/06/1992"),
                    Age = 28,
                    City = "København",
                    UserId = Guid.Parse("5376bf6f-3b8c-443c-8c17-28071e8fd1ed")
                },
                // USER 2
                new Profile()
                {
                    Id = Guid.Parse("62357886-d888-44f2-a929-c015a4b31dad"),
                    FirstName = "Annita",
                    LastName = "Bech Jensen",
                    Gender = Models.Enums.Gender.Female,
                    Birthday = DateTime.Parse("21/08/1998"),
                    Age = 22,
                    City = "Humlebæk",
                    UserId = Guid.Parse("fa303d07-af85-41c7-8455-29fd9ae6bc9e")
                },
                // USER 3
                new Profile()
                {
                    Id = Guid.Parse("229f7d4f-ffcc-437d-b3ab-82a0096f9c43"),
                    FirstName = "Frederik",
                    LastName = "Skov Laursen",
                    Gender = Models.Enums.Gender.Male,
                    Birthday = DateTime.Parse("02/08/1995"),
                    Age = 25,
                    City = "Ballerup",
                    UserId = Guid.Parse("53e881e9-1b7a-461f-a286-48476deb343d")
                });

            // EXPCATEGORY SEED DATA
            modelBuilder.Entity<ExpCategory>().HasData(
                new ExpCategory()
                {
                    Id = Guid.Parse("199b5113-ae3b-47ce-adee-dcbac4935f88"),
                    Name = "Mad"
                },
                new ExpCategory()
                {
                    Id = Guid.Parse("45671c48-b7a2-4663-b826-044aeefd59ff"),
                    Name = "Natur"
                },
                new ExpCategory()
                {
                    Id = Guid.Parse("21015075-67a0-4f6e-8db7-b9eefd4361a8"),
                    Name = "Musik"
                },
                new ExpCategory()
                {
                    Id = Guid.Parse("af09b6cc-e3f0-4eae-bb89-ef36affd27d7"),
                    Name = "Action"
                },
                new ExpCategory()
                {
                    Id = Guid.Parse("b6f3f639-1450-4334-a123-2b1fb8c68808"),
                    Name = "Kultur"
                },
                new ExpCategory()
                {
                    Id = Guid.Parse("037d26c3-66c4-4017-9c54-ca8e72ae56fa"),
                    Name = "Historie"
                });

            // EXPERIENCE SEED DATA
            modelBuilder.Entity<Experience>().HasData(
                new Experience()
                {
                    Id = Guid.Parse("bd345b81-462b-4ba9-999f-48ff44fad5e8"),
                    Name = "Apollo Bar",
                    Description = "Apollo Bar er en simpel restaurant beliggende i baggården ved Charlottenborg Kunsthal. Apollo Bar tilbyder morgenmad, frokost og middag.",
                    City = "København",
                    Address = "Nyhavn 2",
                    ExpCategoryId = Guid.Parse("199b5113-ae3b-47ce-adee-dcbac4935f88"),
                    ProfileId = Guid.Parse("9600bf95-bf37-4e6d-aeed-53d84a96a205")
                },
                new Experience()
                {
                    Id = Guid.Parse("82a5a437-35b3-44b8-b10a-01d13577b7f1"),
                    Name = "Akustikken",
                    Description = "Akustikken er en guitarbutik, der både sælger og reparerer musikudstyr.",
                    City = "København",
                    Address = "Bagerstræde 7",
                    ExpCategoryId = Guid.Parse("21015075-67a0-4f6e-8db7-b9eefd4361a8"),
                    ProfileId = Guid.Parse("9600bf95-bf37-4e6d-aeed-53d84a96a205")
                },
                new Experience()
                {
                    Id = Guid.Parse("a030b459-a8b5-4bba-bcbd-b9a30176f7e4"),
                    Name = "Amager Bakke",
                    Description = "Amager Bakke er en kunstig skibakke, der ligger på toppen af det nye forbrændingsanlæg, Amager Ressource Center (ARC).",
                    City = "København",
                    Address = "Vindmøllevej 6",
                    ExpCategoryId = Guid.Parse("af09b6cc-e3f0-4eae-bb89-ef36affd27d7"),
                    ProfileId = Guid.Parse("9600bf95-bf37-4e6d-aeed-53d84a96a205")
                },
                new Experience()
                {
                    Id = Guid.Parse("c3965bec-3a76-40a9-b435-546d4cd2ad2f"),
                    Name = "Zoologisk Have",
                    Description = "Med sine godt 160 år på bagen er Zoologisk Have København en af Europas ældste zoologiske haver. Mere end 3000 dyr, fordelt på over 200 arter, har deres daglige gang i København Zoo.",
                    City = "Frederiksberg",
                    Address = "Roskildevej 32",
                    ExpCategoryId = Guid.Parse("b6f3f639-1450-4334-a123-2b1fb8c68808"),
                    ProfileId = Guid.Parse("9600bf95-bf37-4e6d-aeed-53d84a96a205")
                },
                new Experience()
                {
                    Id = Guid.Parse("a11d3b85-04d9-4665-bea8-91ac47f6a2d8"),
                    Name = "Kronborg Slot",
                    Description = "Kronborg er et af Danmarks mest betydningsfulde slotte og fuld af Danmarkshistorie.",
                    City = "Helsingør",
                    Address = "Kronborg 2 C",
                    ExpCategoryId = Guid.Parse("037d26c3-66c4-4017-9c54-ca8e72ae56fa"),
                    ProfileId = Guid.Parse("9600bf95-bf37-4e6d-aeed-53d84a96a205")
                },
                new Experience()
                {
                    Id = Guid.Parse("f574dea5-088b-4ecf-a0ba-439381cdfabf"),
                    Name = "Planetarium",
                    Description = "I Planetarium kan du opleve stjernehimlen, galakser og planeter helt tæt på - både i deres udstilling, på rumrejser i Kuppelsalen og til foredrag om astronomi og rumfart.",
                    City = "Gammel kongevej 10",
                    Address = "København",
                    ExpCategoryId = Guid.Parse("b6f3f639-1450-4334-a123-2b1fb8c68808"),
                    ProfileId = Guid.Parse("9600bf95-bf37-4e6d-aeed-53d84a96a205")
                },
                new Experience()
                {
                    Id = Guid.Parse("ce9ba768-c6d0-4c1d-842e-2027eb3542d1"),
                    Name = "Louisiana",
                    Description = "Louisiana Museum of Modern Art er et museum beliggende i en stor gammel park ved Nordsjællands kyst i Humlebæk.",
                    City = "Humlebæk",
                    Address = "Gl. Strandvej 13",
                    ExpCategoryId = Guid.Parse("b6f3f639-1450-4334-a123-2b1fb8c68808"),
                    ProfileId = Guid.Parse("62357886-d888-44f2-a929-c015a4b31dad")
                },
                new Experience()
                {
                    Id = Guid.Parse("173bf385-9aba-408a-a7ea-8bfe892e91b3"),
                    Name = "Dyrehaven",
                    Description = "Dyrehaven er en stor naturpark, der ligger nord for København. Dyrehaven rummer skovområder, små søer og åbne sletter, hvor mere end 2000 vilde hjorte har deres daglige gang.",
                    City = "Klampenborg",
                    Address = "Dyrehaven",
                    ExpCategoryId = Guid.Parse("45671c48-b7a2-4663-b826-044aeefd59ff"),
                    ProfileId = Guid.Parse("62357886-d888-44f2-a929-c015a4b31dad")
                });

            modelBuilder.Entity<Review>().HasData(
                new Review()
                { 
                    Id = Guid.Parse("e7c89ea8-7d68-4d37-b592-fefd69277684"),
                    Description = "Er en god oplevelse!",
                    ProfileId = Guid.Parse("9600bf95-bf37-4e6d-aeed-53d84a96a205"),
                    ExperienceId = Guid.Parse("bd345b81-462b-4ba9-999f-48ff44fad5e8")
                },
                new Review()
                {
                    Id = Guid.Parse("fb68f42a-09a0-45db-9216-7570f68219a4"),
                    Description = "Elsker maden",
                    ProfileId = Guid.Parse("62357886-d888-44f2-a929-c015a4b31dad"),
                    ExperienceId = Guid.Parse("bd345b81-462b-4ba9-999f-48ff44fad5e8")
                });

            modelBuilder.Entity<Rating>().HasData(
                new Rating() 
                { 
                  Id = Guid.Parse("fe2f716e-65b3-484a-8a52-42a275f03e7d"),
                  RatingCount = 4,
                  ProfileId = Guid.Parse("9600bf95-bf37-4e6d-aeed-53d84a96a205"),
                  ExperienceId = Guid.Parse("bd345b81-462b-4ba9-999f-48ff44fad5e8")
                },
                new Rating()
                {
                    Id = Guid.Parse("959ec261-0361-405a-86ef-84a33092ae4a"),
                    RatingCount = 5,
                    ProfileId = Guid.Parse("62357886-d888-44f2-a929-c015a4b31dad"),
                    ExperienceId = Guid.Parse("bd345b81-462b-4ba9-999f-48ff44fad5e8")
                });
        }
    }
}
