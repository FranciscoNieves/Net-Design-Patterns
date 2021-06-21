using System.Threading.Tasks;

namespace LogLibrary
{
    public interface IContentWriter
    {
        Task<bool> Write(string content);
    }
}
