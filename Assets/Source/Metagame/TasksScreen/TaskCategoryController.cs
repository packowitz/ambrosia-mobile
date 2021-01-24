using Backend.Models.Enums;
using TMPro;
using UnityEngine;

namespace Metagame.TasksScreen
{
    public class TaskCategoryController : MonoBehaviour
    {
        [SerializeField] private TMP_Text categoryTxt;

        public void SetCategory(TaskCategory category)
        {
            switch (category)
            {
                case TaskCategory.BUILDER:
                    categoryTxt.text = "Builder Tasks";
                    break;
                case TaskCategory.ACTIVITY:
                    categoryTxt.text = "Activity Tasks";
                    break;
                case TaskCategory.LABORATORY:
                    categoryTxt.text = "Laboratory Tasks";
                    break;
                case TaskCategory.RESOURCE_SPENT:
                    categoryTxt.text = "Resource Tasks";
                    break;
            }
        }
    }
}