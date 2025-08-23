using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableList : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    private List<Pickable> _pickableList = new List<Pickable>();
 
    // Start is called before the first frame update
    void Start()
    {
        InitPickable();
    }

    private void InitPickable()
    {
        Pickable[] pickableObject = GameObject.FindObjectsOfType<Pickable>();
        for (int i = 0; i < pickableObject.Length; i++)
        {
            _pickableList.Add(pickableObject[i]);
            pickableObject[i].onPicked += onPickablePicked;
        }
        Debug.Log("Pickable List = " + _pickableList.Count);
    }

    private void onPickablePicked(Pickable pickable)
    {
        _pickableList.Remove(pickable);

        if (pickable.pickableType == PickableType.PowerUp)
        {
            _player.PickPowerUp();
        }

        if (_pickableList.Count <= 0)
        {
            Debug.Log("WIN");
        }
    }
}
