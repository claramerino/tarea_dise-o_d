using System;
using System.Collections.Generic;
using System.Linq;

namespace Octopath_Traveler_Model
{
    // SkillFactory extendida para incluir las nuevas skills
    public class ExtendedSkillFactory : SkillFactory
    {
        public new Skill Create(string name)
        {
            return name switch
            {
                "Heal Wounds" => new HealWounds(),
                "Heal More" => new HealMore(),
                "First Aid" => new FirstAid(),
                "Revive" => new Revive(),
                "Vivify" => new Vivify(),
                "Spearhead" => new Spearhead(),
                "Leghold Trap" => new LegholdTrap(),
                _ => base.Create(name)
            };
        }
    }
}
