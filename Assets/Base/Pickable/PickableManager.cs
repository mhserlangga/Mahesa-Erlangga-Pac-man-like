using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickableList : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private ScoreManager _scoreManager;
    [SerializeField]
    private string _winScreenName;
    [SerializeField]
    private AudioSource _pickUpSFX;

    private List<Pickable> _pickableList = new List<Pickable>();
 
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
        _scoreManager.SetMaxScore(_pickableList.Count);
    }

    private void onPickablePicked(Pickable pickable)
    {
        _pickableList.Remove(pickable);
        _scoreManager.AddScore(1);
        if (pickable.pickableType == PickableType.PowerUp)
        {
            _player.PickPowerUp();
        }
        else
        {
            _pickUpSFX.Play();
        }

        if (_pickableList.Count <= 0)
        {
            SceneManager.LoadScene(_winScreenName);
        }
    }
}
