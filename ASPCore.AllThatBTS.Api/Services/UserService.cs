using ASPCore.AllThatBTS.Api.BizDac;
using ASPCore.AllThatBTS.Api.Entities;
using ASPCore.AllThatBTS.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCore.AllThatBTS.Api.Services
{
    public class UserService
    {
        private readonly UserRepository userRepository;

        public UserService()
        {
            userRepository = new UserRepository();
        }

        public int CreateUser(UserT user)
        {
            user.Password = CryptoHelper.Crypto.HashPassword(user.Password);
            return userRepository.InsertUser(user);
        }

        public UserT GetUser(string userNo)
        {
            return userRepository.SelectUser(userNo);
        }

        public List<UserT> GetAllUser()
        {
            return userRepository.SelectAllUsers();
        }

        public int SetUser(UserT user)
        {
            return userRepository.UpdateUser(user);
        }

        public int RemoveUser(string userNo)
        {
            return userRepository.DeleteUser(userNo);
        }
    }
}
