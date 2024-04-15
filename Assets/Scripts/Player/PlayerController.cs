using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement m_Movement;
    private PlayerFlashLight m_FlashLight;
    private PlayerCamera m_Camera;

    public event Action OnCloseTextInfo;

    private static PlayerController m_Instance;
    public static PlayerController Instance { get => m_Instance; }

    private void Awake()
    {
        if (m_Instance != null) Destroy(gameObject);

        m_Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        m_Movement = GetComponent<PlayerMovement>();
        m_FlashLight = GetComponentInChildren<PlayerFlashLight>();
        m_Camera = PlayerCamera.Instance;
    }

    public void Execute()
    {
        m_Movement.Execute();
        m_FlashLight.Execute();
        m_Camera.Execute();
    }



    public void CloseTextInfo()
    {
        OnCloseTextInfo?.Invoke();
    }
}
