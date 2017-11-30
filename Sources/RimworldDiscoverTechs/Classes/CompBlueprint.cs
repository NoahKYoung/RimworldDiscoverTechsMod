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

            rand = new Random();
            int randTechLevel = rand.Next(1, 5);

            // Sets this blueprint's tech level
            switch (randTechLevel)
            {
                case 1:
                    this.techLevel = TechLevel.Neolithic;
                    break;

                case 2:
                    this.techLevel = TechLevel.Medieval;
                    break;

                case 3:
                    this.techLevel = TechLevel.Industrial;
                    break;

                case 4:
                    this.techLevel = TechLevel.Spacer;
                    break;

                default:
                    this.techLevel = TechLevel.Neolithic;
                    break;
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
