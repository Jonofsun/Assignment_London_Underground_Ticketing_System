using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_London_Underground_Ticketing_System
{
    public class JonathansList<T> : IEnumerable<T> where T : Rider
    {
        private T[] _items;
        private int _count;
        private const int InitialCapacity = 10;

        public JonathansList()
        {
            _items = new T[InitialCapacity];
            _count = 0;
        }
        public JonathansList(int initialSize)
        {
            _items = new T[initialSize]; _count = 0;
        }
        private void CheckIntegrity()
        {
            if(_count >= 0.8 * _items.Length)
            {
                T[] newAAR = new T[_items.Length + 2];
                Array.Copy(_items, newAAR, _count);
                _items = newAAR;
            }
        }
        public void Add(T item)
        {
            CheckIntegrity();
            _items[_count++] = item;
            
        }
        public void AddAtIndex(T item, int index)
        {
            CheckIntegrity();
            for (int i = _count - 1; i >= index; i--)
            {
                _items[i + 1] = _items[i];
            }
            _items[index] = item;
            _count++;
        }
        public void RemoveAtIndex(int index)
        {
            if(index < 0 || index >= _count) return;
            for (int i = index + 1; i < _count; i++)
            {
                _items[i - 1] = _items[i];
            }
            _count--;
        }
        public T? GetItem(int index)
        {
            if( index < 0 || index >= _count)
            {
                return default(T);
            }
            return _items[index];
        }
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        public JonathansList<T> ReturnRidersAtStation(int index)
        {
            JonathansList<T> values = new JonathansList<T>();
            foreach (T item in this)
            {
                if((int)item.StationOn == index)
                {
                    values.Add(item);
                }
            }
            return values;
        }
        public JonathansList<T> ReturnRidersAtStation(Station station)
        {
            return this.ReturnRidersAtStation((int)station);
        }
        public JonathansList<T> ReturnAllActiveRiders()
        {
            JonathansList<T> values = new JonathansList<T>();
            foreach (T item in this)
            {
                if (item.IsActive) { values.Add(item); }
            }
            return values;
        }
    }
}
