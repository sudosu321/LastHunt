using UnityEngine;

[ExecuteAlways]
public class AutoTileByScale : MonoBehaviour
{
    public Vector2 baseTiling = new Vector2(1, 1);
    Renderer rend;

    void Update()
    {
        if (!rend) rend = GetComponent<Renderer>();
        if (!rend || !rend.sharedMaterial) return;

        Vector3 s = transform.localScale;

        // Adjust tiling based on object scale
        rend.sharedMaterial.mainTextureScale = new Vector2(
            baseTiling.x * s.x,
            baseTiling.y * s.z
        );
    }
}