using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T> where T:struct
    {
        public int AddObject(T o1);
        public void UpDateObject(T o);
        public void DeleteObject(int id);
        public T GetObject(int id);
        IEnumerable<T> GetAllObject();
    }
}
