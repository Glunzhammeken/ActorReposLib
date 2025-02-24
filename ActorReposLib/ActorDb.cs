using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActorReposLib.Interfaces;

namespace ActorReposLib
{
    public class ActorDb<T> where T : class, IIdentifiable
    {
        private List<T> list = new List<T>();
        private int nextid = 1;
        
        public List<T> GetList()
        {
            return new List<T>(list);
        }
        public T GetItemById(int id)
        {
            T? item = list.Find(i => i.Id == id);
            
            if (item == null)
            {
                throw new ArgumentNullException("item is not in the list");
            }
            return item;
        }

        public T Add(T item) 
        {

            if(item == null) {throw new ArgumentNullException("item is null"); }
            item.Id = nextid++;
            list.Add(item);
            return item;
        }
        public T Remove(int id)
        {
            T? item = GetItemById(id);
            if(item == null) { throw new ArgumentNullException("item is null"); }
            list.Remove(item);
            return item;
        }
        public T Update(int id, T newData)
        {
            T? existingItem = list.Find(i => i.Id == id);
            if (existingItem == null) { throw new ArgumentNullException("item is null"); }

            // Use reflection to update properties dynamically
            foreach (var prop in typeof(T).GetProperties())
            {
                if (prop.CanWrite) // Ensure property is writable
                {
                    var newValue = prop.GetValue(newData);
                    if (newValue != null) // Avoid overwriting with null
                    {
                        prop.SetValue(existingItem, newValue);
                    }
                }
            }
            return existingItem;
        }









    }
}
