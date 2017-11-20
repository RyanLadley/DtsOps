using dtso.auth.Enum;
using dtso.auth.Models.Buisness;
using dtso.auth.Models.DataTransfer.Form;
using dtso.data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.auth.Logic.Interfaces
{
    public interface IUserRegistrar
    {
        data.Entities.User GetUser(int userId);
        int EditUser(UserRegistrationForm form);
        AuthStatus RegisterUser(UserRegistrationForm registration);
        data.Entities.User ValidateLogin(LoginCredentialsForm credentials);
        List<data.Entities.User> GetUserListing();
    }
}
