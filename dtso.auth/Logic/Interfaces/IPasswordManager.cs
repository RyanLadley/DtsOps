using dtso.auth.Models.Buisness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dtso.data.Entities;

namespace dtso.auth.Logic.Interfaces
{
    public interface IPasswordManager
    {
        PasswordIngredients HashPassphrase(PasswordIngredients ingredients);
        bool IsValidPassphrase(string passphrase);
        bool VerifyPassword(data.Entities.User storedUser, LoginCredentialsForm credentials);
    }
}
