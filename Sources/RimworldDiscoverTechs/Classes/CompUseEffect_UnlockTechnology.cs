using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace RimworldDiscoverTechs
{
    public class CompUseEffect_UnlockTechnology : CompUseEffect
    {
        private const float ResearchGainAmount = (500f / 0.007f);

        public override void DoEffect(Pawn usedBy)
        {
            base.DoEffect(usedBy);

            // Get Tech Level & appropriate techs
            TechLevel techLevel = parent.GetComp<CompBlueprint>().techLevel;
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
                Messages.Message("The " + techLevel.ToStringHuman() + " technology blueprint revealed everything about " + chosenResearchProject.label.ToString() + ".", MessageTypeDefOf.PositiveEvent); // Adds a message top left
            }
            else
            {
                // Change current project just the time to add research points then restore it
                ResearchProjectDef storedResearchProject = Find.ResearchManager.currentProj;
                Find.ResearchManager.currentProj = chosenResearchProject;

                Find.ResearchManager.ResearchPerformed(ResearchGainAmount, usedBy);

                Find.ResearchManager.currentProj = storedResearchProject;

                //Find.ResearchManager.InstantFinish(chosenResearchProject, false);
                Messages.Message("The " + techLevel.ToStringHuman() + " technology blueprint is complicated, but it helped understand " + chosenResearchProject.label.ToString() + ".", MessageTypeDefOf.PositiveEvent); // Adds a message top left
            }
        }
    }
}
