using System;
using UnityEngine;

public class PerkManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IPerkContainer>() is not IPerkContainer perkContainer)
        {
            return;
        }

        this.AssignPerkToPlayer(perkContainer.ContainedPerkType, perkContainer.Color);
        Destroy(other.gameObject);
    }

    private void AssignPerkToPlayer(Type perkType, Color color)
    {
#if DEBUG
        // can't figure out how to do this bit with generics :(
        if (!typeof(IPerk).IsAssignableFrom(perkType))
        {
            throw new InvalidOperationException("THAT'S NOT A FKN PERK M8");
        }
#endif

        IPerk perk = this.gameObject.AddComponent(perkType) as IPerk;
        perk.Color = color;
        perk.IsActive = true;
    }
}
