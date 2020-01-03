using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DAL.Entities;
using Newtonsoft.Json;

namespace BLL.JWT
{
    public class Tokens
    {
        public static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, User user, string userRole, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {

            var response = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                username = user.UserName,
                email = user.Email,
                role = userRole,
                token = await jwtFactory.GenerateEncodedToken(user.UserName, identity),
                expires_in = (int)jwtOptions.ValidFor.TotalSeconds,
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
    }
}
