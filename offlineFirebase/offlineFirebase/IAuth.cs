
using System.Threading.Tasks;

namespace offlineFirebase
{
    public interface IAuth
    {
        Task<string> LoginWithEmailPassword(string email, string password);
        string SignUpWithEmailPassword(string email, string password);
    }
}
