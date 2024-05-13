using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement m_Movement;
    private PlayerFlashLight m_FlashLight;
    private PlayerCamera m_Camera;

    public event Action OnCloseTextInfo;
    public event Action<float> OnChangeHP;
    public event Action<float> OnChangeSP;
    public event Action<float> OnChangeLightDuration;

    private float m_HP = 10;
    private float m_HPMax = 10;

    private Dictionary<EKeyType, bool> m_Invetory;

    private static PlayerController m_Instance;
    public static PlayerController Instance { get => m_Instance; }

    private void Awake()
    {
        if (m_Instance != null) Destroy(gameObject);

        m_Instance = this;
    }

    private void Start()
    {
        m_Invetory = new Dictionary<EKeyType, bool>();
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
    public void HPNofity()
    {
        OnChangeHP?.Invoke(m_HP/m_HPMax);
    }
    
    public void SPNofity(float SPRatio)
    {
        OnChangeSP?.Invoke(SPRatio);
    }

    public void LightDurationNofity(float lightDurationRatio)
    {
        OnChangeLightDuration?.Invoke(lightDurationRatio);
    }

    public void AddKeyToInvetory(EKeyType key)
    {
        if (!HasKeyInInvetory(key)) m_Invetory.Add(key, true);
        else m_Invetory[key] = true;
    }

    public bool HasKeyInInvetory(EKeyType keyId)
    {
        return m_Invetory.ContainsKey(keyId);
    }

    public bool IsWin()
    {
        return m_Invetory.ContainsKey(EKeyType.Win) && m_Invetory[EKeyType.Win];
    }

    public void GetHit()
    {
        m_HP--;
        HPNofity();
    }

    public bool IsDead()
    {
        return m_HP <= 0;
    }

    public bool IsFlashLightTurnOn()
    {
        return m_FlashLight.IsTurnOn();
    }
}
