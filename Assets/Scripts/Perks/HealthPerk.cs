using UnityEngine;

public class HealthPerk : PerkBase
{
    protected override void InnerUpdate()
    {

    }

    protected override void OnIsActiveChanged(bool oldValue, bool newValue)
    {
        this.PlayerController.maxHealth = 200;
    }
}