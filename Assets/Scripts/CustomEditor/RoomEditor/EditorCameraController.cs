using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCameraController : MonoBehaviour
{
    private Vector3 input;

    private Vector3Int blockPlacePos;

    private Vector2 lookInput;

    public LayerMask everything;

    private float speed = 25f;
    private float mouseHeldTime = 0;
    private float destroyBlockTime = 0;


    //
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!EditorManager.Instance.canMove)
        {
            return;
        }

        CalculateInput();

        //move the camera
        transform.localPosition += ((transform.right * input.x) + (transform.forward * input.z) + (Vector3.up * input.y)) * Time.deltaTime * speed;
        transform.rotation = Quaternion.Euler(lookInput.x, lookInput.y, 0);

        //Left Click
        if (Input.GetMouseButtonDown(0))
        {
            mouseHeldTime = 0;
        }

        if (Input.GetMouseButton(0))
        {
            mouseHeldTime += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0) && mouseHeldTime < 0.3f)
        {
            TryPlaceBlock();
        }

        //Right Click
        if (Input.GetMouseButtonDown(1))
        {
            destroyBlockTime = 0;
        }

        if (Input.GetMouseButton(1))
        {
            destroyBlockTime += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(1) && destroyBlockTime < 0.3f)
        {
            TryDestroyBlock();
        }
    }

    private void CalculateInput()
    {
        input.x = 0;
        input.y = 0;
        input.z = 0;

        //Left Right
        if (Input.GetKey(KeyCode.A))
        {
            input.x += -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            input.x += 1;
        }

        //Forward Back
        if (Input.GetKey(KeyCode.W))
        {
            input.z += +1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            input.z += -1;
        }

        //World Up Down
        if (Input.GetKey(KeyCode.Q))
        {
            input.y += 1;
        }
        if (Input.GetKey(KeyCode.E))
        {
            input.y += -1;
        }

        //Mouse Up
        lookInput.x += -Input.GetAxisRaw("Mouse Y");
        lookInput.x = Mathf.Clamp(lookInput.x, -90, 90);

        //Mouse Right
        lookInput.y += Input.GetAxisRaw("Mouse X");
        lookInput.y = Mathf.Repeat(lookInput.y, 360);
    }

    private void TryPlaceBlock()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit _hit, 15f, everything))
        {
            blockPlacePos.x = Mathf.RoundToInt((_hit.point.x - (-_hit.normal.x)) / GameConstants.Editor.blockSpace);
            blockPlacePos.y = Mathf.RoundToInt((_hit.point.y - (-_hit.normal.y)) / GameConstants.Editor.blockSpace);
            blockPlacePos.z = Mathf.RoundToInt((_hit.point.z - (-_hit.normal.z)) / GameConstants.Editor.blockSpace);

            bool _isTopOfBlock = _hit.point.y - Mathf.FloorToInt(_hit.point.y) > 0.5f;

            EditorManager.Instance.PlaceBlock(BlockType.Stone, blockPlacePos, _hit);
        }
    }

    private void TryDestroyBlock()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit _hit, 15f, everything))
        {
            DestroyImmediate(_hit.collider.gameObject);
        }
    }
}
