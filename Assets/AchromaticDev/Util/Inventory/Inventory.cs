using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AchromaticDev.Util.Inventory
{
    [Serializable]
    public class Inventory<T> : IEnumerable where T : Enum
    {
        public int Size => _size;
        [SerializeField] int _size;

        internal Action<ItemStack<T>> _onItemAdded;
        internal Action<ItemStack<T>> _onItemRemoved;

        [SerializeField] private List<ItemStack<T>> items = new List<ItemStack<T>>();

        public Inventory(int size)
        {
            _size = size;
        }

        public IEnumerator GetEnumerator()
        {
            return items.GetEnumerator();
        }

        public void AddItem(T type, int count)
        {
            var item = GetItem(type);
            if (item == null)
            {
                if (items.Count >= _size)
                    return;

                item = new ItemStack<T>(type, count);
                item._inventory = this;
                items.Add(item);
                _onItemAdded?.Invoke(item);
            }
            else
            {
                item.Count += count;
            }
        }

        public void RemoveItem(T type, int count)
        {
            var item = GetItem(type);
            if (item == null)
                return;

            item.Count -= count;
            if (item.Count <= 0)
            {
                items.Remove(item);
                _onItemRemoved?.Invoke(item);
            }
        }

        public ItemStack<T> GetItem(T type)
        {
            foreach (var item in items)
            {
                if (item.Type.Equals(type))
                    return item;
            }

            return null;
        }

        public bool IsEmpty()
        {
            return items.Count == 0;
        }

        public bool IsFull()
        {
            return items.Count == _size;
        }
    }
}
