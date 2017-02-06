using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace Subdivisionary.Models.Collections
{
    /// <summary>
    /// Entity Framework does not allow lists of any kind in databases, but sometimes
    /// we need an array alternative. Csv List uses strings to serialize 
    /// an array of objects as a Comma-seperated-value list.
    /// </summary>
    [ComplexType]
    public abstract class SerializableList<T> : ICollection<T>
    {
        /// <summary>
        /// DO NOT alter value of the seperator. Lists are forever comma seperated. Character conflicts are
        /// resolved through Regular Expressions.
        /// </summary>
        protected static readonly string[] SEPERATOR = {"|"};

        /// <summary>
        /// Stores list of objects once loaded from database
        /// </summary>
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
                string serializedValue = string.Join(SEPERATOR[0],
                    data.Select(x => SerializeObjectList(SerializeObject(x))));
                return serializedValue;
            }
            set
            {
                data.Clear();
                if (value.IsNullOrWhiteSpace())
                    return;
                var collection = value.Split(SEPERATOR, StringSplitOptions.None);
                for (int i = 0; i < collection.Length; i+=ParamCount)
                {
                    string[] toParse = new string[ParamCount];
                    for (int j = 0; j < ParamCount; j++)
                        toParse[j] = collection[i + j];
                    data.Add(ParseObject(toParse));
                }
            }
        }

        /// <summary>
        /// The string array is representative of multiple parameters of a single object
        /// This converts the multiple parameters into one cohesive string by resolving
        /// conflicts with seperator characters
        /// </summary>
        /// <param name="serializeParams">symbolic multiple parameters</param>
        /// <returns></returns>
        private string SerializeObjectList(string[] serializeParams)
        {
            if (serializeParams.Length != ParamCount)
                throw new IndexOutOfRangeException("Classes must serialze correctly");
            StringBuilder builder = new StringBuilder(serializeParams.Length);
            for (int i = 0; i < serializeParams.Length; i++)
            {
                string param = serializeParams[i];
                // Check for conflicts with seperator characters
                if (param != null)
                    foreach (string seperator in SEPERATOR)
                        param = param.Replace(seperator, "");
                builder.Append(param);
                // Deliniate seperate parameters with the seperator character
                if (i != serializeParams.Length - 1)
                    builder.Append(SEPERATOR[0]);
            }
            return builder.ToString();
        }

        public void AddUntilIndex(int index, T item, T blankItem)
        {
            if (index >= data.Count)
                for (int i = data.Count - 1; i != index; i++)
                    data.Add(blankItem);
            data[index] = item;
        }

        /// <summary>
        /// The number of parameters in each object.
        /// This ensures that subclasses do not have to deal with data parsing
        /// </summary>
        protected abstract int ParamCount { get; }

        /// <summary>
        /// Convert multiple parameters of string into a single object. Serialization order is 
        /// important, and is defined by subclass
        /// </summary>
        /// <param name="param">Parameters that need parsing to create the object</param>
        /// <returns>Parsed Object</returns>
        protected abstract T ParseObject(string[] param);

        /// <summary>
        /// Converts one object into an array of strings that represent parameters.
        /// Note that order of serialized parameters are defined here.
        /// </summary>
        /// <param name="serialize"></param>
        /// <returns>Serialized Object</returns>
        protected abstract string[] SerializeObject(T serialize);

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