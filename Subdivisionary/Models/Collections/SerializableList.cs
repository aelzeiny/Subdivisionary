using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace Subdivisionary.Models.Collections
{
    public interface ICollectionAdd
    {
        void AddObject(object o);
        void AddObjectUntilIndex(int index, object value, object blankItem);
    }
    /// <summary>
    /// Entity Framework does not allow lists of any kind in databases, but sometimes
    /// we need an array alternative. Csv List uses strings to serialize 
    /// an array of objects as a Comma-seperated-value list.
    /// </summary>
    [ComplexType]
    public abstract class SerializableList<T> : ICollection<T>, ICollectionAdd
    {

        /// <summary>
        /// Stores list of objects once loaded from database
        /// </summary>
        [NotMapped]
        protected List<T> data;

        /// <summary>
        /// Constructor init list as empty
        /// </summary>
        protected SerializableList()
        {
            data = new List<T>();
        }

        public T this[int key]
        {
            get { return data[key]; }
            set { data[key] = value; }
        } 

        /// <summary>
        /// Populates data field from database
        /// GET: Converts an object value into a serializable string
        /// SET: Converts a serializable string into objects
        /// </summary>
        public string Data
        {
            get
            {
                string serializedValue = JsonConvert.SerializeObject(data);
                return serializedValue;
            }
            set
            {
                data.Clear();
                if (value.IsNullOrWhiteSpace())
                    return;
                data = JsonConvert.DeserializeObject<List<T>>(value);
            }
        }
        
        public void AddUntilIndex(int index, T item, T blankItem)
        {
            if (index >= data.Count)
                for (int i = data.Count - 1; i != index; i++)
                    data.Add(blankItem);
            data[index] = item;
        }

        #region Useful Iterface Implementation
        public void AddObject(object item)
        {
            this.Add((T)item);
        }

        public void AddObjectUntilIndex(int index, object item, object blankItem)
        {
            this.AddUntilIndex(index, (T)item, (T)blankItem);
        }
        #endregion

        #region Boring ICollection Stuff

        public int IndexOf(T item)
        {
            return data.IndexOf(item);
        }
        public void Add(T item)
        {
            data.Add(item);
        }

        public void Clear()
        {
            data.Clear();
        }

        public void RemoveAt(int index)
        {
            data.RemoveAt(index);
        }

        public bool Contains(T item)
        {
            return data.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            data.CopyTo(array, arrayIndex);
        }
        
        public void AddRange(IEnumerable<T> uploads)
        {
            this.data.AddRange(uploads);
        }

        public List<T> ToList()
        {
            return this.data;
        }

        public int Count
        {
            get { return data.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public T First()
        {
            return data.First();
        }

        public bool Remove(T item)
        {
            return data.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }
        #endregion
    }
}