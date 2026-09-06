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

    [Fact]
    public async Task GetAccessToken_WhenCalledConcurrently_LogsInOnlyOnce()
    {
        var authApiClient = new Mock<IAuthApiClient>();
        var loginStarted = new TaskCompletionSource();
        var allowLoginToComplete = new TaskCompletionSource();

        authApiClient
            .Setup(client => client.Login(It.IsAny<CancellationToken>()))
            .Returns(async () =>
            {
                loginStarted.SetResult();
                await allowLoginToComplete.Task;

                return new AuthTokenDto
                {
                    AccessToken = "access-token",
                    RefreshToken = "refresh-token"
                };
            });

        var provider = new AccessTokenProvider(authApiClient.Object);

        var firstRequest = provider.GetAccessToken(CancellationToken.None);
        await loginStarted.Task;
        var concurrentRequests = Enumerable.Range(0, 4)
            .Select(_ => provider.GetAccessToken(CancellationToken.None))
            .ToArray();

        allowLoginToComplete.SetResult();

        var tokens = await Task.WhenAll(concurrentRequests.Append(firstRequest));

        Assert.All(tokens, token => Assert.Equal("access-token", token));
        authApiClient.Verify(
            client => client.Login(It.IsAny<CancellationToken>()),
            Times.Once);
    }
}