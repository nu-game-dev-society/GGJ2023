using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ShaderSpotLightManager : MonoBehaviour
{
    public Color globalPlayerSpotlightColour;
    public float globalPlayerSpotlightIntensity;
    public float globalPlayerSpotlightDistance;


    public void Update()
    {
        Shader.SetGlobalColor("_LightingColor", globalPlayerSpotlightColour);
        Shader.SetGlobalFloat("_LightingDistance", globalPlayerSpotlightDistance);
        Shader.SetGlobalFloat("_LightingIntensity", globalPlayerSpotlightIntensity);
    }
}
