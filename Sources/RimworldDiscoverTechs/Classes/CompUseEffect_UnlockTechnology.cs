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
            List<ResearchProjectDef> techList = DefDatabase<ResearchProjectDef>.AllDefsListForReading.FindAll((ResearchProjectDef x) => ((x.techLevel == techLevel) && (!x.IsFinished) && (x.CanStartNow)));
            ResearchProjectDef chosenResearchProject = techList.RandomElement<ResearchProjectDef>();

            if(chosenResearchProject == null)
            {
                // RESEARCH IS NULL! Do nothing.
                Log.Error("No research has been selected because none fit the conditions.");
                return;
            }

            if (Faction.OfPlayer.def.techLevel >= techLevel)
            {
                Find.ResearchManager.InstantFinish(chosenResearchProject, false);
                Messages.Message("The " + techLevel.ToStringHuman() + " technology blueprint layed out how " + chosenResearchProject.label.ToString() + " works.", MessageTypeDefOf.PositiveEvent); // Adds a message top left
            }
            else
            {
                Find.ResearchManager.InstantFinish(chosenResearchProject, false);
                Messages.Message("The " + techLevel.ToStringHuman() + " technology blueprint has helped delve into the secrets of " + chosenResearchProject.label.ToString() + ".", MessageTypeDefOf.PositiveEvent); // Adds a message top left
            }
        }
    }
}
