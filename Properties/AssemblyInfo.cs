using System.Reflection;
using MelonLoader;

[assembly: AssemblyTitle(NEP.NoDeathAnimations.BuildInfo.Name)]
[assembly: AssemblyDescription(NEP.NoDeathAnimations.BuildInfo.Description)]
[assembly: AssemblyCompany(NEP.NoDeathAnimations.BuildInfo.Company)]
[assembly: AssemblyProduct(NEP.NoDeathAnimations.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + NEP.NoDeathAnimations.BuildInfo.Author)]
[assembly: AssemblyTrademark(NEP.NoDeathAnimations.BuildInfo.Company)]
[assembly: AssemblyVersion(NEP.NoDeathAnimations.BuildInfo.Version)]
[assembly: AssemblyFileVersion(NEP.NoDeathAnimations.BuildInfo.Version)]
[assembly: MelonInfo(typeof(NEP.NoDeathAnimations.NDAMod), NEP.NoDeathAnimations.BuildInfo.Name, NEP.NoDeathAnimations.BuildInfo.Version, NEP.NoDeathAnimations.BuildInfo.Author, NEP.NoDeathAnimations.BuildInfo.DownloadLink)]
[assembly: MelonColor()]

// Create and Setup a MelonGame Attribute to mark a Melon as Universal or Compatible with specific Games.
// If no MelonGame Attribute is found or any of the Values for any MelonGame Attribute on the Melon is null or empty it will be assumed the Melon is Universal.
// Values for MelonGame Attribute can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame("BONEWORKS", "Stress Level Zero")]