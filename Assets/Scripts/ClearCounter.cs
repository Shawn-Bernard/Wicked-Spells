using UnityEngine;

public class ClearCounter : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        Debug.Log("Interact");
    }
}
