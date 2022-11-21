using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T> where T:struct
    {
        public void Add (T newObject);
        public void Update (T updetedObject);
        public void Delete (int id);
        public T Get (int id);
        IEnumerable<T> GetAll ();
    }
}
