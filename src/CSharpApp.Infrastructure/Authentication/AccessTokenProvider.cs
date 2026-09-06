using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpApp.Core.Interfaces.Authentication;

namespace CSharpApp.Infrastructure.Authentication
{
    public sealed class AccessTokenProvider : IAccessTokenProvider
    {
        private readonly IAuthApiClient _authApiClient;
        private readonly SemaphoreSlim _tokenLock = new(1, 1);

        private string? _accessToken;

        public AccessTokenProvider(IAuthApiClient authApiClient)
        {
            _authApiClient = authApiClient;
        }

        public async Task<string> GetAccessToken(CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(_accessToken))
            {
                return _accessToken;
            }

            await _tokenLock.WaitAsync(cancellationToken);

            try
            {
                if (!string.IsNullOrWhiteSpace(_accessToken))
                {
                    return _accessToken;
                }

                var authToken = await _authApiClient.Login(cancellationToken);

                _accessToken = authToken.AccessToken;

                return _accessToken;
            }
            finally
            {
                _tokenLock.Release();
            }
        }
    }
}