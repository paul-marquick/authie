namespace ApiClientCore;

public abstract class HttpServiceBase(HttpClient httpClient) : IHttpService
{
    public async Task<string> GetAsync(string path)
    {
        return await httpClient.GetStringAsync(path);
    }
}