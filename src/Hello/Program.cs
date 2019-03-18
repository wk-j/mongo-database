using System;
using MongoDB.Driver;
using System.Linq;
using MongoDB.Bson;

namespace Hello {
    class Student {
        public ObjectId Id { set; get; }
        public string Name { set; get; }
        public string LastName { set; get; }
    }

    class Program {
        static void Main(string[] args) {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("hello");

            db.DropCollection("students");
            var students = db.GetCollection<Student>("students");


            students.InsertMany(new[] {
                new Student { Name = "wk" },
                new Student { Name = "jw" },
                new Student { Name = "tk" },
            });

            foreach (var item in students.AsQueryable().Where(x => x.Name == "wk")) {
                Console.WriteLine($"{item.Id} {item.Name} {item.LastName}");
            }

            var updateRef = Builders<Student>.Update.Set(x => x.LastName, "kw");
            students.UpdateOne(x => x.Name == "wk", updateRef);

            foreach (var item in students.AsQueryable().Where(x => x.Name == "wk")) {
                Console.WriteLine($"{item.Id} {item.Name} {item.LastName}");
            }
        }
    }
}