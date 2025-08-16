using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField]
    public PickableType pickableType;

    public Action<Pickable> onPicked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Pick: " + pickableType);
            onPicked(this);
            Destroy(gameObject);
        }
    }
}
