using Haraka.WebApp.Shared.Information;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public async Task Login(string user)
        {
            var token = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");

            if (string.IsNullOrWhiteSpace(token))
            {
                using HttpResponseMessage response = await httpClient
                    .PostAsJsonAsync(controllerName + "login", new LoginInfo { Username = user });

                token = await response.Content.ReadAsStringAsync();
                await jsRuntime.InvokeVoidAsync("localStorage.setItem", "token", token);
            }

            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            using HttpResponseMessage testResponse = await httpClient.GetAsync(controllerName + "test");

            IsAuthenticated = true;

        }
    }
}
