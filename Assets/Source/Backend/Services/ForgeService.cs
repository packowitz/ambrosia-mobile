using System;
using System.Collections.Generic;
using Backend.Models;
using Backend.Models.Enums;
using Backend.Requests;
using Backend.Responses;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class ForgeService
    {
        [Inject] private ServerAPI serverAPI;
        [Inject] private ProgressService progressService;
        public AutoBreakdownConfiguration AutoBreakdownConfiguration { get; private set; }

        public ForgeService(SignalBus signalBus)
        {
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                if (signal.Data.autoBreakdownConfiguration != null)
                {
                    AutoBreakdownConfiguration = signal.Data.autoBreakdownConfiguration;
                }
            });
        }
        
        public void Breakdown(List<long> gearIds, Action<PlayerActionResponse> onSuccess = null)
        {
            serverAPI.DoPost("/forge/breakdown", new Breakdown {gearIds = gearIds, silent = true}, onSuccess);
        }

        public bool IsAutoBreakdown(Gear gear)
        {
            if (progressService.Progress.autoBreakDownEnabled) {
                var conf = AutoBreakdownConfiguration;
                switch (gear.rarity) {
                    case Rarity.SIMPLE:
                        return GetNumberOfJewels(gear) < conf.simpleMinJewelSlots || GearQualityToNumber(gear.gearQuality) < conf.simpleMinQuality;
                    case Rarity.COMMON:
                        return GetNumberOfJewels(gear) < conf.commonMinJewelSlots || GearQualityToNumber(gear.gearQuality) < conf.commonMinQuality;
                    case Rarity.UNCOMMON:
                        return GetNumberOfJewels(gear) < conf.uncommonMinJewelSlots || GearQualityToNumber(gear.gearQuality) < conf.uncommonMinQuality;
                    case Rarity.RARE:
                        return GetNumberOfJewels(gear) < conf.rareMinJewelSlots || GearQualityToNumber(gear.gearQuality) < conf.rareMinQuality;
                    case Rarity.EPIC:
                        return GetNumberOfJewels(gear) < conf.epicMinJewelSlots || GearQualityToNumber(gear.gearQuality) < conf.epicMinQuality;
                    case Rarity.LEGENDARY:
                        return GetNumberOfJewels(gear) < conf.legendaryMinJewelSlots || GearQualityToNumber(gear.gearQuality) < conf.legendaryMinQuality;
                }
            }

            return false;
        }
        
        private int GetNumberOfJewels(Gear gear) {
            if (gear.jewelSlot4 != null) { return 4; }
            if (gear.jewelSlot3 != null) { return 3; }
            if (gear.jewelSlot2 != null) { return 2; }
            if (gear.jewelSlot1 != null) { return 1; }
            return 0;
        }
        
        private int GearQualityToNumber(GearQuality quality) {
            switch (quality) {
                case GearQuality.SHABBY: return 1;
                case GearQuality.RUSTY: return 2;
                case GearQuality.ORDINARY: return 3;
                case GearQuality.USEFUL: return 4;
                case GearQuality.GOOD: return 5;
                case GearQuality.AWESOME: return 6;
                case GearQuality.FLAWLESS: return 7;
                case GearQuality.PERFECT: return 8;
                case GearQuality.GODLIKE: return 9;
            }
            return 0;
        }
    }
}