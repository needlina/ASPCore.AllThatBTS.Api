using ASPCore.AllThatBTS.Api.Common;
using ASPCore.AllThatBTS.Api.Entities;
using ASPCore.AllThatBTS.Api.Model;
using ASPCore.AllThatBTS.Api.Repository;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ASPCore.AllThatBTS.Api.Service
{
    public class AuthService
    {
        private readonly AuthRepository authRepository;

        public AuthService()
        {
            authRepository = new AuthRepository();
        }
        public AuthT CreateToken(ReadUserM user)
        {

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppConfiguration.JwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Sid, user.UserNo)
                }),
                Expires = DateTime.Now.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                IssuedAt = DateTime.Now,
                Audience = "aud.allthatbts.com",
                Issuer = "api.allthatbts.com",
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);


            AuthT apiToken = new AuthT()
            {
                UserNo = user.UserNo,
                AccessToken = tokenHandler.WriteToken(token),
                AccessTokenExpireDate = tokenDescriptor.Expires.Value,
                RefreshToken = CreateRefreshToken(),
                RefreshTokenExpireDate = DateTime.Now.AddDays(7),
                Scope = null
            };

            // 토큰 DB에 저장
            authRepository.UpsertToken(apiToken);
            apiToken = authRepository.SelectToken(user.UserNo);

            return apiToken;
        }

        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        public AuthT GetToken(string userNo)
        {
            return authRepository.SelectToken(userNo);
        }

        public bool VerifyAccessToken(string accessToken, string refreshToken)
        {
            bool validAccessToken = false;
            bool validRefreshToken = true;

            SecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(AppConfiguration.GetJwtSecret()));
            SecurityToken validatedToken = null;
            var validationParameters = new TokenValidationParameters()
            {
                ValidAudience = "aud.allthatbts.com",
                ValidIssuer = "api.allthatbts.com",
                IssuerSigningKey = key
            };

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(accessToken, validationParameters, out validatedToken);

            validAccessToken = validatedToken != null ? true : false;

            AuthT token = authRepository.SelectTokenByRefreshToken(refreshToken);

            validRefreshToken = token.AccessToken == accessToken ? true : false;

            if (validAccessToken == true && validRefreshToken == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
