using FluentMigrator;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeMigrations.Seeds
{
    public record User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
    }

    [Migration(1006)]
    public class _1006_UserTableSeed : Migration
    {
        public static PasswordHasher<User> hasher = new();
        public static List<User> userDetails = new()
        {
            new User
            {
                Username="MariamMostafa.493",
                Password=hasher.HashPassword(new User(), "@MariamMostafa.493"),
                RefreshToken = null
            },
            new User
            {
                Username="MariamDawood444",
                Password=hasher.HashPassword(new User(), "Mariam_Dawood!444"),
                RefreshToken = null
            },
            new User
            {
                Username="Mariam123.Mostafa",
                Password=hasher.HashPassword(new User(), "passwordMariam"),
                RefreshToken = null
            }
        };
        public override void Up()
        {
            foreach (var user in userDetails)
            {
                Insert.IntoTable("user")
                    .Row(new
                    {
                        username=user.Username,
                        password=user.Password,
                        refresh_token = user.RefreshToken
                    });
            }
        }
        public override void Down()
        {
            //empty, not using
        }
    }

}
