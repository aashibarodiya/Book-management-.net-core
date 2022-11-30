namespace BooksWeb.Config
{
    public class MongoConfig
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string BookCollectionName { get; set; }
        public string AuthorsCollectionName { get; set; }
        public string UsersCollectionName { get; set; }
    }
}
