using System;
using System.Collections.Generic;
using UnityEngine;

public interface IPerkContainer
{
    Type ContainedPerkType { get; }
}

public class PerkContainer : MonoBehaviour, IPerkContainer
{
    private static readonly Dictionary<EPerkType, Type> perkTypeDictionary = new Dictionary<EPerkType, Type>
    {
        { EPerkType.GreenPerk, typeof(GreenPerk) },
        { EPerkType.RedPerk, typeof(RedPerk) },
    };

    [field: SerializeField]
    private EPerkType containedPerkTypeEnum;
    public Type ContainedPerkType => perkTypeDictionary[this.containedPerkTypeEnum];
}