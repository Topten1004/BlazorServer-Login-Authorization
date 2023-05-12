using BlazorBaseApp.Pages;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.RenderTree;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.ComponentModel;
using System.Net;
using System.Security.Claims;
using BlazorBaseApp.Model;

namespace BlazorBaseApp.Services
{
    public class AuthenticationService
    {
        public bool IsLoggedIn { get; private set; }
        public PersonModel LoggedInUser { get; private set; }

        public bool Login(PersonModel user)
        {
            LoggedInUser = user;
            IsLoggedIn = true;
            return true;
        }

        public async Task Logout()
        {
            IsLoggedIn = false;
            
        }
    }
}
