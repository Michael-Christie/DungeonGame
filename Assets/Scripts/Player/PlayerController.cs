using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] private Camera playerCamera;

    [SerializeField] private CMF.Mover moveComponent;

    [SerializeField] private CMF.AdvancedWalkerController advancedWalking;

    [SerializeField] private CMF.CharacterKeyboardInput input;

    [SerializeField] private CMF.CameraController cameraController;

    [SerializeField] private Rigidbody rigidBody;

    [SerializeField] private LayerMask interactHitLayer;

    private IInteractable lastHitInteractable;

    [SerializeField] private Transform cameraControl;

    private const float defaultMoveSpeed = 7.0f;

    private int defaultHealth = 100;

    public PlayerClass playerClass { get; private set; }

    public Action<int> onHealthUpdate;

    public Camera PlayerCamera
    {
        get
        {
            return playerCamera;
        }
    }

    private int _currentHealth;
    public int Health
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = value;
            onHealthUpdate?.Invoke(_currentHealth);
        }
    }

    //
    private void Awake()
    {
        Instance = this;
        //DisableCharacter();
        EnableCharacter();
    }

    public void Update()
    {
        //This maybe need to move over to a new input system way to get this more flexible with input
        if (Input.GetKeyDown(KeyCode.F))
        {
            lastHitInteractable?.OnInteract();
        }
    }

    public void FixedUpdate()
    {
        Ray _ray = new Ray(playerCamera.transform.position, cameraControl.forward);
        if(Physics.Raycast(_ray, out RaycastHit _hit, 5.0f, interactHitLayer))
        {
            lastHitInteractable = _hit.collider.GetComponent<IInteractable>();
        }

        Debug.DrawRay(_ray.origin, _ray.direction);
    }

    public void EnableCharacter()
    {
        Debug.Log("Player Enabled");
        playerCamera.gameObject.SetActive(true);
        EnableMovement();
    }

    public void DisableCharacter()
    {
        Debug.Log("Player Disabled");
        playerCamera.gameObject.SetActive(false);
        DisableMovement();
    }

    public void DisableMovement()
    {
        moveComponent.enabled = false;
        rigidBody.isKinematic = true;
        cameraController.isEnabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void EnableMovement()
    {
        moveComponent.enabled = true;
        rigidBody.isKinematic = false;
        cameraController.isEnabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SetPosition(Vector3 _pos)
    {
        transform.position = _pos;
    }

    public void SetPlayerClass(PlayerClass _class)
    {
        playerClass = _class;

        advancedWalking.movementSpeed = defaultMoveSpeed * _class.Speed;
        _currentHealth = defaultHealth = _class.Health;
    }

    public void OnDamageRecieved(int _damageAmount)
    {
        //Reduce/Increase the damage taking by the classes defence
        Health -= Mathf.FloorToInt(_damageAmount * playerClass.Defence);
    }
}
