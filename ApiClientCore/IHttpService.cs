namespace ApiClientCore;

public interface IHttpService
{
    Task<string> GetAsync(string path);
}
