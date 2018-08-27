using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace SGSApp
{
    public interface IAuthenticator
    {
        Task<AuthenticationResult> Authenticate(string tenantUrl, string graphResourceUri, string applicationId,
            string returnUri, string evento);
    }
}