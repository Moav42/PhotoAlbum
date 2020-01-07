using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DAL.Entities;
using Newtonsoft.Json;

namespace BLL.JWT
{
    public class Tokens
    {
        /// <summary>
        /// Generate JWT with contains information about user, his Encoded Token and the time of its validity.
        /// </summary>
        /// <param name="identity">Claims of the given user</param>
        /// <param name="jwtFactory">Fatory for generation Encoded Token</param>
        /// <param name="user">User name</param>
        /// <param name="userRole">User role</param>
        /// <param name="jwtOptions">Setting for JWT options </param>
        /// <param name="serializerSettings"> Settings for serialization of JWT</param>
        /// <returns></returns>
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
