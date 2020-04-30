using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace offlineFirebase
{
    public interface IFirebaseRepository<T>
    {
        T GetOne(string id);
        Task<T> GetOneAsync(string id);

      /*  IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
*/
        Task<List<T>> NewGetAll();

        Boolean SetPerson(int id,string name,int age);

        Task<List<Persons>> NewGetAllAndListens();
    }
}
