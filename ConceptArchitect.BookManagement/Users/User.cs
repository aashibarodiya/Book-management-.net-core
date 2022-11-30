using ConceptArchitect.BookManagement.ModelValidators;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{


    public class UserInfo
    {


        public string Name { get; set; }

        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Email { get; set; }

        [ExistingRoles]
        [BsonIgnore]
        //[BsonRepresentation(BsonType.Array)]
        public virtual List<Role> Roles { get; set; } = new List<Role>();

        public string ProfilePicture { get; set; }

        [BsonIgnore]
        public virtual List<FavoriteBook> FavoritieBooks { get; set; } = new List<FavoriteBook>();

        [BsonIgnore]
        public virtual List<FavoriteAuthor> FavoriteAuthors { get; set; } = new List<FavoriteAuthor>();

    }


    public class User : UserInfo
    {
        public string Password { get; set; }


    }



}