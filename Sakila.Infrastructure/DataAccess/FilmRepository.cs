﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sakila.Core.Inventory.Movies.Interfaces;
using Sakila.Core.Inventory.Movies.Models;

namespace Sakila.Infrastructure.DataAccess
{
    public class FilmRepository : IFilmRepository
    {
        private readonly MySqlContext _mySqlContext;

        public FilmRepository(MySqlContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
        }

        public async Task<IEnumerable<Film>> GetAllFilmsAsync()
        {
            return await _mySqlContext.Film.ToListAsync();
        }
        public async Task<Film?> GetFilmByFilmIdAsync(int filmId)
        {
            return await _mySqlContext.Film.Where(f => f.FilmId == filmId).FirstOrDefaultAsync();
        }

        public async Task<int> AddFilmAsync(Film film)
        {
            _mySqlContext.Film.Add(film);

            return await _mySqlContext.SaveChangesAsync();
        }

        public async Task<int> UpdateFilmAsync(Film film)
        {
            _mySqlContext.Film.Update(film);

            return await _mySqlContext.SaveChangesAsync();
        }

        public async Task<int> DeleteFilmByFilmIdAsync(int filmId)
        {
            var film = await GetFilmByFilmIdAsync(filmId);

            if (film == null)
                return 0;

            return await DeleteFilm(film);
        }
        public async Task<int> DeleteFilm(Film film)
        {
            _mySqlContext.Film.Remove(film);

            return await _mySqlContext.SaveChangesAsync();
        }
    }
}