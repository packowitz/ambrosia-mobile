using System;
using UnityEngine;
using UnityEngine.UI;

namespace Metagame
{
    public class BuildingController : MonoBehaviour
    {
        [SerializeField] private Button aButton;

        private void Start()
        {
            aButton.onClick.AddListener(() =>
            {
                Debug.Log("building button clicked");
            });
        }
    }
}