using UnityEngine;

public class GreenPerk : PerkBase
{
    protected override void InnerUpdate()
    {

    }

    protected override void OnIsActiveChanged(bool oldValue, bool newValue)
    {
        Debug.Log($"{nameof(GreenPerk)} {nameof(OnIsActiveChanged)}!");
    }
}
