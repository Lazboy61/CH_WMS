using System.Collections;

public interface ICRUDinterface<T>
{
    // Create Read Update Delete
    Task<bool> Post(T target);
    T Get(int id);
    List<T> GetAll();
    Task<bool> Put(T target); //replace entirely
    bool Patch(T target); // replace partially // these 2 i find way too hard to use with generics
    bool Delete(int id);
    bool Delete(T target);
    
}