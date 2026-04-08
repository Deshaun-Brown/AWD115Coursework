using Chapter_8_1_Student_Project.Models;

namespace Chapter_8_1_Student_Project.Data;

public interface IRepository<T> where T : class
{
    IEnumerable<T> List(QueryOptions<T> options);
    T? Get(int id);
    void Insert(T entity);
    void Update(T entity);
    void Delete(T entity);
    void Save();
}