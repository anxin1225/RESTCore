using System;
using System.Collections.Generic;
using System.Text;

namespace DataSource
{
    public interface IDataSource<T>
    {
        void Set(T data);

        T Get();
    }

    public interface IListDataSource<T> : IDataSource<List<T>>
    {
        void Add(T data);

        bool Delete(T data);

        T FirstOrDefault(Func<T, bool> matching = null);

        List<T> GetAll(Func<T, bool> matching = null);

        List<T> Top(int count, Func<T, bool> matching = null);
    }

    public interface IMappingDataSource<K, V> : IListDataSource<KeyValuePair<K, V>>
    {
        V GetValue(K key);

        void SetValue(K key, V value);
    }
}
