using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResetState : MonoBehaviour
{
    public Transform shell;
    public Transform magnets;
    public Transform shaft;
    public Transform spacer;
    public Transform brushes;
    public Transform contacts;
    public Transform originalParent;

    public TextMeshPro textMesh; // Board's text box
    public string text1 = null; // Optional first text
    public string text2 = null; // Optional second text
    public string text3 = null; // Optional third text


    // Dictionary to store initial positions and rotations
    private Dictionary<Transform, (Vector3 position, Quaternion rotation)> initialStates;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialStates = new Dictionary<Transform, (Vector3, Quaternion)>();

        AddInitialState(shell);
        AddInitialState(magnets);
        AddInitialState(shaft);
        AddInitialState(spacer);
        AddInitialState(brushes);
        AddInitialState(contacts);
    }

    private void AddInitialState(Transform t)
    {
        initialStates[t] = (t.position, t.rotation);
    }

    public void OnButtonPress()
    {
        foreach (var kvp in initialStates)
        {
            kvp.Key.position = kvp.Value.position;
            kvp.Key.rotation = kvp.Value.rotation;
            kvp.Key.SetParent(originalParent);
        }

        textMesh.text = $"{text1}\n{text2} {text3}";
    }
}
