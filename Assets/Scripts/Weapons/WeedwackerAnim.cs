using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeedwackerAnim : MonoBehaviour
{
    [SerializeField]
    private Vector3 attackPos;
    [SerializeField]
    private Transform weaponModel, blade;

    [SerializeField] AudioSource asIdle, asActive, asHitting;
    public float AttackSpinSpeed = 360f;
    public float IdleSpinSpeed = 90f;
    float spinSpeed = 90f;
    float engineVolume = 0.0f;
    public void Animate(bool attacking, float range, bool hitting = false)
    {
        weaponModel.transform.localPosition = Vector3.Lerp(weaponModel.transform.localPosition, attacking ? (range * attackPos) : Vector3.zero, Time.deltaTime * (attacking ? 6.0f : 4.0f));
        spinSpeed = Mathf.Lerp(spinSpeed, attacking ? AttackSpinSpeed : IdleSpinSpeed, Time.deltaTime * (attacking ? 4.0f : 2.0f));
        blade.Rotate(spinSpeed * Time.deltaTime * Vector3.up);

        engineVolume = Mathf.Lerp(engineVolume, attacking ? 1.0f : 0.0f, Time.deltaTime * (attacking ? 4.0f : 2.0f));
        asIdle.volume = (1.0f - engineVolume) * 0.5f;
        asActive.volume = engineVolume;
        asHitting.volume = hitting ? 1.0f : 0.0f;
    }
}
