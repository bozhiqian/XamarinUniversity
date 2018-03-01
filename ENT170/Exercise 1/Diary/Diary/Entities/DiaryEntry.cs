using System;
using Prism.Mvvm;
using SQLite;

namespace Diary.Entities
{
    [Table("DiaryEntry")]
    public class DiaryEntry: BindableBase
    {
        private DateTime _timestamp;

        public DiaryEntry()
        {
            Timestamp = DateTime.Now;
        }

        public DiaryEntry(byte[] initializationVector, byte[] cipherText, string account)
            : this()
        {
            InitializationVector = initializationVector;
            CipherText = cipherText;
            AccountName = account;
        }

        [PrimaryKey] [AutoIncrement]
        public int Id { get; set; } = -1;

        public DateTime Timestamp
        {
            get => _timestamp;
            set => SetProperty(ref _timestamp, value);
        }

        public byte[] InitializationVector { get; set; }
        public byte[] CipherText { get; set; }
        public string AccountName { get; set; }

    }
}