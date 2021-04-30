
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class AnaglyphEffect : MonoBehaviour
{
    public float strength = 0;
    public float limit = 20f;
    [SerializeField]
    private Shader shader;
    private Material baseMaterial;

    // Find the Anaglyph shader source.
    private void Awake()
    {
        baseMaterial = new Material(shader);

        baseMaterial.SetFloat("_Strength", strength);
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, baseMaterial);
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        strength += h * 0.1f;

        strength = Mathf.Clamp(strength, -limit, limit);

        baseMaterial.SetFloat("_Strength", strength);

    }
}