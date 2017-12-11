using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace RimworldDiscoverTechs
{
    public class CompBlueprint : CompUsable
    {
        public TechLevel techLevel;
        public List<TechLevel> techLevels;
        static Random rand;

        protected override string FloatMenuOptionLabel
        {
            get
            {
                return string.Format(Props.useLabel, techLevel.ToStringHuman()); // This is what is shown on tooltip menu
            }
        }

        [DebuggerHidden]
        public override IEnumerable<FloatMenuOption> CompFloatMenuOptions(Pawn myPawn)
        {
            // Hide if nothing is available; then execute standard functions to check.
            if (!AnyTargetableTechnology())
            {
                yield return new FloatMenuOption("Cannot use " + FloatMenuOptionLabel + " (No available technology)", null, MenuOptionPriority.Default, null, null, 0f, null, null);
            }
            else if (!myPawn.CanReserve(parent, 1, -1, null, false))
            {
                yield return new FloatMenuOption("Cannot use " + FloatMenuOptionLabel + " (" + "Reserved".Translate() + ")", null, MenuOptionPriority.Default, null, null, 0f, null, null);
            }
            else
            {
                FloatMenuOption useopt = new FloatMenuOption("Use " + FloatMenuOptionLabel, delegate
                {
                    if (myPawn.CanReserveAndReach(parent, PathEndMode.Touch, Danger.Deadly, 1, -1, null, false))
                    {
                        foreach (CompUseEffect current in parent.GetComps<CompUseEffect>())
                        {
                            if (current.SelectedUseOption(myPawn))
                            {
                                return;
                            }
                        }
                        TryStartUseJob(myPawn);
                    }
                }, MenuOptionPriority.Default, null, null, 0f, null, null);
                yield return useopt;
            }
        }

        public override void PreAbsorbStack(Thing otherStack, int count)
        {
            base.PreAbsorbStack(otherStack, count);
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look<TechLevel>(ref techLevel, "techLevel");
        }

        public bool AnyTargetableTechnology()
        {
            bool retBool = true;
            List<ResearchProjectDef> techList = DefDatabase<ResearchProjectDef>.AllDefsListForReading.FindAll((ResearchProjectDef x) => ((x.techLevel == techLevel) && (!x.IsFinished) && (x.CanStartNow)));

            if(techList.Count == 0)
            {
                retBool = false;
            }

            return retBool;
        }

        public override void Initialize(CompProperties props)
        {
            base.Initialize(props);

            //Choose a random tech level from blueprints in there
            techLevels = parent.GetComp<CompBlueprintTech>().Props.techLevels;

            if (techLevels != null)
            {
                rand = new Random();
                int index = rand.Next(techLevels.Count());

                techLevel = techLevels.ElementAt(index);
            }
            else
            {
                Log.Error("No tech levels listed for this blueprint!");
            }
        }

        public override string TransformLabel(string label)
        {
            // Item's label
            return label;
        }

        // allow stacking same types of blueprints & disallows else
        public override bool AllowStackWith(Thing other)
        {
            if (!base.AllowStackWith(other))
            {
                return false;
            }

            CompBlueprint compBlueprint = other.TryGetComp<CompBlueprint>();
            return compBlueprint != null && compBlueprint.techLevel == techLevel;
        }

        public override void PostSplitOff(Thing piece)
        {
            base.PostSplitOff(piece);
            CompBlueprint compBlueprint = piece.TryGetComp<CompBlueprint>();

            if(compBlueprint != null)
            {
                compBlueprint.techLevel = techLevel;
            }
        }
    }
}
