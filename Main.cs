using MelonLoader;

using ModThatIsNotMod;
using ModThatIsNotMod.BoneMenu;

using PuppetMasta;

using StressLevelZero.AI;

using UnityEngine;

using System.Collections.Generic;
using System.Linq;

namespace NEP.NoDeathAnimations
{
    public static class BuildInfo
    {
        public const string Name = "No Death Animations"; // Name of the Mod.  (MUST BE SET)
        public const string Description = "Removes death animations from NPCs you select."; // Description for the Mod.  (Set as null if none)
        public const string Author = "Not Enough Photons"; // Author of the Mod.  (MUST BE SET)
        public const string Company = "Not Enough Photons"; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }

    public class NDAMod : MelonMod
    {
        public static NDAMod instance;

        public Dictionary<TriggerRefProxy.NpcType, bool> enabledDictionary;
        public float muscleWeight { get; set; } = 0f;
        public bool enableAnimations { get; set; } = false;


        public override void OnApplicationStart()
        {
            if(instance == null)
            {
                instance = this;
            }

            enabledDictionary = new Dictionary<TriggerRefProxy.NpcType, bool>();

            foreach(TriggerRefProxy.NpcType npc in System.Enum.GetValues(typeof(TriggerRefProxy.NpcType)))
            {
                enabledDictionary.Add(npc, false);
            }

            InitializeSettings();
        }

        private void InitializeSettings()
        {
            MenuCategory ndaMain = MenuManager.CreateCategory("No Death Animations", Color.red);

            ndaMain.CreateBoolElement("Enable animations", Color.white, enableAnimations, (enabled) => enableAnimations = enabled);
            ndaMain.CreateFloatElement("Muscle weight", Color.white, muscleWeight, (value) => muscleWeight = value, 0.05f, 0f, 2f, false);
            MenuCategory affectedNPCsCat = ndaMain.CreateSubCategory("Affected NPCs", Color.white);

            for(int i = 0; i < enabledDictionary.Count; i++)
            {
                TriggerRefProxy.NpcType currentNPC = enabledDictionary.ElementAt(i).Key;
                bool currentEnabled = enabledDictionary.ElementAt(i).Value;

                affectedNPCsCat.CreateBoolElement(PairNPCName(currentNPC), Color.white, currentEnabled, (enabled) => enabledDictionary[currentNPC] = enabled);
            }
        }

        private string PairNPCName(TriggerRefProxy.NpcType npc)
        {
            switch (npc)
            {
                case TriggerRefProxy.NpcType.FordHair: return "Ford";
                case TriggerRefProxy.NpcType.FordShortHair: return "Ford VR Junkie";
                case TriggerRefProxy.NpcType.Crablet: return "Crablet";
                case TriggerRefProxy.NpcType.EarlyExit: return "Early Exit";
                case TriggerRefProxy.NpcType.Fordlet: return "Fordlet";
                case TriggerRefProxy.NpcType.NullBody: return "Null Body";
                case TriggerRefProxy.NpcType.NullRat: return "Nullrat";
                case TriggerRefProxy.NpcType.Voidman: return "Void Watcher";
                case TriggerRefProxy.NpcType.OmniWrecker: return "Omniwrecker";
                case TriggerRefProxy.NpcType.OmniProjector: return "Omni Projector";
                case TriggerRefProxy.NpcType.OmniTurret: return "Omni Turret";
                case TriggerRefProxy.NpcType.Turret: return "Turret";
            }

            return string.Empty;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(BehaviourBaseNav))]
    [HarmonyLib.HarmonyPatch(nameof(BehaviourBaseNav.KillStart))]
    public static class PuppetMasterKillStartPatch
    {
        public static void Postfix(BehaviourBaseNav __instance)
        {
            if (NDAMod.instance.enableAnimations) { return; }

            TriggerRefProxy npcProxy = __instance.puppetMaster.GetComponentInChildren<TriggerRefProxy>();
            TriggerRefProxy.NpcType npcType = npcProxy.npcType;

            for (int i = 0; i < NDAMod.instance.enabledDictionary.Count; i++)
            {
                TriggerRefProxy.NpcType currentNPC = NDAMod.instance.enabledDictionary.ElementAt(i).Key;
                bool currentEnabled = NDAMod.instance.enabledDictionary.ElementAt(i).Value;

                if(npcType.HasFlag(currentNPC))
                {
                    if (currentEnabled)
                    {
                        __instance.puppetMaster.muscleWeight = NDAMod.instance.muscleWeight;
                    }
                }
            }
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(BehaviourBase))]
    [HarmonyLib.HarmonyPatch(nameof(BehaviourBase.OnDisable))]
    public static class PuppetMasterKillEndPatch
    {
        public static void Postfix(BehaviourBase __instance)
        {
            __instance.puppetMaster.muscleWeight = 1f;
        }
    }
}