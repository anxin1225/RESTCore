using System;
using System.Collections.Generic;
using System.Text;

namespace DataSource
{
    public class MemoryDataSource<T> : IDataSource<T>
    {
        private T _Data;

        public T Get()
        {
            return _Data;
        }

        public void Set(T data)
        {
            _Data = data;
        }
    }

    public class MemoryListDataSource<T> : IListDataSource<T>
        where T : IListDataItem
    {
        private List<T> _ListData;

        public void Add(T data)
        {
            throw new NotImplementedException();
        }

        public bool DaleteByPK(string pk)
        {
            throw new NotImplementedException();
        }

        public bool Delete(T data)
        {
            throw new NotImplementedException();
        }

        public T FirstOrDefault(Func<T, bool> matching = null)
        {
            throw new NotImplementedException();
        }

        public List<T> Get()
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll(Func<T, bool> matching = null)
        {
            throw new NotImplementedException();
        }

        public T GetByPK(string pk)
        {
            throw new NotImplementedException();
        }

        public void Set(List<T> data)
        {
            throw new NotImplementedException();
        }

        public List<T> Top(int count, Func<T, bool> matching = null)
        {
            throw new NotImplementedException();
        }

        public bool Update(T data)
        {
            throw new NotImplementedException();
        }
    }
}
