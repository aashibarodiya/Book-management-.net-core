using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ConceptArchitect.BookManagement
{
    public class RoleInfo
    {
        [Key]
        public string Name { get; set; }
    }

    public class Role : RoleInfo 
    {
        [BsonIgnore]
        public virtual List<User> Users { get; set; } = new List<User>();
    }
}