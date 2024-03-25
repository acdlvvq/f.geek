using fgeek.Entities;

namespace fgeek.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<bool> IsAlreadyTakenFieldAsync(Func<User, bool> compare);
        public Task       CreateAccountAsync(User user);
        public Task<User> LogInAsync(AuthorizationRequest authorizationRequest);
        public Task<User> UserByIdAsync(string id);
        public Task<bool> IsLikedMovieAsync(string userId, string movieId);
        public Task       Like(string userId, string itemId, string itemType);
        public Task       Unlike(string userId, string itemId, string itemType);
    }
}
