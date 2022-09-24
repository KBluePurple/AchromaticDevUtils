using System;

namespace AchromaticDev.Util.Inventory
{
    public class ItemStack<T> where T : Enum
    {
        public T Type => _type;
        private readonly T _type;

        public Inventory<T> Inventory => _inventory;
        internal Inventory<T> _inventory;

        public int Count
        {
            get => _count;
            set => SetCount(value);
        }
        private int _count;

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