using System;
using RimWorld;
using Verse;

namespace RimworldDiscoverTechs
{
    public class CompUseEffect_UnlockTechnology : CompUseEffect
    {
        private const float ResearchGainAmount = 50000f;

        public override void DoEffect(Pawn usedBy)
        {
            base.DoEffect(usedBy);

            Messages.Message("You have used the blueprint correctly.", MessageTypeDefOf.PositiveEvent); // Adds a message top left
        }
    }
}
