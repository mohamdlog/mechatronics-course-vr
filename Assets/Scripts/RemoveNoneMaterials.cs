using UnityEngine;
using UnityEditor;

public class RemoveNoneMaterials : Editor
{
    [MenuItem("Tools/Remove None Materials")]
    public static void RemoveNoneMaterialsFromSelection()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            ProcessGameObject(obj);
        }
        Debug.Log("Finished removing 'None' materials.");
    }

    private static void ProcessGameObject(GameObject obj)
    {
        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            // Record current state of the object for undo
            Undo.RecordObject(renderer, "Remove 'None' Materials");

            // Get the materials array
            Material[] materials = renderer.sharedMaterials;

            // Filter out None materials
            Material[] filteredMaterials = System.Array.FindAll(materials, material => material != null);

            // Only return materials if changes are necessary
            if (materials.Length != filteredMaterials.Length)
            {
                renderer.sharedMaterials = filteredMaterials;
                Debug.Log($"Removed 'None' materials from {obj.name}");
            }

        }
        
        foreach (Transform child in obj.transform)
        {
            ProcessGameObject(child.gameObject);
        }
    }
}
