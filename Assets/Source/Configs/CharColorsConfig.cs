using System;
using System.Collections.Generic;
using UnityEngine;
using Color = Backend.Models.Color;

namespace Configs
{
    [CreateAssetMenu(fileName = "CharColorsConfig", menuName = "Ambrosia/Configs/CharColorsConfig")]
    public class CharColorsConfig : ScriptableObject
    {
        [SerializeField] private List<CharColorConfig> configs;

        public CharColorConfig GetConfig(Color color) => configs.Find(c => c.color == color);
    }

    [Serializable]
    public struct CharColorConfig
    {
        public Color color;
        public UnityEngine.Color playerBorderColor;
    }
}