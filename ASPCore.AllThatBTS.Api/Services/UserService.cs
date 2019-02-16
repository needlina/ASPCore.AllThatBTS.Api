using ASPCore.AllThatBTS.Api.Common;
using ASPCore.AllThatBTS.Api.Entities;
using ASPCore.AllThatBTS.Api.Repository;
using System.Collections.Generic;

namespace ASPCore.AllThatBTS.Api.Service
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
        public UserT GetUserByEmail(string userEmail)
        {
            return userRepository.SelectUserByEmail(userEmail);
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

        public bool VerifyUser(string email, string password)
        {
            UserT user = userRepository.SelectUserByEmail(email);
            if(user == null)
            {
                throw new NotFoundException("사용자가 존재하지 않습니다.", "사용자 없음");
            }

            return CryptoHelper.Crypto.VerifyHashedPassword(user.Password, password);
        }
    }
}
