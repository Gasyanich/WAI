using VkNet;
using VkNet.Model;

namespace WAI.API.Services;

public class VkClientFactory : IVkClientFactory
{
    public VkApi GetVkClient(string accessToken)
    {
        var vkApi = new VkApi();

        vkApi.Authorize(new ApiAuthParams
        {
            AccessToken = accessToken
        });

        return vkApi;
    }
}

public interface IVkClientFactory
{
    VkApi GetVkClient(string accessToken);
}