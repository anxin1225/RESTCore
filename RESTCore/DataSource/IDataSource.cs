using System;
using System.Collections.Generic;
using System.Text;

namespace DataSource
{
    public interface IListDataItem
    {
        string GetPrimaryKey();
    }

    public interface IDataSource<T>
    {
        void Set(T data);

        T Get();
    }

    public interface IListDataSource<T> : IDataSource<List<T>>
        where T : IListDataItem
    {
        void Add(T data);

        bool Delete(T data);

        bool DaleteByPK(string pk);

        bool Update(T data);

        T GetByPK(string pk);

        T FirstOrDefault(Func<T, bool> matching = null);

        List<T> GetAll(Func<T, bool> matching = null);

        List<T> Top(int count, Func<T, bool> matching = null);
    }
}
