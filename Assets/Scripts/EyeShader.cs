using UnityEngine;

public class EyeShader : MonoBehaviour
{
    public Material eyeMaterial; // Drag your eye material here through the Inspector

    // Example method to set shader property
    public void SetEyeShade(float amount)
    {
        if (eyeMaterial != null)
        {
            eyeMaterial.SetFloat("_Amount", amount);
        }
    }
}
