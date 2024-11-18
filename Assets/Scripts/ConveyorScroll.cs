using UnityEngine;

public class ConveyorScroll : MonoBehaviour
{
    public Material conveyorMaterial;
    public float scrollSpeed = 2.5f;

    // Update is called once per frame
    void Update()
    {
        if (conveyorMaterial != null)
        {
            Vector2 offset = conveyorMaterial.mainTextureOffset;
            offset.y = (offset.y + scrollSpeed * Time.deltaTime) % 1000.0f;
            conveyorMaterial.mainTextureOffset = offset;
        }
    }
}
