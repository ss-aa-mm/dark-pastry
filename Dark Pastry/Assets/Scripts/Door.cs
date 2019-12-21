using System.Linq;
using UnityEngine;

public class Door : MonoBehaviour
{
    private GameObject _openState;
    private GameObject _closedState;


    private void Awake()
    {
        var attachedComponents = GetComponentsInChildren<Transform>(true).ToList();
        _openState = attachedComponents.Find(obj => obj.CompareTag("OpenDoor")).gameObject;
        _closedState = attachedComponents.Find(obj => obj.CompareTag("ClosedDoor")).gameObject;
    }

    private void Update()
    {
        if (Pentagram.IsUnlocked())
            Open();
        else
        {
            Close();
        }
    }

    private void Open()
    {
        _closedState.SetActive(false);
        _openState.SetActive(true);
    }

    private void Close()
    {
        _closedState.SetActive(true);
        _openState.SetActive(false);
    }
}
