using System;
using UnityEngine;

namespace AchromaticDev.Util.Inventory
{
    [Serializable]
    public abstract class ItemBase
    {
        public Sprite Icon => _icon;
        [SerializeField] Sprite _icon;

        public string Name => _name;
        [SerializeField] string _name;

        public string Description => _description;
        [SerializeField] string _description;

        public int MaxStack => _maxStack;
        [SerializeField] int _maxStack;

        public bool IsStackable => _isStackable;
        [SerializeField] bool _isStackable;

        static ItemBase()
        {
            ItemDatabase.RegisterItem<ItemBase>();
        }

        public ItemBase(Sprite icon, string name, string description, int maxStack, bool isStackable)
        {
            _icon = icon;
            _name = name;
            _description = description;
            _maxStack = maxStack;
            _isStackable = isStackable;
        }
    }
}