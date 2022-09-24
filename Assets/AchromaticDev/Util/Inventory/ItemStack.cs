using System;
using UnityEngine;

namespace AchromaticDev.Util.Inventory
{
    [Serializable]
    public class ItemStack<T> where T : Enum
    {
        public T Type => _type;
        [SerializeField] T _type;

        public Inventory<T> Inventory => _inventory;
        [NonSerialized] internal Inventory<T> _inventory;

        public int Count
        {
            get => _count;
            set => SetCount(value);
        }
        [SerializeField] int _count;

        public ItemStack(T type, int count)
        {
            _type = type;
            _count = count;
        }

        public void SetCount(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException("Count cannot be less than 0");

            if (count > ItemDatabase<T>.GetItemInfo(_type).MaxStack)
                throw new ArgumentOutOfRangeException("Count cannot be greater than the max stack size");
        }
    }
}