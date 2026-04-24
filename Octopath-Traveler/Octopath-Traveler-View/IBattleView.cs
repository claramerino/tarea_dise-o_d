using System.Collections.Generic;

namespace Octopath_Traveler;

public interface IBattleView
{
    void ShowRoundStart(int roundNumber);
    void ShowBattleState(List<string> playerLines, List<string> enemyLines);
    void ShowRoundTurns(List<string> remainingTurns, List<string> nextRoundTurns);

    string AskTravelerAction(string travelerName);
    string AskWeaponOption(List<string> weapons);
    string AskTargetOption(string travelerName, List<string> targetDescriptions);
    string AskBpOption();
    string AskSkillOption(string travelerName, List<string> skills);
    void ShowSkillResult(string userName, string skillName, List<string> impactLines, List<string> hpLines);

    void ShowTravelerAttackResult(string attackerName, string targetName, int damage, string attackType, int targetRemainingHp, bool isWeakness, bool enteredBreakingPoint);
    void ShowBeastSkillResult(string beastName, string skillName, List<string> impactLines, List<string> hpLines);

    void ShowEscapeMessage();
    void ShowWinner(string winner);
}