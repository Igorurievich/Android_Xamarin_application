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

        public void CreateTable()
        {
            var db = new SQLiteConnection(_dbPath);
            db.DeleteAll<UserData>();
            db.DropTable<UserData>();
            db.CreateTable<UserData>();
        }

        public void DeleteAll()
        {
            var db = new SQLiteConnection(_dbPath);
            db.DeleteAll<UserData>();
        }

        public void InsertRecord(string name, string surname, DateTime birthdate)
        {
            var db = new SQLiteConnection(_dbPath);
            UserData item = new UserData()
            {
                Name = name,
                Surname = surname,
                BirthDate = birthdate
            };
            db.Insert(item);
        }

        public void InsertRecord(UserData userDataObj)
        {
            var db = new SQLiteConnection(_dbPath);
            db.Insert(userDataObj);
        }

        public List<UserData> GetAllRecords()
        {
            var db = new SQLiteConnection(_dbPath);
        
            List<UserData> items = new List<UserData>(db.Table<UserData>());
            return items;
        }

        public UserData GetRecordById(int id)
        {
            var db = new SQLiteConnection(_dbPath);

            var item = db.Get<UserData>(id);
            return item;
        }

        public void UpdateRecord(UserData task)
        {
            var db = new SQLiteConnection(_dbPath);
            db.Update(task);
        }

        public void RemoveUserById(int id)
        {
            var db = new SQLiteConnection(_dbPath);
            var item = db.Get<UserData>(id);
            db.Delete(item);
        }
    }
}