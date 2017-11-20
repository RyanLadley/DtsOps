using dtso.auth.Logic.Interfaces;
using dtso.auth.Models.DataTransfer.Form;
using dtso.data.Entities;

namespace dtso.auth.Models.Buisness
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public int Permissions { get; set; }

        private IPasswordManager _passwordManager;

        public User(IPasswordManager _passMan)
        {
            _passwordManager = _passMan;
        }

        public data.Entities.User MapToEntity()
        {
            var user = new data.Entities.User()
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                Password = this.Password,
                Salt = this.Salt,
                Permissions = this.Permissions
            };

            return user;
        }

        public static User MapFromObject(UserRegistrationForm form, IPasswordManager passwordManager)
        {
            var passwordIngredients = passwordManager.HashPassphrase(new PasswordIngredients { Passphrase = form.Passphrase});
            var newUser = new User(passwordManager)
            {
                FirstName = form.FirstName,
                LastName = form.LastName,
                Email = form.Email,
                Password = passwordIngredients.Password,
                Salt = passwordIngredients.Salt,
                Permissions = form.Permissions
            };

            return newUser;
        }
    }
}
