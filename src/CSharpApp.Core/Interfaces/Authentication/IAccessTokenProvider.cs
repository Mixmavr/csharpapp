using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpApp.Core.Interfaces.Authentication
{
    public interface IAccessTokenProvider
    {
        Task<string> GetAccessToken(CancellationToken cancellationToken);
    }
}