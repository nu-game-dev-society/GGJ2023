using UnityEngine;

public class SprintPerk : PerkBase
{
    protected override void InnerUpdate()
    {

    }

    protected override void OnIsActiveChanged(bool oldValue, bool newValue)
    {
        this.PlayerController.sprintMultiplier = 1.5f;
    }
}
