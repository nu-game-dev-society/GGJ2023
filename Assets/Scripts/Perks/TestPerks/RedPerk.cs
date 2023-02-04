using UnityEngine;

public class RedPerk : PerkBase
{
    protected override void InnerUpdate()
    {

    }

    protected override void OnIsActiveChanged(bool oldValue, bool newValue)
    {
        Debug.Log($"{nameof(RedPerk)} {nameof(OnIsActiveChanged)}!");
    }
}
