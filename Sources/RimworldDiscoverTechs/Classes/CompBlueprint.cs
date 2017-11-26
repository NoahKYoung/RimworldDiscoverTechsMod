using System;
using RimWorld;
using Verse;

namespace RimworldDiscoverTechs
{
    public class CompBlueprint: CompUsable
    {
        //public Tech techLevel;

        protected override string FloatMenuOptionLabel
        {
            get
            {
                return string.Format("Use " + base.Props.useLabel); // This is what is shown on menu
            }
        }

        public override void Initialize(CompProperties props)
        {
            base.Initialize(props);
            // Todo : set a era for research
        }

        public override string TransformLabel(string label)
        {
            return label; // This is the final item name - put actual tech here.
            //return techLevel + " " + label; // This is the final item name - put actual tech here.
        }

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
