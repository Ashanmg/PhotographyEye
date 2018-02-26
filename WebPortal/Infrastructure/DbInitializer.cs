using PhotographyEye.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhotographyEye.Infrastructure
{
    public static class DbInitializer
    {
        private static PhotographyEyeContext context;
        public static void Initialize(IServiceProvider serviceProvider, string imagePath)
        {
            context = (PhotographyEyeContext)serviceProvider.GetService(typeof(PhotographyEyeContext));

            InitializePhotoAlbums(imagePath);
            InitializeUserRoles();
        }

        private static void InitializePhotoAlbums(string imagepath)
        {
            if (!context.Albums.Any())
            {
                List<Album> _albums = new List<Album>();

                var _album1 = context.Albums.Add(
                    new Album
                    {
                        DateCreated = DateTime.Now,
                        Title = "Album 1",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
                    }).Entity;
                _albums.Add(_album1);

                var _album2 = context.Albums.Add(
                    new Album
                    {
                        DateCreated = DateTime.Now,
                        Title = "Album 2",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
                    }).Entity;
                _albums.Add(_album2);

                var _album3 = context.Albums.Add(
                    new Album
                    {
                        DateCreated = DateTime.Now,
                        Title = "Album 3",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
                    }).Entity;
                _albums.Add(_album3);

                var _album4 = context.Albums.Add(
                    new Album
                    {
                        DateCreated = DateTime.Now,
                        Title = "Album 4",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
                    }).Entity;
                _albums.Add(_album4);

                string[] _images = Directory.GetFiles(Path.Combine(imagepath, "images"));
                Random rnd = new Random();

                foreach (string image in _images)
                {
                    int _selectedAlbum = rnd.Next(1, 4);
                    string _fileName = Path.GetFileName(image);

                    context.Photos.Add(
                        new Photo()
                        {
                            Title = _fileName,
                            DateUploaded = DateTime.Now,
                            Uri = _fileName,
                            Album = _albums.ElementAt(_selectedAlbum)
                        });
                }

                context.SaveChanges();
            }
        }

        private static void InitializeUserRoles()
        {
            if (!context.Roles.Any())
            {
                //Create new role
                context.Roles.AddRange(new Role[]
                    {
                        new Role()
                        {
                            Name = "Admin"
                        }
                    });

                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Users.Add(new User()
                {
                    Email = "ashanmg9@gmail.com",
                    Username = "ashanm",
                    HashedPassword = "9wsmLgYM5Gu4zA/BSpxK2GIBEWzqMPKs8wl2WDBzH/4=",
                    Salt = "GTtKxJA6xJuj3ifJtTXn9Q==",
                    IsLocked = false,
                    DateCreated = DateTime.Now
                });

                // create user-admin for ashanm
                context.UserRoles.AddRange(new UserRole[] {
                new UserRole() {
                    RoleId = 1, // admin
                    UserId = 1  // ashanm
                } });

                context.SaveChanges();
            }
        }
    }
}
