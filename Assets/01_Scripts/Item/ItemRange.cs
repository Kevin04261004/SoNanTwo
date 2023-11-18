using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRange : MonoBehaviour
{
    [SerializeField]
    private Item parent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parent.ReceivedPlayer = other.gameObject;
            parent.ReceiveItem();
        }
    }
}
