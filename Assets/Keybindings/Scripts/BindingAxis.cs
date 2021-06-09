using System;

using UnityEngine;

namespace Keybinds
{
    [Serializable]
    public class BindingAxis
    {
        public string Name { get { return name; } }
        public KeyCode Positive { get { return positive.Value; } }
        public KeyCode Negative { get { return negative.Value; } }
        public string ValueDisplay { get { return $"{positive.ValueDisplay} - {negative.ValueDisplay}"; } }

        [SerializeField]
        private string name;
        [SerializeField]
        private KeyCode defaultPositive = KeyCode.W;
        [SerializeField]
        private KeyCode defaultNegative = KeyCode.S;

        private Binding positive;
        private Binding negative;

        public BindingAxis()
        {
            positive = new Binding(name + "-positive", defaultPositive);
            negative = new Binding(name + "-negative", defaultNegative);
        }

        public void Rebind(KeyCode _positive, KeyCode _negative)
        {
            positive.Rebind(_positive);
            negative.Rebind(_negative);
        }

        public float Axis()
        {
            float axis = 0;

            axis += positive.Held() ? 1 : 0;
            axis -= negative.Held() ? 1 : 0;

            return axis;
        }

        public void Load()
        {
            positive = new Binding(name + "-positive", defaultPositive);
            negative = new Binding(name + "-negative", defaultNegative);

            positive.Load();
            negative.Load();
        }

        public void Save()
        {
            positive.Save();
            negative.Save();
        }
    }
}