using ASPCore.AllThatBTS.Api.Common;
using ASPCore.AllThatBTS.Api.Entities;
using ASPCore.AllThatBTS.Api.Model;
using ASPCore.AllThatBTS.Api.Service;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

namespace ASPCore.AllThatBTS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        protected readonly UserService userService;
        protected readonly IMapper mapper;
        protected readonly Logger logger;

        public UserController(IMapper _mapper)
        {
            userService = new UserService();
            mapper = _mapper;

            logger = NLog.Web.NLogBuilder.ConfigureNLog(AppConfiguration.NLogPath).GetCurrentClassLogger();
        }

        [AllowAnonymous]
        [HttpPost("MakeUser")]
        public Response MakeUser(MakeUserM user)
        {

            int result = 0;
            Response response = new Response();

            UserT userEntity = mapper.Map<MakeUserM, UserT> (user);

            result = userService.CreateUser(userEntity);

            if(result > 0)
            {
                response.Status = ((int)HttpStatusCode.OK).ToString();
                response.Message = "사용자 생성에 성공하였습니다.";
                logger.Log(LogLevel.Info, response.Message);
            }
            else
            {
                response.Status = ((int)HttpStatusCode.BadRequest).ToString();
                response.ErrMsg = "사용자 생성에 실패하였습니다.";
                logger.Log(LogLevel.Warn, response.ErrMsg);
            }

            logger.Log(LogLevel.Info, string.Format("호출 성공 : {0}", MethodBase.GetCurrentMethod().Name));
            return response;

        }

        [Authorize]
        [HttpGet("ReadUser")]
        public Response<ReadUserM> ReadUser(string userNo)
        {
            Response<ReadUserM> response = new Response<ReadUserM>();

            UserT userEntity = userService.GetUser(userNo);
            ReadUserM user = mapper.Map<UserT, ReadUserM>(userEntity);

            if(user != null)
            {
                response.Result = user;
                response.Status = ((int)HttpStatusCode.OK).ToString();
            }
            else
            {
                response.Status = ((int)HttpStatusCode.NotFound).ToString();
                response.ErrMsg = "사용자가 존재하지 않습니다.";
                logger.Log(LogLevel.Warn, response.ErrMsg);
            }

            logger.Log(LogLevel.Info, string.Format("호출 성공 : {0}", MethodBase.GetCurrentMethod().Name));
            return response;
        }

        [Authorize]
        [HttpGet("ReadAllUser")]
        public ListResponse<ReadUserM> ReadAllUser()
        {
            ListResponse<ReadUserM> response = new ListResponse<ReadUserM>();

            List<UserT> userEntities = userService.GetAllUser();
            List<ReadUserM> userList = mapper.Map<List<UserT>, List<ReadUserM>>(userEntities);

            if (userList != null || userList.Count > 0)
            {
                response.ListResult = userList;
                response.Status = ((int)HttpStatusCode.OK).ToString();
            }
            else
            {
                response.Status = ((int)HttpStatusCode.NotFound).ToString();
                response.ErrMsg = "사용자가 존재하지 않습니다.";
                logger.Log(LogLevel.Warn, response.ErrMsg);
            }

            logger.Log(LogLevel.Info, string.Format("호출 성공 : {0}", MethodBase.GetCurrentMethod().Name));
            return response;

        }

        [Authorize]
        [HttpPost("ModifyUser")]
        public Response ModifyUser(ModifyUserM user)
        {
            int result = 0;
            Response response = new Response();

            UserT userEntity = mapper.Map<ModifyUserM, UserT>(user);

            result = userService.SetUser(userEntity);

            if (result > 0)
            {
                response.Status = ((int)HttpStatusCode.OK).ToString();
                response.Message = "사용자를 수정하였습니다.";
                logger.Log(LogLevel.Info, response.Message);
            }
            else
            {
                response.Status = ((int)HttpStatusCode.OK).ToString();
                response.ErrMsg = "사용자 수정에 실패하였습니다.";
                logger.Log(LogLevel.Warn, response.ErrMsg);
            }

            logger.Log(LogLevel.Info, string.Format("호출 성공 : {0}", MethodBase.GetCurrentMethod().Name));
            return response;
        }

        [Authorize]
        [HttpGet("EraseUser")]
        public Response EraseUser(string userNo)
        {
            int result = 0;
            Response response = new Response();

            result = userService.RemoveUser(userNo);

            if (result > 0)
            {
                response.Status = ((int)HttpStatusCode.OK).ToString();
                response.Message = "사용자를 삭제하였습니다.";
                logger.Log(LogLevel.Info, response.Message);
            }
            else
            {
                response.Status = ((int)HttpStatusCode.OK).ToString();
                response.ErrMsg = "사용자 삭제에 실패하였습니다.";
                logger.Log(LogLevel.Warn, response.ErrMsg);
            }

            logger.Log(LogLevel.Info, string.Format("호출 성공 : {0}", MethodBase.GetCurrentMethod().Name));
            return response;
        }

    }
}