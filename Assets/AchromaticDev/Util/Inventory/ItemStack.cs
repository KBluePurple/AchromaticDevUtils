using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace AchromaticDev.Util.Inventory
{
    [Serializable]
    public class ItemStack<T> where T : Enum
    {
        public T Type => type;
        [FormerlySerializedAs("_type")] [SerializeField] T type;
        
        public Inventory<T> Inventory { get; internal set; }

        public int Count
        {
            get => count;
            set => SetCount(value);
        }
        [FormerlySerializedAs("_count")] [SerializeField] int count;

        public ItemStack(T type, int count)
        {
            this.type = type;
            this.count = count;
        }

        public void SetCount(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException("Count cannot be less than 0");

            if (count > ItemDatabase<T>.GetItemInfo(type).MaxStack)
                throw new ArgumentOutOfRangeException("Count cannot be greater than the max stack size");
        }
    }
}