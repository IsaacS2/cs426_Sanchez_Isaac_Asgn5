using UnityEngine;

public class EyeExtrusionControl : MonoBehaviour
{
    public Material eyeMaterial; // Assign this through the Unity Editor

    void Start()
    {
        SetExtrusion(0.05f); // Set an initial extrusion amount
    }

    public void SetExtrusion(float amount)
    {
        if (eyeMaterial != null)
        {
            var Renderer = GetComponent<Renderer>();
            Renderer.material = eyeMaterial;
            // eyeMaterial.SetFloat("_Amount", amount);
        }
    }
}
