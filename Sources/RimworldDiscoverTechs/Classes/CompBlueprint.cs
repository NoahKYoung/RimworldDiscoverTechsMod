using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace RimworldDiscoverTechs
{
    public class CompBlueprint: CompUsable
    {
        public TechLevel techLevel;
        public List<TechLevel> techLevels;
        static Random rand;

        protected override string FloatMenuOptionLabel
        {
            get
            {
                return string.Format(base.Props.useLabel); // This is what is shown on tooltip menu
            }
        }

        [DebuggerHidden]
        public override IEnumerable<FloatMenuOption> CompFloatMenuOptions(Pawn myPawn)
        {
            // Hide if nothing is available!
            IEnumerable<FloatMenuOption> returnIEnumerables = base.CompFloatMenuOptions(myPawn);

            if(!AnyTargetableTechnology())
            {
                returnIEnumerables.Concat<FloatMenuOption>(new FloatMenuOption(FloatMenuOptionLabel + " (No available technology)", null, MenuOptionPriority.Default, null, null, 0f, null, null));
            }

            return returnIEnumerables;
        }

        public bool AnyTargetableTechnology()
        {
            bool retBool = false;
            List<ResearchProjectDef> techList = DefDatabase<ResearchProjectDef>.AllDefsListForReading.FindAll((ResearchProjectDef x) => ((x.techLevel == techLevel) && (!x.IsFinished) && (x.CanStartNow)));

            if(techList.Count == 0)
            {
                retBool = true;
            }

            return retBool;
        }

        public override void Initialize(CompProperties props)
        {
            base.Initialize(props);

            //Choose a random tech level from blueprints in there
            techLevels = this.parent.GetComp<CompBlueprintTech>().Props.techLevels;

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
            // Item's name with tech level
            return string.Format(techLevel.ToStringHuman()+" "+label);
        }

        // allow stacking same types of blueprints TODO
        public override bool AllowStackWith(Thing other)
        {
            if (!base.AllowStackWith(other))
            {
                return false;
            }
            return false;
        }

        public override void PostSplitOff(Thing piece)
        {
            base.PostSplitOff(piece);
            CompBlueprint compBlueprint = piece.TryGetComp<CompBlueprint>();
        }
    }
}
