using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action onPowerUpStart;
    public Action onPowerUpStop;

    [SerializeField]
    private float _speed;
    
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private float _powerUpDuration;

    private Rigidbody _rigidbody;
    private Coroutine _powerUpCoroutine;

    public void PickPowerUp()
    {
        if (_powerUpCoroutine != null)
        {
            StopCoroutine(_powerUpCoroutine);
        }

        _powerUpCoroutine = StartCoroutine(StartPowerUp());
    }

    private IEnumerator StartPowerUp()
    {
        if (onPowerUpStart != null)
        {  
            onPowerUpStart(); 
        }

        yield return new WaitForSeconds(_powerUpDuration);

        if (onPowerUpStop != null)
        {
            onPowerUpStop();
        }
    }

    private void HideAndLockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        HideAndLockCursor();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 horizontalDirection = horizontal * _camera.transform.right;
        Vector3 verticalDirection = vertical * _camera.transform.forward;
        horizontalDirection.y = 0;
        verticalDirection.y = 0;

        Vector3 movementDirection = horizontalDirection + verticalDirection;

        _rigidbody.AddForce(movementDirection * _speed * Time.deltaTime);
    }
}
