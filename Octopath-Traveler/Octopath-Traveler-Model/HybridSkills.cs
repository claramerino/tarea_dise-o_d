using System;
using System.Collections.Generic;
using System.Linq;

namespace Octopath_Traveler_Model
{
    // Revive: Revive a todos los aliados caídos con 1 HP. Además, cura a todos los aliados con un modificador de 1.4 por cada BP.
    public class Revive : HybridSkill
    {
        private const int SpCostValue = 50;
        private const double BaseModifier = 1.4;

        public Revive() : base("Revive", SpCostValue) { }

        protected override List<SkillImpact> ApplyEffects(Traveler user, GameState gameState, Unit? selectedTarget)
        {
            var impacts = new List<SkillImpact>();
            var party = gameState.PlayerTeam.Travelers.Cast<Unit>().ToList();
            foreach (var ally in party)
            {
                if (ally.IsDead)
                {
                    ally.Revive(1);
                    impacts.Add(new SkillImpact(ally.Name, 1, ally.CurrentHp, "revivir"));
                }
            }
            // Curación a todos los aliados vivos (incluidos los recién revividos)
            foreach (var ally in party)
            {
                if (!ally.IsDead)
                {
                    int healing = (int)Math.Floor(user.Stats.ElementalAttack * BaseModifier);
                    ally.Heal(healing);
                    impacts.Add(new SkillImpact(ally.Name, healing, ally.CurrentHp, "curación"));
                }
            }
            return impacts;
        }
    }

    // Vivify: Revive a un aliado caído y le cura una cantidad de HP. Modificador base 1.5, +50% por BP.
    public class Vivify : HybridSkill
    {
        private const int SpCostValue = 16;
        private const double BaseModifier = 1.5;
        private const double BpScaling = 0.5;

        public Vivify() : base("Vivify", SpCostValue) { }

        protected override List<SkillImpact> ApplyEffects(Traveler user, GameState gameState, Unit? selectedTarget)
        {
            var impacts = new List<SkillImpact>();
            if (selectedTarget != null && selectedTarget.IsDead)
            {
                selectedTarget.Revive(1);
                impacts.Add(new SkillImpact(selectedTarget.Name, 1, selectedTarget.CurrentHp, "revivir"));
                int healing = (int)Math.Floor(user.Stats.ElementalAttack * (BaseModifier + BpScaling * user.CurrentBp));
                selectedTarget.Heal(healing);
                impacts.Add(new SkillImpact(selectedTarget.Name, healing, selectedTarget.CurrentHp, "curación"));
            }
            return impacts;
        }
    }
}
