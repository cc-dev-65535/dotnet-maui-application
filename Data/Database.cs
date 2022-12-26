using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scratch.Models;
using SQLite;

namespace Scratch.Data
{
    public class Database
    {

        private readonly SQLiteAsyncConnection _connection;

        public Database()
        {
            var dataDir = FileSystem.AppDataDirectory;
            var databasePath = Path.Combine(dataDir, "NoteStorage.db");
            Console.WriteLine(databasePath);

            var dbOptions = new SQLiteConnectionString(databasePath, true);

            _connection = new SQLiteAsyncConnection(dbOptions);

            _ = Initialize();
        }

        private async Task Initialize()
        {
            await _connection.CreateTableAsync<NoteItem>();
        }

        public async Task<int> AddNote(NoteItem item)
        {
            return await _connection.InsertAsync(item);
        }

        public async Task<List<NoteItem>> GetNotes()
        {
            return await _connection.Table<NoteItem>().OrderByDescending(a => a.CreatedDate).ToListAsync();
        }

        public async Task<NoteItem> GetNote(int id)
        {
            var query = _connection.Table<NoteItem>().Where(t => t.Id == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<int> DeleteNote(NoteItem item)
        {
            return await _connection.DeleteAsync(item);
        }

        public async Task<int> DeleteNote(int id)
        {
            return await _connection.DeleteAsync<NoteItem>(id);
        }

        public async Task<int> DeleteAllNotes()
        {
            return await _connection.DeleteAllAsync<NoteItem>();
        }

        public async Task<int> UpdateNote(NoteItem item)
        {
            return await _connection.UpdateAsync(item);
        }
    }
}
