using fgeek.Services.Interfaces;
using fgeek.Entities;
using fgeek.Exceptions;
using System.Text;

namespace fgeek.Services
{
    public class AccountService : IAccountService
    {
        private readonly IDatabaseService? databaseService;

        public AccountService(IDatabaseService _databaseService)
        {
            this.databaseService = _databaseService;
            databaseService.OpenAsync("Accounts.db").Wait();
        }

        public async Task<bool> IsAlreadyTakenField(Func<User, bool> compare)
        {
            return (await databaseService!.TableAsync<User>()).
                    AsParallel().
                    Where(compare).
                    Any();
        }

        public async Task CreateAccountAsync(User user)
        {
            user.Id = Guid.NewGuid().ToString();

            var isAlreadyTakenEmail = IsAlreadyTakenField
            (
                User.CompareEmail(user.Email)
            );
            var isAlreadyTakenUsername = IsAlreadyTakenField
            (
                User.CompareUsername(user.Username)
            );

            Task.WaitAll(isAlreadyTakenEmail, isAlreadyTakenUsername);
            if (isAlreadyTakenEmail.Result || isAlreadyTakenUsername.Result)
            {
                throw new FieldAlreadyTakenException
                      (
                          new StringBuilder().
                          Append("This ").
                          Append($"{(isAlreadyTakenEmail.Result ? "email" : "username")} ").
                          Append("is already taken").
                          ToString()
                      );
            }

            await databaseService!.InsertAsync(user);
        }

        public async Task<User> LogInAsync(AuthorizationRequest authorizationRequest)
        {
            var accounts = (await databaseService!.TableAsync<User>()).
                           Where
                           (
                               AuthorizationRequest.IsCorrectRequest(authorizationRequest)
                           );

            if (!accounts.Any())
            {
                throw new AccountNotFoundException("Failed To Log In");
            }

            return accounts.First();
        }

        public async Task<User> UserById(string id)
        {
            var accounts = (await databaseService!.TableAsync<User>()).
                           Where
                           (
                                User.CompareId(id)
                           );

            if (!accounts.Any())
            {
                throw new UserNotFoundException
                (
                    new StringBuilder().
                    Append("User With Id = ").
                    Append(id).
                    Append(" Was Not Found").
                    ToString()
                );
            }

            return accounts.First();
        }
    }
}
