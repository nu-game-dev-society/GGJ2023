using System;
using System.Collections.Generic;
using UnityEngine;

public interface IPerkContainer
{
    Type ContainedPerkType { get; }
    Color Color { get; }
}

public class PerkContainer : MonoBehaviour, IPerkContainer
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
}