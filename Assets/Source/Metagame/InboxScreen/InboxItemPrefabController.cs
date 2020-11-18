using Backend.Models;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Metagame.InboxScreen
{
    public class InboxItemPrefabController : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text amount;
        [SerializeField] private SpriteAtlas resourcesAtlas;

        public void SetItem(InboxMessageItem item)
        {
            // TODO currently only resources can be an item
            
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (item.resourceType != null)
            {
                icon.sprite = resourcesAtlas.GetSprite(item.resourceType.ToString());
                amount.text = $"{item.resourceAmount}";
            }
        }
    }
}