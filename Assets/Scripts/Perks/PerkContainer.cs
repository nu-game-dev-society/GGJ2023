using System;
using System.Collections.Generic;
using UnityEngine;

public class PerkContainer : MonoBehaviour, IPerkContainer, IInteractable
{
    private static readonly Dictionary<EPerkType, Type> perkTypeDictionary = new Dictionary<EPerkType, Type>
    {
        { EPerkType.GreenPerk, typeof(GreenPerk) },
        { EPerkType.RedPerk, typeof(RedPerk) },
        { EPerkType.SprintPerk, typeof(SprintPerk) },
        { EPerkType.HealthPerk, typeof(HealthPerk) },
    };

    [field: SerializeField]
    public Color Color { get; private set; }
    [SerializeField]
    private EPerkType containedPerkTypeEnum;
    public Type ContainedPerkType => perkTypeDictionary[this.containedPerkTypeEnum];

    public void Interact(PlayerController interactor)
    {
#if DEBUG
        // can't figure out how to do this bit with generics :(
        if (!typeof(IPerk).IsAssignableFrom(ContainedPerkType))
        {
            throw new InvalidOperationException("THAT'S NOT A FKN PERK M8");
        }
#endif

        IPerk perk = interactor.gameObject.AddComponent(ContainedPerkType) as IPerk;
        perk.Color = Color;
        perk.IsActive = true;

        Destroy(gameObject);
    }

    public bool CanInteract(PlayerController interactor) => true;

    public bool ShouldHighlight() => false;

    public string PopupText() => "Drink " + perkNames[ContainedPerkType];

    private static readonly Dictionary<Type, string> perkNames = new()
    {
        { typeof(GreenPerk), "Photosyntherise" },
        { typeof(RedPerk), "Sunny P" },
        { typeof(SprintPerk), "Speed Growth" },
    };
}