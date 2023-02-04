using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshaker : MonoBehaviour
{

    public static Screenshaker Instance;
    /// <summary>
    /// Amount of Shake
    /// </summary>
    public Vector3 Amount = new Vector3(0.3f, 0.3f, 0);

    /// <summary>
    /// Duration of Shake
    /// </summary>
    public float Duration = 0.5f;

    /// <summary>
    /// Shake Speed
    /// </summary>
    public float Speed = 2;

    /// <summary>
    /// Amount over Lifetime [0,1]
    /// </summary>
    public AnimationCurve Curve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    /// <summary>
    /// Set it to true: The camera position is set in reference to the old position of the camera
    /// Set it to false: The camera position is set in absolute values or is fixed to an object
    /// </summary>
    public bool DeltaMovement = true;

    protected Camera[] Camera;
    protected float time = 0;
    protected Vector3 lastPos;
    protected Vector3 nextPos;
    protected float lastFoV;
    protected float nextFoV;
    protected bool destroyAfterPlay;

    /// <summary>
    /// awake
    /// </summary>
    private void Awake()
    {
        Instance = this;
        Camera = GetComponentsInChildren<Camera>();
    }

    /// <summary>
    /// Do the shake
    /// </summary>
    public void ShakeOnce(float duration = 0.5f, float speed = 4f, Vector3? amount = null, bool deltaMovement = true, AnimationCurve curve = null)
    {
        //set data
        Duration = duration;
        Speed = speed;
        if (amount != null)
            Amount = (Vector3)amount;
        if (curve != null)
            Curve = curve;
        DeltaMovement = deltaMovement;

        Shake();
    }

    /// <summary>
    /// Do the shake
    /// </summary>
    public void Shake()
    {
        ResetCam();
        time = Duration;
    }

    private void LateUpdate()
    {
        if (time > 0)
        {
            //do something
            time -= Time.deltaTime;
            if (time > 0)
            {
                //next position based on perlin noise
                nextPos = (Mathf.PerlinNoise(time * Speed, time * Speed * 2) - 0.5f) * Amount.x * transform.right * Curve.Evaluate(1f - time / Duration) +
                          (Mathf.PerlinNoise(time * Speed * 2, time * Speed) - 0.5f) * Amount.y * transform.up * Curve.Evaluate(1f - time / Duration);
                nextFoV = (Mathf.PerlinNoise(time * Speed, time * Speed) - 0.5f) * Amount.z * Curve.Evaluate(1f - time / Duration);

                foreach (var c in Camera)
                    c.fieldOfView += (nextFoV - lastFoV);
                transform.Translate(DeltaMovement ? (nextPos - lastPos) : nextPos);

                lastPos = nextPos;
                lastFoV = nextFoV;
            }
            else
            {
                //last frame
                ResetCam();
                if (destroyAfterPlay)
                    Destroy(this);
            }
        }
    }

    private void ResetCam()
    {
        //reset the last delta
        transform.Translate(DeltaMovement ? -lastPos : Vector3.zero);

        foreach(var c in Camera)
            c.fieldOfView -= lastFoV;

        //clear values
        lastPos = nextPos = Vector3.zero;
        lastFoV = nextFoV = 0f;
    }
}
