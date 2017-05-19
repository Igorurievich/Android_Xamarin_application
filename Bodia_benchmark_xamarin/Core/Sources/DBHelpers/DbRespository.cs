using System;
using System.IO;
using SQLite;
using System.Collections.Generic;
using Bodia_benchmark_xamarin.Sources.DbModels;

namespace Bodia_benchmark_xamarin.Sources.DBHelpers
{
    public class DbRespository
    {
        private readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
            Globals.UsersDatabaseName);

        public void CreateDb()
        {
            var sqLiteConnection = new SQLiteConnection(_dbPath);
        }

        public bool CreateTable()
        {
            var db = new SQLiteConnection(_dbPath);
            //db.DeleteAll<UserData>();
            db.CreateTable<UserData>();
            return true;
        }

        public bool InsertRecord(string name, string surname, DateTime birthdate)
        {
            var db = new SQLiteConnection(_dbPath);
            UserData item = new UserData()
            {
                Name = name,
                Surname = surname,
                BirthDate = birthdate
            };
            db.Insert(item);
            return true;
        }

        public List<UserData> GetAllRecords()
        {
            var db = new SQLiteConnection(_dbPath);
        
            List<UserData> item = new List<UserData>(db.Table<UserData>());
            return item;
        }

        public UserData GetTaskById(int id)
        {
            var db = new SQLiteConnection(_dbPath);

            var item = db.Get<UserData>(id);
            return item;
        }

        public bool UpdateRecord(int id, UserData task)
        {
            var db = new SQLiteConnection(_dbPath);

            db.Update(task);
            return true;
        }

        public bool RemoveUserById(int id)
        {
            var db = new SQLiteConnection(_dbPath);
            var item = db.Get<UserData>(id);
            db.Delete(item);
            return true;
        }
    }
}