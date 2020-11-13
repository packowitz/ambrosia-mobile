using Backend.Models;
using Backend.Services;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using Zenject;

namespace Metagame.VehicleAvatar
{
    public class VehicleAvatarPrefabController : MonoBehaviour
    {
        [SerializeField] private Image avatarImg;
        [SerializeField] private SpriteAtlas vehicleAtlas;
        [SerializeField] private Button background;
        [SerializeField] private TMP_Text level;
        [SerializeField] private Image engineImg;
        [SerializeField] private Image frameImg;
        [SerializeField] private Image computerImg;
        [SerializeField] private Image special1Img;
        [SerializeField] private Image special2Img;
        [SerializeField] private Image special3Img;
        [SerializeField] private GameObject avatar;

        [Inject] private VehicleService vehicleService;
        [Inject] private VehicleBaseService vehicleBaseService;

        public Vehicle Vehicle { get; private set; }

        public void SetVehicle(Vehicle vehicle)
        {
            Vehicle = vehicle;
            if (Vehicle == null)
            {
                avatar.SetActive(false);
                return;
            }
            avatar.SetActive(true);
            var baseVehicle = vehicleBaseService.GetVehicleBase(vehicle.baseVehicleId);
            avatarImg.sprite = vehicleAtlas.GetSprite(vehicle.avatar);
            level.text = Vehicle.level.ToString();
            engineImg.sprite = vehicleAtlas.GetSprite(Vehicle.engine != null ? "ENGINE" : "ENGINE_NONE");
            frameImg.sprite = vehicleAtlas.GetSprite(Vehicle.frame != null ? "FRAME" : "FRAME_NONE");
            computerImg.sprite = vehicleAtlas.GetSprite(Vehicle.computer != null ? "COMPUTER" : "COMPUTER_NONE");
            
            if (baseVehicle.specialPart1 != null)
            {
                var img = baseVehicle.specialPart1.ToString();
                if (vehicle.specialPart1 == null)
                {
                    img += "_NONE";
                }
                special1Img.sprite = vehicleAtlas.GetSprite(img);
            }
            else
            {
                special1Img.sprite = vehicleAtlas.GetSprite("SPECIAL_NONE");
            }
            
            if (baseVehicle.specialPart2 != null)
            {
                var img = baseVehicle.specialPart2.ToString();
                if (vehicle.specialPart2 == null)
                {
                    img += "_NONE";
                }
                special2Img.sprite = vehicleAtlas.GetSprite(img);
            }
            else
            {
                special2Img.sprite = vehicleAtlas.GetSprite("SPECIAL_NONE");
            }
            
            if (baseVehicle.specialPart3 != null)
            {
                var img = baseVehicle.specialPart3.ToString();
                if (vehicle.specialPart3 == null)
                {
                    img += "_NONE";
                }
                special3Img.sprite = vehicleAtlas.GetSprite(img);
            }
            else
            {
                special3Img.sprite = vehicleAtlas.GetSprite("SPECIAL_NONE");
            }
        }

        public void ActivateNextAvailableOnClick()
        {
            if (Vehicle != null)
            {
                background.onClick.AddListener(() =>
                    {
                        var availableVehicles = vehicleService.AvailableVehicles();
                        var next = availableVehicles.Find(v => v.slot > Vehicle.slot) ?? availableVehicles[0];
                        SetVehicle(next);
                    });
            }
        }
    }
}