namespace DalApi
{
    public interface ICrud<T> where T : struct
    {
        public int AddObject(T o1);
        public void UpDateObject(T o);
        public void DeleteObject(int id);
        public T? GetObject(int id);
        IEnumerable<T?> GetAllObject(Func<T?, bool>? func = null);
        public T? GetObjectByFilter(Func<T?, bool>? func);
    }
}
