﻿using fgk.Core.Models;

namespace fgk.Application.Interfaces
{
    public interface IAccountService
    {
        Task<string?> LogInAsync(string? email, string? password);
        Task<string?> CreateAccountAsync(string? email, string? username, string? password);
        Task<Account?> GetByIdAsync(string id);
        Task<Account?> LikeMovieAsync(Account account, Movie movie);
    }
}