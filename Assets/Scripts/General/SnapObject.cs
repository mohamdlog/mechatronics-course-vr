 using UnityEngine;

public class SnapObject : MonoBehaviour
{
    public Transform snapTarget; // The target's position we want to snap to
    public float snapDistance = 0.2f; // The distance tolerated
    private bool isSnapped = false; // To track if item has been snapped

    // Update is called once per frame
    void Update()
    {
        if (!isSnapped)
        {
            float distance = Vector3.Distance(transform.position, snapTarget.position);
            if (distance < snapDistance)
            {
                transform.position = snapTarget.position;
                isSnapped = true;
                Debug.Log($"{gameObject.name} snapped into place!");
            }
        }
    }
}
