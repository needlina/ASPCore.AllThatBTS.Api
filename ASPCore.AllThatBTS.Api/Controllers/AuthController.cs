using ASPCore.AllThatBTS.Api.Common;
using ASPCore.AllThatBTS.Api.Entities;
using ASPCore.AllThatBTS.Api.Enum;
using ASPCore.AllThatBTS.Api.Model;
using ASPCore.AllThatBTS.Api.Service;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace ASPCore.AllThatBTS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        protected readonly AuthService authService;
        protected readonly UserService userService;
        protected readonly IMapper mapper;
        protected readonly Logger logger;

        public AuthController(IMapper _mapper)
        {
            authService = new AuthService();
            userService = new UserService();
            mapper = _mapper;

            logger = NLog.Web.NLogBuilder.ConfigureNLog(AppConfiguration.NLogPath).GetCurrentClassLogger();
        }

        /// <summary>
        /// 인증 (토큰생성)
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public ReadUserM Login(string email, string password)
        {
            ReadUserM user = new ReadUserM();

            //비밀번호 검증
            bool verified = userService.VerifyUser(email, password);

            // 비밀번호 검증 완료 시
            if (verified)
            {
                UserT userEntity = userService.GetUserByEmail(email);
                user = mapper.Map<UserT, ReadUserM>(userEntity);

                // 토큰생성
                TokenT authEntity = authService.CreateToken(user);
                TokenM token = mapper.Map<TokenT, TokenM>(authEntity);
                user.Token = token;

            }
            else
            {
                throw new IncorrectDataException("비밀번호가 올바르지 않습니다.", "비밀번호 오류", LayerID.AuthController);
            }

            logger.Log(LogLevel.Info, string.Format("호출 성공 : {0}", MethodBase.GetCurrentMethod().Name));
            return user;

        }

        /// <summary>
        /// 토큰 조회 (조회용)
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("ReadToken")]
        public TokenM ReadToken(string email, string password)
        {
            TokenM token = new TokenM();
            //비밀번호 검증
            bool verified = userService.VerifyUser(email, password);

            // 비밀번호 검증 완료 시
            if (verified)
            {
                UserT userEntity = userService.GetUserByEmail(email);
                TokenT authEntity = authService.GetToken(userEntity.UserNo);
                token = mapper.Map<TokenT, TokenM>(authEntity);
            }
            else
            {
                throw new IncorrectDataException("비밀번호가 올바르지 않습니다.", "비밀번호 오류", LayerID.AuthController);
            }

            logger.Log(LogLevel.Info, string.Format("호출 성공 : {0}", MethodBase.GetCurrentMethod().Name));
            return token;
        }

        /// <summary>
        /// 액세스 토큰 재생성 (토큰 만료시)
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        [HttpPost("RefreshToken")]
        public TokenM RefreshToken(string accessToken, string refreshToken)
        {
            TokenM tokenModel = new TokenM();
            TokenT token = authService.ReadAccessToken(accessToken, refreshToken);

            DateTime validTo = new JwtSecurityTokenHandler().ReadJwtToken(accessToken).ValidTo;
            DateTime tokenExpiredTime = CommonHelper.GetDateTime(validTo);

            if (tokenExpiredTime < DateTime.Now)
            {
                UserT userEntity = userService.GetUser(token.UserNo);
                ReadUserM user = mapper.Map<UserT, ReadUserM>(userEntity);

                TokenT authEntity = authService.CreateToken(user);
                tokenModel = mapper.Map<TokenT, TokenM>(authEntity);
            }
            else
            {
                throw new BadRequestException("토큰 만료 시간이 유효합니다.", "토큰 오류", LayerID.AuthController);
            }

            logger.Log(LogLevel.Info, string.Format("호출 성공 : {0}", MethodBase.GetCurrentMethod().Name));
            return tokenModel;
        }
    }
}