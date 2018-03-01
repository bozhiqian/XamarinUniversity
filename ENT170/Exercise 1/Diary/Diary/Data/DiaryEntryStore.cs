using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Diary.Entities;
using SQLite;

namespace Diary.Data
{
    public class DiaryEntryStore
    {
        private static string _dbPath;
        private readonly SQLiteAsyncConnection _connection;
        
        public DiaryEntryStore(string dbPath)
        {
            _dbPath = dbPath;
            _connection = new SQLiteAsyncConnection(dbPath);
            _connection.CreateTableAsync<DiaryEntry>().Wait();
        }
        public DiaryEntryStore(string folder, string filename) : this(_dbPath)
        {
            if (string.IsNullOrEmpty(folder) || string.IsNullOrWhiteSpace(filename))
                return;

            _dbPath = System.IO.Path.Combine(folder, filename);
        }

        public Task SaveEntryAsync(DiaryEntry entry)
        {
            if (entry.Id == -1)
                return _connection.InsertAsync(entry);
            else
                return _connection.InsertOrReplaceAsync(entry);
        }

        public Task DeleteEntryAsync(DiaryEntry entry)
        {
            return _connection.DeleteAsync(entry);
        }

        public Task<List<DiaryEntry>> GetEntriesAsync(string accountName)
        {
            return _connection.Table<DiaryEntry>().Where(d => d.AccountName == accountName).ToListAsync();
        }

        public Task<DiaryEntry> GetEntry(int id)
        {
            return _connection.Table<DiaryEntry>().Where(d => d.Id == id).FirstOrDefaultAsync();
        }
    }
}
