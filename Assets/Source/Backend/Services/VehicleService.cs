using System.Collections.Generic;
using System.Linq;
using Backend.Models;
using Backend.Responses;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class VehicleService
    {

        private List<Vehicle> vehicles;
        private List<VehiclePart> parts;
        
        public VehicleService(SignalBus signalBus)
        {
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                Consume(signal.Data);
            });
        }

        public Vehicle Vehicle(long? id)
        {
            if (id == null)
            {
                return null;
            }
            return vehicles.Find(v => v.id == id);
        }

        public Vehicle AvailableVehicle(long? id)
        {
            if (id == null)
            {
                return null;
            }
            var vehicle = vehicles.Find(v => v.id == id);
            if (vehicle != null && vehicle.missionId == null && vehicle.playerExpeditionId == null && !vehicle.upgradeTriggered)
            {
                return vehicle;
            }

            return null;
        }

        public Vehicle AvailableVehicle()
        {
            return vehicles.Find(v =>
                v.slot != null && v.missionId == null && v.playerExpeditionId == null && !v.upgradeTriggered);
        }

        public List<Vehicle> AvailableVehicles()
        {
            return vehicles.Where(v =>
                v.slot != null && v.missionId == null && v.playerExpeditionId == null && !v.upgradeTriggered).OrderBy(v => v.slot).ToList();
        }

        public VehiclePart VehiclePart(long id)
        {
            return parts.Find(p => p.id == id);
        }

        private void Consume(PlayerActionResponse data)
        {
            if (data.vehicles != null)
            {
                if (vehicles == null)
                {
                    vehicles = data.vehicles;
                }
                else
                {
                    foreach (var vehicle in data.vehicles)
                    {
                        Update(vehicle);
                    }
                }
            }
            if (data.vehicleParts != null)
            {
                if (parts == null)
                {
                    parts = data.vehicleParts;
                }
                else
                {
                    foreach (var part in data.vehicleParts)
                    {
                        Update(part);
                    }
                }
            }
        }

        private void Update(Vehicle vehicle)
        {
            var idx = vehicles.FindIndex(v => v.id == vehicle.id);
            if (idx >= 0)
            {
                vehicles[idx] = vehicle;
            }
            else
            {
                vehicles.Add(vehicle);
            }
        }

        private void Update(VehiclePart part)
        {
            var idx = parts.FindIndex(v => v.id == part.id);
            if (idx >= 0)
            {
                parts[idx] = part;
            }
            else
            {
                parts.Add(part);
            }
        }
    }
}