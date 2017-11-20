using dtso.auth.Enum;
using dtso.auth.Logic.Interfaces;
using dtso.auth.Models.Buisness;
using dtso.auth.Models.DataTransfer.Form;
using dtso.data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.auth.Logic
{
    public class UserRegistrar : IUserRegistrar
    {
        private IPasswordManager _passwordManager;
        private IUserRepository _userRepository;

        public UserRegistrar(IUserRepository userRepo, IPasswordManager passMan)
        {
            _passwordManager = passMan;
            _userRepository = userRepo;
        }

        public data.Entities.User GetUser(int userId)
        {
            return  _userRepository.Get(userId);
        }


        public AuthStatus RegisterUser(UserRegistrationForm registration)
        {
            if (!_passwordManager.IsValidPassphrase(registration.Passphrase))
                return AuthStatus.InvalidPassword;

            var newUser = User.MapFromObject(registration, _passwordManager);

            var user = newUser.MapToEntity();
            user = _userRepository.Add(user);
            
            return AuthStatus.Good;
        }

        public data.Entities.User ValidateLogin(LoginCredentialsForm credentials)
        {
            var storedUser = _userRepository.GetFromEmail(credentials.Email);

            if (storedUser != null && _passwordManager.VerifyPassword(storedUser, credentials))
               return storedUser;
            
            return null;
        }

        public int EditUser(UserRegistrationForm form)
        {

            var user = GetUser(form.UserId);

            user.FirstName = form.FirstName;
            user.LastName = form.LastName;
            user.Email = form.Email;
        
            if (!string.IsNullOrEmpty(form.Passphrase))
            {

                if (!_passwordManager.IsValidPassphrase(form.Passphrase))
                    return 0;

                else
                {
                    var passwordIngredients = _passwordManager.HashPassphrase(new PasswordIngredients { Passphrase = form.Passphrase });
                    user.Password = passwordIngredients.Password;
                    user.Salt = passwordIngredients.Salt;
                }
            }

            //If permissions is -1 , we will just leave it as is
            user.Permissions = (form.Permissions == -1) ? user.Permissions : form.Permissions;

            user = _userRepository.Update(user);

            return user.UserId;
        }

        public List<data.Entities.User> GetUserListing()
        {
            return _userRepository.GetAll();
        }
    }
}
