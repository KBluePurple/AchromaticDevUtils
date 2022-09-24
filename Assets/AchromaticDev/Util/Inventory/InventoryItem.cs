using System;

namespace AchromaticDev.Util.Inventory
{
    public class InventoryItem
    {
        public ItemBase Info => _info;
        private ItemBase _info;

        public Inventory Inventory => _inventory;
        private Inventory _inventory;

        public int Count => _count;
        private int _count;

        public InventoryItem(ItemBase itemBase, int count)
        {
            _info = itemBase;
            _count = count;
        }
    }
}