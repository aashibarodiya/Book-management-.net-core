namespace ConceptArchitect.BookManagement
{
    public interface IDataSeeder<T>
    {
        Task Seed();
        //
    }
}