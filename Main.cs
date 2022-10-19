using MelonLoader;

namespace NEP.NoDeathAnimations
{
    public static class BuildInfo
    {
        public const string Name = "No Death Animations"; // Name of the Mod.  (MUST BE SET)
        public const string Description = "Removes death animations from NPCs."; // Description for the Mod.  (Set as null if none)
        public const string Author = "Not Enough Photons"; // Author of the Mod.  (MUST BE SET)
        public const string Company = "Not Enough Photons"; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }

    public class NDAMod : MelonMod
    {
    }

    [HarmonyLib.HarmonyPatch(typeof(PuppetMasta.PuppetMaster))]
    [HarmonyLib.HarmonyPatch(nameof(PuppetMasta.PuppetMaster.Awake))]
    public static class PuppetMasterPatch
    {
        public static void Postfix(PuppetMasta.PuppetMaster __instance)
        {
            PuppetMasta.PuppetMaster.StateSettings settings = new PuppetMasta.PuppetMaster.StateSettings()
            {
                deadMuscleDamper = __instance.stateSettings.deadMuscleDamper,
                deadMuscleWeight = __instance.stateSettings.deadMuscleWeight,
                enableAngularLimitsOnKill = __instance.stateSettings.enableAngularLimitsOnKill,
                enableInternalCollisionsOnKill = __instance.stateSettings.enableInternalCollisionsOnKill,
                killDuration = 0f,
                maxFreezeSqrVelocity = __instance.stateSettings.maxFreezeSqrVelocity
            };

            __instance.stateSettings = settings;
        }
    }
}