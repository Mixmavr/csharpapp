using CSharpApp.Core.Dtos.AuthDto;
using CSharpApp.Core.Interfaces;
using CSharpApp.Infrastructure.Authentication;
using Moq;

namespace CSharpApp.Application.Tests.Authentication;

public sealed class AccessTokenProviderTests
{
    [Fact]
    public async Task GetAccessToken_WhenCalledTwice_LogsInOnlyOnce()
    {
        var authApiClient = new Mock<IAuthApiClient>();

        authApiClient
            .Setup(client => client.Login(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new AuthTokenDto
            {
                AccessToken = "access-token",
                RefreshToken = "refresh-token"
            });

        var provider = new AccessTokenProvider(
            authApiClient.Object);

        var firstToken = await provider.GetAccessToken(
            CancellationToken.None);

        var secondToken = await provider.GetAccessToken(
            CancellationToken.None);

        Assert.Equal("access-token", firstToken);
        Assert.Equal(firstToken, secondToken);

        authApiClient.Verify(
            client => client.Login(
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}