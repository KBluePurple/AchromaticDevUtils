
namespace AchromaticDev.Util.Inventory
{
    public class InventoryItem
    {
        public ItemBase Info => _info;
        private ItemBase _info;
        private Inventory _inventory;

        public int Count => _count;
        private int _count;

        public InventoryItem(ItemBase itemBase, int count)
        {
            _info = itemBase;
            _count = count;
        }

        internal void Add(int count)
        {
            _count += count;
        }
        
        internal void Remove(int count)
        {
            _count -= count;
        }

        internal void SetCount(int count)
        {
            
        }
    }
}