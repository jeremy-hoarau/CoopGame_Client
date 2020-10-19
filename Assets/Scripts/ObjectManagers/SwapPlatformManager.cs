using UnityEngine;

public class SwapPlatformManager : MonoBehaviour
{
    public int swapPlatformId = 1;
    public bool activated = false;
    public Material swapMaterial;

    private Material defaultMaterial;
    private new Renderer renderer;

    private void Start()
    {
        GameManager.swapPlatforms.Add(swapPlatformId, this);

        renderer = GetComponent<Renderer>();
        defaultMaterial = renderer.material;
        
        if (activated)
        {
            renderer.material = swapMaterial;
        }
    }

    public void Activate()
    {
        renderer.material = swapMaterial;
        activated = true;
    }

    public void Deactivate()
    {
        renderer.material = defaultMaterial;
        activated = false;
    }
}
