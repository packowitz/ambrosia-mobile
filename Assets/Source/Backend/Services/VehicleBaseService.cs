using System;
using Backend.Models;
using UnityEngine;

namespace Backend.Services
{
    public class VehicleBaseService
    {
        private readonly ServerAPI serverAPI;

        private VehicleBase[] baseVehicles;

        public bool VehiclesInitialized => baseVehicles != null && baseVehicles.Length > 0;

        public VehicleBaseService(ServerAPI serverAPI)
        {
            this.serverAPI = serverAPI;
        }

        public void LoadBaseVehicles()
        {
            serverAPI.DoGet<VehicleBase[]>("/vehicle", data =>
            {
                baseVehicles = data;
                Debug.Log($"Loaded {baseVehicles.Length} base vehicles");
            });
        }

        public VehicleBase GetVehicleBase(long id)
        {
            return Array.Find(baseVehicles, vehicle => vehicle.id == id);
        }
    }
}