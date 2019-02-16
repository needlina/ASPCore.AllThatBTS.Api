using ASPCore.AllThatBTS.Api.Common;
using ASPCore.AllThatBTS.Api.Entities;
using ASPCore.AllThatBTS.Api.Model;
using ASPCore.AllThatBTS.Api.Service;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

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

            string logConfigPath = Path.Combine(Directory.GetCurrentDirectory(), "nlog.config");
            logger = NLog.Web.NLogBuilder.ConfigureNLog(logConfigPath).GetCurrentClassLogger();
        }

        /// <summary>
        /// 인증 (토큰생성
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
                AuthT authEntity = authService.CreateToken(user);
                Token token = mapper.Map<AuthT, Token>(authEntity);
                user.Token = token;

            }
            else
            {
                throw new BadRequestException("비밀번호가 올바르지 않습니다.", "비밀번호 오류");
            }

            return user;

        }

        /// <summary>
        /// 토큰 조회 (조회용)
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("ReadToken")]
        public Token ReadToken(string email, string password)
        {
            Token token = new Token();
            //비밀번호 검증
            bool verified = userService.VerifyUser(email, password);

            // 비밀번호 검증 완료 시
            if (verified)
            {
                UserT userEntity = userService.GetUserByEmail(email);
                AuthT authEntity = authService.GetToken(userEntity.UserNo);
                token = mapper.Map<AuthT, Token>(authEntity);
                
            }

            return token;
        }

        /// <summary>
        /// 액세스 토큰 재생성 (토큰 만료시)
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("RefreshToken")]
        public Token RefreshTokenAsync(string refreshToken)
        {
            Token token = new Token();
            //토큰 검증
            string accessToken = Request.Headers["Authorization"].ToString().Replace("bearer ", "");

            bool verified = authService.VerifyAccessToken(accessToken, refreshToken);

            // 토큰 검증 완료 시
            if (verified)
            {
                //Get the current claims principal
                var identity = HttpContext.User.Identity as ClaimsIdentity;

                // Get the claims values
                var sid = identity.Claims.Where(c => c.Type == ClaimTypes.Sid)
                                   .Select(c => c.Value).SingleOrDefault();

                UserT userEntity = userService.GetUser(sid);
                ReadUserM user = mapper.Map<UserT, ReadUserM>(userEntity);

                AuthT authEntity = authService.CreateToken(user);
                token = mapper.Map<AuthT, Token>(authEntity);
            }
            else
            {
                throw new Exception("유효하지 않은 Token입니다.");
            }

            return token;
        }
    }
}