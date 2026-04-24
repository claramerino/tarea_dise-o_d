using System;
using System.Collections.Generic;
using System.Linq;

namespace Octopath_Traveler_Model
{
    // Spearhead: Daño tipo Spear a un enemigo y posiciona al usuario primero en la cola de turnos de la siguiente ronda. Modificador base 1.5, +100% por BP.
    public class Spearhead : OffensiveSkill
    {
        private const int SpCostValue = 6;
        private const double BaseModifier = 1.5;
        private const double BpScaling = 1.0;

        public Spearhead() : base("Spearhead", SpCostValue) { }

        public override bool RequiresManualTarget() => true;

        public override List<Unit> GetTargets(Traveler user, GameState gameState, Unit? selectedTarget)
        {
            return selectedTarget == null ? new List<Unit>() : new List<Unit> { selectedTarget };
        }

        protected override SkillEffect GetEffect()
        {
            // El TurnManager debe ser modificado en el controlador después de aplicar el daño
            // No se puede acceder a user aquí, así que el cálculo debe hacerse en otro lado si es necesario
            return new TypedPhysicalDamageEffect(BaseModifier, "Spear");
        }
    }

    // Leghold Trap: Hace que un enemigo actúe al final de la cola de turnos por 2 rondas (+2 por BP)
    public class LegholdTrap : TurnOrderSkill
    {
        private const int SpCostValue = 6;
        private const int BaseDuration = 2;
        private const int BpScaling = 2;

        public LegholdTrap() : base("Leghold Trap", SpCostValue) { }

        protected override void ModifyTurnOrder(Traveler user, GameState gameState, Unit? target)
        {
            // La manipulación de turnos debe hacerse en el controlador, aquí solo se puede devolver la intención
            // Por ejemplo, podrías guardar la duración en un campo si se requiere
        }
    }
}
