using MediatR;
using Microsoft.AspNetCore.Identity;
using VkNet.Enums.Filters;
using WAI.API.DataAccess.Entities;
using WAI.API.Features.Auth.VkModels;
using WAI.API.Services;

namespace WAI.API.Features.Auth;

public class VkLoginHandler : IRequestHandler<VkLoginRequest>
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IVkClientFactory _vkClientFactory;

    public VkLoginHandler(
        IConfiguration configuration,
        HttpClient httpClient,
        SignInManager<User> signInManager,
        UserManager<User> userManager,
        IVkClientFactory vkClientFactory)
    {
        _configuration = configuration;
        _httpClient = httpClient;
        _signInManager = signInManager;
        _userManager = userManager;
        _vkClientFactory = vkClientFactory;
    }


    public async Task<Unit> Handle(VkLoginRequest request, CancellationToken cancellationToken)
    {
        var vkAccessToken = await GetVkAccessTokenResponseAsync(request.Code);

        var user = await _userManager.FindByIdAsync(vkAccessToken.UserId.ToString());
        if (user != null)
        {
            await _signInManager.SignInAsync(user, true);
            return Unit.Value;
        }

        var vkApiClient = _vkClientFactory.GetVkClient(vkAccessToken.AccessToken);

        var profileInfos = await vkApiClient.Users.GetAsync(new[] {vkAccessToken.UserId}, ProfileFields.Photo100);

        var profileInfo = profileInfos.First();

        user = new User
        {
            Id = vkAccessToken.UserId,
            FirstName = profileInfo.FirstName,
            LastName = profileInfo.LastName,
            Email = vkAccessToken.Email,
            UserName = vkAccessToken.Email,
            AvatarUrl = profileInfo.Photo100.ToString(),
            EmailConfirmed = true,
            VkAccessToken = vkAccessToken.AccessToken
        };

        await _userManager.CreateAsync(user);
        await _signInManager.SignInAsync(user, true);

        return Unit.Value;
    }

    private async Task<VkAccessTokenResponse> GetVkAccessTokenResponseAsync(string code)
    {
        var vkClientId = _configuration.GetValue<string>("VkClientId");
        var vkRedirectUrl = _configuration.GetValue<string>("VkRedirectUrl");
        var vkAppSecret = _configuration.GetValue<string>("VkAppSecret");

        var vkResponse = await _httpClient.GetAsync(
            "https://oauth.vk.com/access_token?" +
            $"client_id={vkClientId}&" +
            $"client_secret={vkAppSecret}&" +
            $"redirect_uri={vkRedirectUrl}" +
            $"&code={code}");

        return await vkResponse.Content.ReadFromJsonAsync<VkAccessTokenResponse>() ??
               throw new InvalidOperationException();
    }
}