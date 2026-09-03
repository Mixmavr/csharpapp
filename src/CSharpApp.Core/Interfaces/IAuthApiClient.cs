using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpApp.Core.Dtos.AuthDto;

namespace CSharpApp.Core.Interfaces
{
    public interface IAuthApiClient
    {
        Task<AuthTokenDto> Login(CancellationToken cancellationToken);
    }
}