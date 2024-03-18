using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager m_Instance;
    public static InputManager Instance { get 
        { 
            if (m_Instance == null)
            {
                GameObject go = new GameObject(nameof(InputManager));
                m_Instance = go.AddComponent<InputManager>();
            }

            return m_Instance; 
        } 
    }

    private PlayerControls m_PlayerControls;

    private void Awake()
    {
        m_PlayerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        m_PlayerControls.Enable();
    }

    private void OnDisable()
    {
        m_PlayerControls.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return m_PlayerControls.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return m_PlayerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool IsPlayerJumpedThisFrame()
    {
        return m_PlayerControls.Player.Jump.WasPressedThisFrame();
    }

    public bool IsLooking()
    {
        return m_PlayerControls.Player.Looking.IsPressed();
    }

    public bool IsLookingReleaseThisFrame()
    {
        return m_PlayerControls.Player.Looking.WasReleasedThisFrame();
    }

    public bool IsLookingPressedThisFrame()
    {
        return m_PlayerControls.Player.Looking.WasPressedThisFrame();
    }
}
