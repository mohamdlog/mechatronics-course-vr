using UnityEngine;
using UnityEditor;

public class PlaceObjectOnTopTool : EditorWindow
{
  private GameObject targetObject; // Reference to target object
  private GameObject objectToPlace; // Reference to object to place

  // Add a menu item to open the tool window
  [MenuItem("Tools/Place Object In Middle")]
  public static void ShowWindow()
  {
    GetWindow<PlaceObjectOnTopTool>("Place Object In Middle");
  }

  private void OnGUI()
  {
    GUILayout.Label("Place Object On Top Tool", EditorStyles.boldLabel);

    // Object fields for target and object to place
    targetObject = (GameObject)EditorGUILayout.ObjectField("Target Object", targetObject, typeof(GameObject), true);
    objectToPlace = (GameObject)EditorGUILayout.ObjectField("Object to Place", objectToPlace, typeof(GameObject), true);

    GUILayout.Space(10);

    // Button to execute the placement
    if (GUILayout.Button("Place Object"))
    {
      if (targetObject == null || objectToPlace == null)
      {
        EditorUtility.DisplayDialog("Error", "Please assign both Target Object and Object to Place.", "OK");
        return;
      }

      PlaceObjectOnTop(targetObject, objectToPlace);
    }
  }

  private void PlaceObjectOnTop(GameObject target, GameObject toPlace)
  {
    // Ensure both objects have Colliders
    Collider targetCollider = target.GetComponent<Collider>();
    Collider placeCollider = toPlace.GetComponent<Collider>();

    if (targetCollider == null)
    {
      EditorUtility.DisplayDialog("Error", "Target Object does not have a Collider component.", "OK");
      return;
    }

    if (placeCollider == null)
    {
      EditorUtility.DisplayDialog("Error", "Object to Place does not have a Collider component.", "OK");
      return;
    }

    // Calculate the top position of the target in world space
    Bounds targetBounds = targetCollider.bounds;
    float targetTopY = targetBounds.max.y;

    // Calculate the bounds of the object to place in world space
    Bounds placeBounds = placeCollider.bounds;
    float placeHeight = placeBounds.size.y;

    // Calculate the center position on X and Z axes
    Vector3 targetCenter = targetBounds.center;

    // Calculate the required Y position
    float newY = targetTopY + (placeHeight / 2);

    // Determine the new position
    Vector3 newPosition = new Vector3(
        targetCenter.x,
        newY,
        targetCenter.z
    );

    // Adjust for the pivot point of the object to place
    // If the pivot is not at the center, this ensures proper alignment
    Vector3 pivotOffset = toPlace.transform.position - placeBounds.center;
    newPosition += pivotOffset;

    // If placing a prefab, instantiate it; otherwise, move the existing object
    if (PrefabUtility.IsPartOfPrefabAsset(toPlace))
    {
      GameObject instantiatedObject = PrefabUtility.InstantiatePrefab(toPlace) as GameObject;
      if (instantiatedObject != null)
      {
        Undo.RegisterCreatedObjectUndo(instantiatedObject, "Place Object On Top");
        instantiatedObject.transform.position = newPosition;
        Selection.activeGameObject = instantiatedObject;
      }
      else
      {
        EditorUtility.DisplayDialog("Error", "Failed to instantiate the prefab.", "OK");
      }
    }
    else
    {
      Undo.RecordObject(toPlace.transform, "Move Object On Top");
      toPlace.transform.position = newPosition;
    }
  }
}
