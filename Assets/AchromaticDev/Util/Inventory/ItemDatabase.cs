using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AchromaticDev.Util.Inventory
{
    public static class ItemDatabase
    {
        static Dictionary<string, ItemBase> _items = new Dictionary<string, ItemBase>();

        public static void RegisterItem<T>() where T : ItemBase
        {
            var item = System.Activator.CreateInstance<T>();
            _items.Add(item.Name, item);
        }

        public static ItemBase GetItem(string name)
        {
            return _items[name];
        }
    }
}
