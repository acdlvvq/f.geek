using fgeek.Services.Interfaces;
using fgeek.Entities;
using fgeek.Exceptions;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace fgeek.Services
{
    public class AccountService : IAccountService
    {
        private readonly IDatabaseService? databaseService;

        public AccountService(IDatabaseService _databaseService)
        {
            this.databaseService = _databaseService;
            databaseService.Open("Accounts.db");
        }

        public async Task<bool> IsAlreadyTakenFieldAsync(Func<User, bool> compare)
        {
            return (await databaseService!.TableAsync<User>()).
                    AsParallel().
                    Where(compare).
                    Any();
        }

        public async Task CreateAccountAsync(User user)
        {
            user.Id = BitConverter.ToString(SHA256.HashData(
                      Encoding.UTF8.GetBytes(user.Username))).
                      Replace("-", "");

            var isAlreadyTakenEmail = IsAlreadyTakenFieldAsync
            (
                User.CompareEmail(user.Email)
            );
            var isAlreadyTakenUsername = IsAlreadyTakenFieldAsync
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

        public async Task<User> UserByIdAsync(string id)
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

        // mb search by hashing id's
        public async Task<bool> IsLikedMovieAsync(string userId, string movieId)
        {
            var likes = ByteConverterService.ToIntCollection
            (
                (await UserByIdAsync(userId)).LikeIds
            );

            return (await databaseService!.TableAsync<Like>()).
                    Where(item => item.TargetType == "Movie").
                    Where(item => item.TargetId == movieId).
                    Where(item => likes.Contains(item.Id)).
                    Any();
        }

        public async Task Like(string userId, string itemId, string itemType)
        {
            var id = BitConverter.ToInt32
            (
                SHA256.HashData(Encoding.UTF8.GetBytes
                (
                    new StringBuilder().
                        Append(userId).
                        Append(itemId).
                        Append(itemType).
                        ToString()
                ))    
            );

            var user = await UserByIdAsync(userId);
            var like = new Like(id, DateTime.Now, itemType, itemId);

            var likes = ByteConverterService.ToIntCollection(user.LikeIds);
            likes = likes.Append(id);
            user.LikeIds = ByteConverterService.ToByteCollection(likes).ToArray();

            var update = databaseService!.UpdateAsync(user);
            var insert = databaseService!.InsertAsync(like);

            Task.WaitAll(update, insert);
        }

        public async Task Unlike(string userId, string itemId, string itemType)
        {
            var id = BitConverter.ToInt32
            (
                SHA256.HashData(Encoding.UTF8.GetBytes
                (
                    new StringBuilder().
                        Append(userId).
                        Append(itemId).
                        Append(itemType).
                        ToString()
                ))
            );

            var user = await UserByIdAsync(userId);
            var like = new Like(id, DateTime.Now, itemType, itemId);

            var likes = ByteConverterService.ToIntCollection(user.LikeIds);
            likes = likes.Except([id]);
            user.LikeIds = ByteConverterService.ToByteCollection(likes).ToArray();

            var update = databaseService!.UpdateAsync(user);
            var delete = databaseService!.DeleteAsync(like);
           
            Task.WaitAll(update, delete);
        }
    }
}
