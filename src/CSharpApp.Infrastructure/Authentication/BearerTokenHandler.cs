using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CSharpApp.Core.Interfaces.Authentication;

namespace CSharpApp.Infrastructure.Authentication
{
    public sealed class BearerTokenHandler : DelegatingHandler
    {
        private readonly IAccessTokenProvider _accessTokenProvider;

        public BearerTokenHandler(IAccessTokenProvider accessTokenProvider)
        {
            _accessTokenProvider = accessTokenProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var accessToken = await _accessTokenProvider.GetAccessToken(cancellationToken);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}