using System.Threading.Tasks;

namespace Blazor.Essentials.WebStorage
{
    public interface IStorage
    {
        ValueTask ClearAsync();
        
        ValueTask<string> GetItemAsync(string key);

        ValueTask<int> GetLengthAsync();

        ValueTask<string> KeyAsync(int n);

        ValueTask RemoveItemAsync(string key);

        ValueTask SetItemAsync(string key, string value);
    }
}