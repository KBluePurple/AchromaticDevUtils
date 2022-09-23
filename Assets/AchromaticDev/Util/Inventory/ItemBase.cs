using System;
using UnityEngine;

namespace AchromaticDev.Util.Inventory
{
    [Serializable]
    public abstract class ItemBase
    {
        public Sprite Icon => _icon;
        [SerializeField] private Sprite _icon;

        public string Name => _name;
        [SerializeField] private string _name;

        public string Description => _description;
        [SerializeField] private string _description;

        public int MaxStack => _maxStack;
        [SerializeField] private int _maxStack;

        public bool IsStackable => _isStackable;
        [SerializeField] private bool _isStackable;
    }
}