using Microsoft.AspNetCore.Identity;

namespace EVoucherSystem.Services
{
    public interface ITokenSevice
    {
        public string CreateToken(string username);
    }
}
