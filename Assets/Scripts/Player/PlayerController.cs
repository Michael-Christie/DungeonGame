using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] private Camera playerCamera;

    [SerializeField] private CMF.Mover moveComponent;

    [SerializeField] private CMF.AdvancedWalkerController advancedWalking;

    [SerializeField] private CMF.CharacterKeyboardInput input;

    [SerializeField] private Rigidbody rigidBody;

    [SerializeField] private LayerMask interactHitLayer;

    private IInteractable lastHitInteractable;

    [SerializeField] private Transform cameraControl;

    public Camera PlayerCamera
    {
        get
        {
            return playerCamera;
        }
    }

    //
    private void Awake()
    {
        Instance = this;
        EnableCharacter();
    }

    public void Update()
    {
        //This maybe need to move over to a new input system way to get this more flexible with input
        if (Input.GetMouseButtonDown(0))
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
        playerCamera.gameObject.SetActive(true);
        EnableMovement();
    }

    public void DisableCharacter()
    {
        playerCamera.gameObject.SetActive(false);
        DisableMovement();
    }

    public void DisableMovement()
    {
        moveComponent.enabled = false;
        rigidBody.isKinematic = true;
    }

    public void EnableMovement()
    {
        moveComponent.enabled = false;
        rigidBody.isKinematic = false;
    }
}