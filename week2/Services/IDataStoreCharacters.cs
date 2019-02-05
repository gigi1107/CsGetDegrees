using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace week2.Services
{
    public interface IDataStoreCharacters<T>
    {
        Task<bool> AddCharacterAsync(T character);
        Task<bool> UpdateCharacterAsync(T character);
        Task<bool> DeleteCharacterAsync(string id);
        Task<T> GetCharacterAsync(string id);
        Task<IEnumerable<T>> GetCharactersAsync(bool forceRefresh = false);
    }
}

