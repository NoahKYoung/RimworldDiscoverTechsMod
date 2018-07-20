using Verse;
using System;
using RimWorld;
using System.Collections.Generic;

namespace RimworldDiscoverTechs
{
    [StaticConstructorOnStartup]
    class RimworldDiscoverTechsClass
    {
        static RimworldDiscoverTechsClass()
        {
            Log.Message("### Technology Blueprints mod loaded. ###");

            // Go through research projects, create lists according to techLevels, then add those projectDefs as prerequisites to each technology blueprint researchDef accordingly.
            // First we create lists
            List<ResearchProjectDef> listOfBPs = new List<ResearchProjectDef>(4);
            List<List<ResearchProjectDef>> listOfResearchLists = new List<List<ResearchProjectDef>>(4);

            List<ResearchProjectDef> researchProjectsNeolithic = DefDatabase<ResearchProjectDef>.AllDefsListForReading.FindAll((ResearchProjectDef x) => x.techLevel == TechLevel.Neolithic);
            listOfResearchLists.Add(researchProjectsNeolithic);
            ResearchProjectDef NeolithicBP = researchProjectsNeolithic.Find((ResearchProjectDef x) => x.defName == "NeolithicBlueprint");
            researchProjectsNeolithic.Remove(NeolithicBP);
            listOfBPs.Add(NeolithicBP);

            List<ResearchProjectDef> researchProjectsMedieval = DefDatabase<ResearchProjectDef>.AllDefsListForReading.FindAll((ResearchProjectDef x) => x.techLevel == TechLevel.Medieval);
            listOfResearchLists.Add(researchProjectsMedieval);
            ResearchProjectDef MedievalBP = researchProjectsMedieval.Find((ResearchProjectDef x) => x.defName == "MedievalBlueprint");
            researchProjectsMedieval.Remove(MedievalBP);
            listOfBPs.Add(MedievalBP);

            List<ResearchProjectDef> researchProjectsIndustrial = DefDatabase<ResearchProjectDef>.AllDefsListForReading.FindAll((ResearchProjectDef x) => x.techLevel == TechLevel.Industrial);
            listOfResearchLists.Add(researchProjectsIndustrial);
            ResearchProjectDef IndustrialBP = researchProjectsIndustrial.Find((ResearchProjectDef x) => x.defName == "IndustrialBlueprint");
            researchProjectsIndustrial.Remove(IndustrialBP);
            listOfBPs.Add(IndustrialBP);

            List<ResearchProjectDef> researchProjectsSpacer = DefDatabase<ResearchProjectDef>.AllDefsListForReading.FindAll((ResearchProjectDef x) => x.techLevel == TechLevel.Spacer);
            listOfResearchLists.Add(researchProjectsSpacer);
            ResearchProjectDef SpacerBP = researchProjectsSpacer.Find((ResearchProjectDef x) => x.defName == "SpacerBlueprint");
            researchProjectsSpacer.Remove(SpacerBP);
            listOfBPs.Add(SpacerBP);

            // Then we update the prereqs
            for (int i = 0; i < listOfBPs.Count; i++)
            {
                Log.Message("Checking " + listOfBPs[i].defName);

                foreach (ResearchProjectDef researchProject in listOfResearchLists[i])
                {
                    listOfBPs[i].prerequisites.Add(researchProject);
                    Log.Message("+ Prerequisite " + researchProject.defName);
                }
            }

            Log.Message("Technology Blueprints: modified prerequisites for Technology Blueprints research projects.");
            Log.Message("### We hope you enjoy Technology Blueprints!###");
        }
    }
}
