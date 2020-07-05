using Haraka.WebApp.Shared.Information;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Haraka.WebApp.Client.Services
{
    public class AuthenticationService
    {
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }

        private readonly HttpClient httpClient;
        private readonly IJSRuntime jsRuntime;
        private const string controllerName = "api/authentication/";

        public AuthenticationService(HttpClient httpClient, IJSRuntime runtime)
        {
            this.httpClient = httpClient;
            jsRuntime = runtime;
        }

        public async Task<bool> ValidateToken()
        {
            Debug.WriteLine("Get token");
            var token = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            Debug.WriteLine("validate token");
            using HttpResponseMessage testResponse = await httpClient.GetAsync(controllerName + "test");

            if (testResponse.IsSuccessStatusCode)
            {
                Debug.WriteLine($"validate successfull");
                return IsAuthenticated = true;
            }

            Debug.WriteLine($"Exception on validate");
            await jsRuntime.InvokeAsync<string>("localStorage.removeItem", "token");
            IsAuthenticated = false;
            return false;
        }

        public async Task Login(string user)
        {
            if (IsAuthenticated || await ValidateToken())
                return;

            Debug.WriteLine("User is not authenticated");

            using HttpResponseMessage response = await httpClient
                .PostAsJsonAsync(controllerName + "login", new LoginInfo { Username = user });

            Debug.WriteLine("Get is not authenticated");
            var token = await response.Content.ReadAsStringAsync();
            Debug.WriteLine("Set token and username");
            await jsRuntime.InvokeVoidAsync("localStorage.setItem", "token", token);
            await jsRuntime.InvokeVoidAsync("localStorage.setItem", "username", user);

            await ValidateToken();
        }
    }
}
