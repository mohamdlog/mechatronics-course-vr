using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator; // Reference to the Animator controlling the door

    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Player"))
        {
            doorAnimator.SetTrigger("Open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetTrigger("Close");
        }
    }
}
