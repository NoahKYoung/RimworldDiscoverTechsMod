using Verse;
using Harmony;
using RimWorld;
using System.Reflection;
using System.Linq;
using System;
using System.Collections.Generic;

namespace RimworldDiscoverTechs
{
    [StaticConstructorOnStartup]
    public static class RimworldDiscoverTechsClass
    {
        static RimworldDiscoverTechsClass()
        {
            //Create our Harmony instance to run the patch
            HarmonyInstance.Create("RimworldDiscoverTechsClass").PatchAll();

            Log.Message("Technology Blueprints :: Loading finished, We hope you enjoy Technology Blueprints!");
        }
    }

    //DefModExtension so the prefix code can distinguish between our special ResearchProjecDefs and normal ones
    // to allow for different treatment
    public class CompleteTechLevelPrereq : DefModExtension
    {
        //Value is the entire TechLevel that is the prerequisite
        public TechLevel techLevel;
    }

    //Here is the patch for ResearchProjectDef.PrerequisitesComplete
    //Runs before the original method as a Prefix
    //Checks the Def for our Mod Extension
    //Our Defs run a custom check for completion of all tech of the TechLevel specified in the Def
    //Sets the result to true or false depending on our check, and ignores original method.
    //If the Def being checked isn't one of ours, we skip our code and continue to the original method.
    [HarmonyPatch(typeof(ResearchProjectDef))]
    [HarmonyPatch("PrerequisitesCompleted", MethodType.Getter)]
    public static class PrerequisitesCompleted_Prefix
    {
        [HarmonyPrefix]
        public static bool Prefix(ref bool __result, ResearchProjectDef __instance)
        {
            if (__instance.HasModExtension<CompleteTechLevelPrereq>())
            {
                //Log.Message("Technology Blueprints :: PrerequisitesCompleted HasModExtension " + __instance);

                List<ResearchProjectDef> unfinishedProjects = DefDatabase<ResearchProjectDef>
                    .AllDefsListForReading
                    .FindAll((ResearchProjectDef x) => x.techLevel == __instance.GetModExtension<CompleteTechLevelPrereq>().techLevel
                        && !x.HasModExtension<CompleteTechLevelPrereq>()
                        && !x.IsFinished);

                

                if (unfinishedProjects.NullOrEmpty())
                {
                    __result = true;
                    return false;
                }
                else
                {
                    //Log.Message("Technology Blueprints :: unfinished techs = " + unfinishedProjects.Join(null, ", "));
                    __result = false;
                    return false;
                }
            }
            return true;
        }
    }

    [HarmonyPatch]
    public static class ResearchNode_Get_Available
    {
        public static MethodBase target;

        public static bool Prepare()
        {
            var mod = LoadedModManager.RunningMods.FirstOrDefault(m => m.Name == "ResearchPal" || m.Name == "Research Tree");
            if (mod == null)
            {
                return false;
            }
            Log.Message("Technology Blueprints :: " + mod.Name + " detected, patching ...");

            Type type = null;

            foreach (Assembly a in mod.assemblies.loadedAssemblies)
            {
                Type typeResult = a.GetTypes().ToList().Find((Type t) => t.Name == "ResearchNode");
                if (typeResult != null)
                {
                    type = typeResult;
                }
            }

            if (type == null)
            {
                Log.Warning("Technology Blueprints :: Can't patch " + mod.Name + " No ResearchNode type");
                return false;
            }

            target = AccessTools.Property(type, "Available").GetGetMethod();
            if (target == null)
            {
                Log.Warning("Technology Blueprints :: Can't patch " + mod.Name + " No ResearchNode.Available Property");
                return false;
            }

            return true;
        }

        public static MethodBase TargetMethod()
        {
            return target;
        }

        public static bool Prefix(ref bool __result, object __instance)
        {
            ResearchProjectDef research = AccessTools.Field(__instance.GetType(), "Research").GetValue(__instance) as ResearchProjectDef;

            bool hasExtension = research.HasModExtension<CompleteTechLevelPrereq>();
            if (hasExtension)
            {
                //Log.Message("Technology Blueprints Rsearch Pal/Tree patch :: " + research + ", divert to PrerequisitesCompleted patch");
                __result = research.PrerequisitesCompleted;
                return false;
            }
            return true;
        }
    }
}