namespace Octopath_Traveler_Model;

public abstract class SkillEffect
{
    public abstract List<SkillImpact> Apply(
        Unit user,
        List<Unit> targets,
        DamageCalculator damageCalculator,
        string? selectedWeapon = null,
        int bpUsed = 0);
}

public class PhysicalDamageEffect : SkillEffect
{
    private readonly double _modifier;
    private const string DamageDescription = "daño físico";

    public PhysicalDamageEffect(double modifier)
    {
        _modifier = modifier;
    }

    public override List<SkillImpact> Apply(
        Unit user,
        List<Unit> targets,
        DamageCalculator damageCalculator,
        string? selectedWeapon = null,
        int bpUsed = 0)
    {
        List<SkillImpact> impacts = new();

        foreach (Unit target in targets)
        {
            int damage = damageCalculator.CalculatePhysicalDamage(user, target, _modifier);
            target.ReceiveDamage(damage);

            impacts.Add(new SkillImpact(
                target.Name,
                damage,
                target.CurrentHp,
                DamageDescription));
        }

        return impacts;
    }
}

public class ElementalDamageEffect : SkillEffect
{
    private readonly double _modifier;
    private const string DamageDescription = "daño elemental";

    public ElementalDamageEffect(double modifier)
    {
        _modifier = modifier;
    }

    public override List<SkillImpact> Apply(
        Unit user,
        List<Unit> targets,
        DamageCalculator damageCalculator,
        string? selectedWeapon = null,
        int bpUsed = 0)
    {
        List<SkillImpact> impacts = new();

        foreach (Unit target in targets)
        {
            int damage = damageCalculator.CalculateElementalDamage(user, target, _modifier);
            target.ReceiveDamage(damage);

            impacts.Add(new SkillImpact(
                target.Name,
                damage,
                target.CurrentHp,
                DamageDescription));
        }

        return impacts;
    }
}

public class HalveCurrentHpEffect : SkillEffect
{
    private const string DamageDescription = "daño";

    public override List<SkillImpact> Apply(
        Unit user,
        List<Unit> targets,
        DamageCalculator damageCalculator,
        string? selectedWeapon = null,
        int bpUsed = 0)
    {
        List<SkillImpact> impacts = new();

        foreach (Unit target in targets)
        {
            int newHp = target.CurrentHp / 2;
            int damage = target.CurrentHp - newHp;
            target.ReceiveDamage(damage);

            impacts.Add(new SkillImpact(
                target.Name,
                damage,
                target.CurrentHp,
                DamageDescription));
        }

        return impacts;
    }
}


public class TypedPhysicalDamageEffect : SkillEffect
{
    private readonly double _modifier;
    private readonly string _attackType;

    public TypedPhysicalDamageEffect(double modifier, string attackType)
    {
        _modifier = modifier;
        _attackType = attackType;
    }

    public override List<SkillImpact> Apply(
        Unit user,
        List<Unit> targets,
        DamageCalculator damageCalculator,
        string? selectedWeapon = null,
        int bpUsed = 0)
    {
        List<SkillImpact> impacts = new();

        foreach (Unit target in targets)
        {
            int damage = damageCalculator.CalculatePhysicalDamage(user, target, _modifier);
            target.ReceiveDamage(damage);

            impacts.Add(new SkillImpact(
                target.Name,
                damage,
                target.CurrentHp,
                $"daño de tipo {_attackType}"));
        }

        return impacts;
    }
}

public class TypedElementalDamageEffect : SkillEffect
{
    private readonly double _modifier;
    private readonly string _attackType;

    public TypedElementalDamageEffect(double modifier, string attackType)
    {
        _modifier = modifier;
        _attackType = attackType;
    }

    public override List<SkillImpact> Apply(
        Unit user,
        List<Unit> targets,
        DamageCalculator damageCalculator,
        string? selectedWeapon = null,
        int bpUsed = 0)
    {
        List<SkillImpact> impacts = new();

        foreach (Unit target in targets)
        {
            int damage = damageCalculator.CalculateElementalDamage(user, target, _modifier);
            target.ReceiveDamage(damage);

            impacts.Add(new SkillImpact(
                target.Name,
                damage,
                target.CurrentHp,
                $"daño de tipo {_attackType}"));
        }

        return impacts;
    }
}

public class MercyStrikeEffect : SkillEffect
{
    private const double Modifier = 1.5;
    private const string AttackType = "Bow";

    public override List<SkillImpact> Apply(
        Unit user,
        List<Unit> targets,
        DamageCalculator damageCalculator,
        string? selectedWeapon = null,
        int bpUsed = 0)
    {
        List<SkillImpact> impacts = new();

        foreach (Unit target in targets)
        {
            int damage = damageCalculator.CalculatePhysicalDamage(user, target, Modifier);

            if (damage >= target.CurrentHp)
            {
                damage = Math.Max(0, target.CurrentHp - 1);
            }

            target.ReceiveDamage(damage);

            impacts.Add(new SkillImpact(
                target.Name,
                damage,
                target.CurrentHp,
                $"daño de tipo {AttackType}"));
        }

        return impacts;
    }
}

public enum DamageType
{
    Fire,
    Ice,
    Lightning,
    Wind,
    Light,
    Dark,
    Sword,
    Axe,
    Bow,
    Spear,
    Dagger,
    Stave
}

public class LastStandEffect : SkillEffect
{
    private const double BaseModifier = 1.4;

    public override List<SkillImpact> Apply(
        Unit user,
        List<Unit> targets,
        DamageCalculator damageCalculator,
        string? selectedWeapon = null,
        int bpUsed = 0)
    {
        var impacts = new List<SkillImpact>();

        int missingHpPercentage = (int)Math.Floor(
            100.0 * (user.Stats.MaxHp - user.CurrentHp) / user.Stats.MaxHp
        );

        double multiplier = 1 + (missingHpPercentage * 0.03);

        foreach (var target in targets)
        {
            double rawDamage = damageCalculator.CalculateRawPhysicalDamage(
                user,
                target,
                BaseModifier
            );

            double finalRawDamage = rawDamage * multiplier;

            int finalDamage = (int)Math.Floor(finalRawDamage);

            target.ReceiveDamage(finalDamage);

            impacts.Add(new SkillImpact(
                target.Name,
                finalDamage,
                target.CurrentHp,
                "daño de tipo Axe"
            ));
        }

        return impacts;
    }
}
public class ShootingStarsEffect : SkillEffect
{
    private const double BaseModifier = 0.9;
        
    public override List<SkillImpact> Apply(
        Unit user,
        List<Unit> targets,
        DamageCalculator damageCalculator,
        string? selectedWeapon = null,
        int bpUsed = 0)
    {
        var impacts = new List<SkillImpact>();
        foreach (var target in targets)
        {
            foreach (var type in new[] { "Wind", "Light", "Dark" })
            {
                int damage = damageCalculator.CalculateElementalDamage(
                    user,
                    target,
                    BaseModifier
                );

                target.ReceiveDamage(damage);
                impacts.Add(new SkillImpact(
                    target.Name,
                    damage,
                    target.CurrentHp,
                    $"daño de tipo {type}"
                ));
            }
        }
        return impacts;
    }
}

public class NightmareChimeraEffect : SkillEffect
{
    private const double BaseModifier = 1.9;

    public override List<SkillImpact> Apply(
        Unit user,
        List<Unit> targets,
        DamageCalculator damageCalculator,
        string? selectedWeapon = null,
        int bpUsed = 0)
    {
        var impacts = new List<SkillImpact>();
        if (targets.Count == 0) return impacts;

        Traveler traveler = (Traveler)user;
        double bpBonus = 1 + 0.9 * bpUsed;

        string weapon = selectedWeapon ??
            (traveler.Weapons.Count > 0 ? traveler.Weapons[0] : "Sword");

        Unit target = targets[0];

        double rawDamage = user.PhysicalAttack * (BaseModifier * bpBonus) - target.PhysicalDefense;
        int damage = (int)Math.Floor(rawDamage);
        damage = Math.Max(0, damage);

        target.ReceiveDamage(damage);

        impacts.Add(new SkillImpact(
            target.Name,
            damage,
            target.CurrentHp,
            $"daño de tipo {weapon}"
        ));

        return impacts;
    }
}