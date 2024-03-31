﻿using fgk.Application.Interfaces;
using fgk.Core.Abstractions;
using fgk.Core.Models;

namespace fgk.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMoviesRepository _moviesRepository;

        public MovieService(IMoviesRepository moviesRepository)
        {
            this._moviesRepository = moviesRepository;
        }

        public async Task<List<Movie>> GetMoviesByTitle(string title)
        {
            return (await _moviesRepository.GetAsync())
                .Where(item => item.Title.Contains(title, StringComparison.CurrentCultureIgnoreCase))
                .ToList();
        }

        public async Task<Movie?> GetMovieByTitleQuery(string titleQuery)
        {
            return await _moviesRepository.GetByTitleAsync(titleQuery);
        }

        public async Task<Movie?> LikeMovieAsync(Movie movie)
        {
            return await _moviesRepository.LikeMovieAsync(movie);
        }
    }
}