using System.Collections.Generic;
using Verse;
using RimWorld;

namespace RimworldDiscoverTechs
{
    public class CompProperties_BlueprintTech : CompProperties
    {
        public List<TechLevel> techLevels;

        public CompProperties_BlueprintTech()
        {
            compClass = typeof(CompBlueprintTech);
        }
    }
}
