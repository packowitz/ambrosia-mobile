using Backend.Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Metagame.TasksScreen
{
    public class DailyActivityController : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image border;
        [SerializeField] private Image background;
        [SerializeField] private Image checkImg;
        [SerializeField] private LootItemPrefabController lootItem;
        [SerializeField] private GameObject claimableAlert;

        public int day;
        public Color claimedBorder;
        public Color claimedBackground;
        public Color activeDayBorder;
        public Color activeDayBackground;
        public Color futureDayBorder;
        public Color futureDayBackground;
        private bool activeDay;
        private bool itemClaimed;

        public void SetLootItem(LootedItem item)
        {
            lootItem.SetItem(item);
        }

        public void SetClaimable(bool claimable)
        {
            claimableAlert.SetActive(claimable);
        }

        public void SetActiveDay(bool active)
        {
            activeDay = active;
            SetClaimed(itemClaimed);
        }

        public void SetClaimed(bool claimed)
        {
            itemClaimed = claimed;
            checkImg.gameObject.SetActive(itemClaimed);
            lootItem.gameObject.SetActive(!itemClaimed);

            if (activeDay)
            {
                border.color = activeDayBorder;
                background.color = activeDayBackground;
            }
            else
            {
                border.color = itemClaimed ? claimedBorder : futureDayBorder;
                background.color = itemClaimed ? claimedBackground : futureDayBackground;
            }
        }

        public void AddClickListener(UnityAction call)
        {
            button.onClick.AddListener(call);
        }
    }
}