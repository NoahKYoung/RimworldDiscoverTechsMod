using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace RimworldDiscoverTechs
{
    public class CompUseEffect_UnlockTechnology : CompUseEffect
    {
        private float ResearchGainAmount;
        private float researchPercentage = 0f;
        private String endMessage = "";

        public override void DoEffect(Pawn usedBy)
        {
            base.DoEffect(usedBy);

            // Get Tech Level & appropriate techs
            TechLevel techLevel = parent.GetComp<CompBlueprint>().techLevel;
            List<ResearchProjectDef> techList = DefDatabase<ResearchProjectDef>.AllDefsListForReading.FindAll((ResearchProjectDef x) => ((x.techLevel == techLevel) && (!x.IsFinished) && (x.CanStartNow)));
            ResearchProjectDef chosenResearchProject = techList.RandomElement<ResearchProjectDef>(); // TODO: weigh the selection

            if(chosenResearchProject == null)
            {
                // RESEARCH IS NULL! Do nothing.
                Log.Error("No research has been selected because none fit the conditions.");
                return;
            }

            if (Faction.OfPlayer.def.techLevel >= techLevel)
            {
                researchPercentage = 1f; //100% of base cost
                endMessage = "The " + techLevel.ToStringHuman() + " technology blueprint revealed everything about " + chosenResearchProject.label.ToString() + ".";
            }
            else
            {
                researchPercentage = 0.34f; //34% of base cost
                endMessage = "The " + techLevel.ToStringHuman() + " technology blueprint is complicated, but it helped understand " + chosenResearchProject.label.ToString() + ".";
            }

            // Change current project just the time to add research points then restore it
            ResearchProjectDef storedResearchProject = Find.ResearchManager.currentProj;
            Find.ResearchManager.currentProj = chosenResearchProject;

            ResearchGainAmount = chosenResearchProject.CostApparent * researchPercentage / 0.007f / chosenResearchProject.CostFactor(usedBy.Faction.def.techLevel);

            Find.ResearchManager.ResearchPerformed(ResearchGainAmount, null);

            Find.ResearchManager.currentProj = storedResearchProject;

            // Feedback top left
            Messages.Message(endMessage, MessageTypeDefOf.PositiveEvent); // Adds a message top left
        }
    }
}
