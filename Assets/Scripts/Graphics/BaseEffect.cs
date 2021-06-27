using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEffect : MonoBehaviour
{
    protected Material baseMaterial;

    public virtual void OnCreate() { }
    public abstract void Render(RenderTexture src, RenderTexture dst);
}