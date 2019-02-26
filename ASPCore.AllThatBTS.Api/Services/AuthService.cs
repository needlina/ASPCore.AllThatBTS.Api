using ASPCore.AllThatBTS.Api.Common;
using ASPCore.AllThatBTS.Api.Entities;
using ASPCore.AllThatBTS.Api.Model;
using ASPCore.AllThatBTS.Api.Repository;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TimeZoneConverter;

namespace ASPCore.AllThatBTS.Api.Service
{
    public class AuthService
    {
        private readonly AuthRepository authRepository;

        public AuthService()
        {
            authRepository = new AuthRepository();
        }
        public TokenT CreateToken(ReadUserM user)
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


            TokenT apiToken = new TokenT()
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

        public TokenT GetToken(string userNo)
        {
            return authRepository.SelectToken(userNo);
        }

        /// <summary>
        /// Access Token 유효성 체크
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public TokenT ReadAccessToken(string accessToken, string refreshToken)
        {
            TokenT token = authRepository.SelectTokenByRefreshToken(refreshToken);

            if (token != null)
            {
                // Access Token 일치 여부
                if(token.AccessToken != accessToken)
                {
                    throw new TokenErrorException("정상적인 토큰이 아닙니다.", "Invalid Token Error");
                }

                // Refresh Token 만료시간 확인
                if(token.RefreshTokenExpireDate < CommonHelper.GetDateTimeNow)
                {
                    throw new TokenErrorException("Refresh 토큰이 만료되었습니다. 다시 로그인 해주세요.", "Invalid Token Error");
                }

                return token;
            }
            else
            {
                throw new TokenErrorException("토큰이 존재하지 않습니다.", "Invalid Token Error");
            }
            
        }
    }
}
