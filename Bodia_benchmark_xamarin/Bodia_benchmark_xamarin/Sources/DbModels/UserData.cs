using System;
using SQLite;

namespace Bodia_benchmark_xamarin.Sources.DbModels
{
    public class UserData
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Surname { get; set; }

        public DateTime BirthDate { get; set; }

        public override string ToString()
        {
            return $"[Person: ID={ID}, FirstName={Name}, LastName={Surname}]";
        }
    }
}