using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CSharpApp.Core.Dtos.AuthDto;
using Microsoft.Extensions.Options;

namespace CSharpApp.Infrastructure.Clients
{
    public sealed class AuthApiClient : IAuthApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly RestApiSettings _restApiSettings;

        public AuthApiClient(HttpClient httpClient, IOptions<RestApiSettings> restApiSettings)
        {
            _httpClient = httpClient;
            _restApiSettings = restApiSettings.Value;
        }

        //Εδώ χτυπάμε το εξωτερικό login με τα credentials που έχουμε
        public async Task<AuthTokenDto> Login(CancellationToken cancellationToken)
        {
            var loginRequest = new
            {
                email = _restApiSettings.Username,
                password = _restApiSettings.Password
            };

            var response = await _httpClient.PostAsJsonAsync(
                "auth/login", loginRequest, cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<AuthTokenDto>(
                cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("he authentication API returned an empty response.");
        }
    }
}