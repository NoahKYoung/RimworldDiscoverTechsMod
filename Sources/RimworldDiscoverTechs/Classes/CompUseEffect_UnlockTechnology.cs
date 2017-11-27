using System;
using System.Collections.Generic;
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

            // Get Tech Level & appropriate techs
            TechLevel techLevel = this.parent.GetComp<CompBlueprint>().techLevel;
            List<ResearchProjectDef> techList = DefDatabase<ResearchProjectDef>.AllDefsListForReading.FindAll((ResearchProjectDef x) => ((x.techLevel == techLevel) && (!x.IsFinished)));
            ResearchProjectDef chosenResearchProject = techList.RandomElement<ResearchProjectDef>();

            Find.ResearchManager.InstantFinish(chosenResearchProject, false);

            Messages.Message("The "+techLevel.ToStringHuman()+" technology blueprint revealed the secrets of "+chosenResearchProject.label.ToString()+ ".", MessageTypeDefOf.PositiveEvent); // Adds a message top left
        }
    }
}
