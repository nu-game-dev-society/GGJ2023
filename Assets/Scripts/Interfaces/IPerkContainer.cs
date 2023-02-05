using System;
using UnityEngine;

public interface IPerkContainer
{
    Type ContainedPerkType { get; }
    Color Color { get; }
}
