using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataSource
{
    public class MemoryDataSource<T> : IDataSource<T>
    {
        public MemoryDataSource()
        {
            SourceName = GetType().Name;
        }

        public virtual string SourceName { get; set; }

        public virtual string SerializationFileName { get { return $"{SourceName}.txt"; } }

        protected T Data { get; set; }

        public T Get()
        {
            return Data;
        }

        public void Set(T data)
        {
            Data = data;
        }

        public virtual void Serialization()
        {
            using (StreamWriter sw = new StreamWriter(SerializationFileName))
            {
                sw.Write(JsonConvert.SerializeObject(Data));
            }
        }

        public virtual void DeSerialization()
        {
            using (StreamReader sr = new StreamReader(SerializationFileName))
            {
                var tmp = JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
                if (tmp != null)
                    Data = tmp;
            }
        }
    }

    public class MemoryListDataSource<T> : MemoryDataSource<List<T>>, IListDataSource<T>
    {
        public void Add(T data)
        {
            lock (Data)
                Data.Add(data);
        }

        public bool Delete(T data)
        {
            lock (Data)
                return Data.Remove(data);
        }

        public T FirstOrDefault(Func<T, bool> matching = null)
        {
            lock (Data)
                return Data.FirstOrDefault(matching);
        }

        public List<T> GetAll(Func<T, bool> matching = null)
        {
            lock (Data)
                return Data.Where(matching).ToList();
        }

        public List<T> Top(int count, Func<T, bool> matching = null)
        {
            lock (Data)
            {
                List<T> temp = new List<T>();

                int i = 0;
                foreach (var item in Data)
                {
                    if (i > count)
                        break;

                    if (matching?.Invoke(item) ?? true)
                    {
                        temp.Add(item);
                        ++i;
                    }
                }

                return temp;
            }
        }
    }

    public class MemoryMappingDataSource<K, V> : MemoryListDataSource<KeyValuePair<K, V>>, IMappingDataSource<K, V>
        where K : class
        where V : class
    {
        public V GetValue(K key)
        {
            lock (Data)
                return Data.FirstOrDefault(n => n.Key == key).Value;
        }

        public void SetValue(K key, V value)
        {
            lock (Data)
            {
                Data.RemoveAll(n => n.Key == key);
                Data.Add(new KeyValuePair<K, V>(key, value));
            }
        }
    }
}
