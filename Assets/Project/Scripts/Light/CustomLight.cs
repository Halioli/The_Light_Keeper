using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLight : MonoBehaviour
{
    [SerializeField] protected GameObject lightGameObject;
    protected bool active = false;


    public virtual void Expand() { }
    public virtual void Shrink() { }
    public virtual void SetIntensity(float intensity) { }
    public virtual void SetDistance(float distance) { }
}