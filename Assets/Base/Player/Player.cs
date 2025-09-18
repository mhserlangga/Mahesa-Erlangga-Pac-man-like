using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    private int _health = 3;
    [SerializeField]
    private TMP_Text _healthText;
    [SerializeField]
    private Transform _respawnPoint;
    [SerializeField]
    private float _rotationTime = 0.1f;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private string _loseScreenName;
    [SerializeField]
    private AudioSource _powerUpSFX;
    [SerializeField]
    private AudioSource _powerDownSFX;
    [SerializeField]
    private AudioSource _deathSFX;

    private CharacterController _controller;
    private Coroutine _powerUpCoroutine;
    private bool _isPoweredUp;
    private float _rotationVelocity;
    private Vector3 _playerVelocity;
    private bool _isGrounded;
    public float gravityValue = -9.81f;
    private bool _isInvincible = false;


    public void Dead()
    {
        _health -= 1;
        UpdateUI();
        _deathSFX.Play();

        if (_health > 0)
        {
            _controller.enabled = false;
            transform.position = _respawnPoint.position;
            _controller.enabled = true;

            StartCoroutine(InvincibilityCoroutine());
        }
        else
        {
            _health = 0;
            SceneManager.LoadScene(_loseScreenName);
        }
    }

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
        _isPoweredUp = true;
        _powerUpSFX.Play();
        if (onPowerUpStart != null)
        {  
            onPowerUpStart(); 
        }

        yield return new WaitForSeconds(_powerUpDuration);
        _isPoweredUp = false;
        _powerDownSFX.Play();

        if (onPowerUpStop != null)
        {
            onPowerUpStop();
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        Debug.Log("Player kebal!");
        _isInvincible = true;

        yield return new WaitForSeconds(1.5f);

        _isInvincible = false;
        Debug.Log("Player tidak lagi kebal.");
    }

    private void HideAndLockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Awake()
    {
        UpdateUI();
        _controller = GetComponent<CharacterController>();
        HideAndLockCursor();
    }

    void Update()
    {
        _isGrounded = _controller.isGrounded;
        if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2f;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (movementDirection.magnitude >= 0.1f)
        {
            float rotationAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref _rotationVelocity, _rotationTime);
            transform.rotation = Quaternion.Euler(0, smoothAngle, 0f);
            movementDirection = Quaternion.Euler(0, rotationAngle, 0f) * Vector3.forward;
        }

        _controller.Move(movementDirection.normalized * _speed * Time.deltaTime);

        _playerVelocity.y += gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);

        //float horizontalVelocity = new Vector3(_controller.velocity.x, 0, _controller.velocity.z).magnitude;
        _animator.SetFloat("Velocity", movementDirection.magnitude);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (_isInvincible) return;

        if (hit.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = hit.gameObject.GetComponent<Enemy>();
            if (_isPoweredUp)
            {
                enemy.Dead();
            }
            else
            {
                if (enemy._currentState != enemy.RetreatState)
                {
                    this.Dead();
                }
            }
        }
    }

    private void UpdateUI()
    {
        _healthText.text = _health.ToString();
    }
}
