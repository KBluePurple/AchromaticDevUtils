using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AchromaticDev.Util.Inventory
{
    [Serializable]
    public class Inventory
    {
        public int Size => _size;
        [SerializeField] int _size;

        private Action<InventoryItem> _onItemAdded;
        private Action<InventoryItem> _onItemRemoved;

        [SerializeField] private List<InventoryItem> items = new List<InventoryItem>();

        public Inventory(int size)
        {
            _size = size;
        }
    }
}
