using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony;
using RimWorld;
using Verse;

namespace RimworldDiscoverTechs
{
    [StaticConstructorOnStartup]
    static class HarmonyPatching
    {
        static HarmonyPatching()
        {
            HarmonyInstance harmony = HarmonyInstance.Create("rimworld.lolkatkomrad94.mod.discovertechs");
        }
    }
}
