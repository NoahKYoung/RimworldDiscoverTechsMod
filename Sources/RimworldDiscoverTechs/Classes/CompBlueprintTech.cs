using Verse;

namespace RimworldDiscoverTechs
{
    public class CompBlueprintTech : ThingComp
    {
        public CompProperties_BlueprintTech Props
        {
            get
            {
                return (CompProperties_BlueprintTech)this.props;
            }
        }
    }
}
