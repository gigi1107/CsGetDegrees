using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using week2.Models;


namespace week2.Services
{
    public class MockDataStoreChar : IDataStoreCharacters<Character>
    {
        List<Character> characters;

        public MockDataStoreChar()
        {
            characters = new List<Character>();
        }

        public async Task<bool> AddCharacterAsync(Character character)
        {
            characters.Add(character);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateCharacterAsync(Character character)
        {
            var oldChar = characters.Where((Character arg) => arg.Id == character.Id).FirstOrDefault();
            characters.Remove(oldChar);
            characters.Add(character);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteCharacterAsync(string id)
        {
            var oldChar = characters.Where((Character arg) => arg.Id == id).FirstOrDefault();
            characters.Remove(oldChar);

            return await Task.FromResult(true);

        }

        public async Task<Character> GetCharacterAsync(string id)
        {
            return await Task.FromResult(characters.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Character>> GetCharactersAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(characters);
        }
    }
}


