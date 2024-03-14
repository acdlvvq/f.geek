using fgeek.Entities;

namespace fgeek.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<bool> IsAlreadyTakenField(Func<User, bool> compare);
        public Task       CreateAccountAsync(User user);
        public Task<User> LogInAsync(AuthorizationRequest authorizationRequest);
        public Task<User> UserById(string id);
    }
}
