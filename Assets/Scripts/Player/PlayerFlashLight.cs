using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerFlashLight : MonoBehaviour
{
    [SerializeField] private float m_MaxDuration = 10f;
    [SerializeField] private float m_TimeInSecToConsume = 1f;
    private float m_Duration;
    [SerializeField] private float m_Sensitivity = 2f;
    float m_VerticalRotation = 0f;
    float m_HorizontalRotation = 0f;

    bool m_TurnOnToggled = true;

    private void Start()
    {
        Recharge();
    }


    public void Recharge()
    {
        m_Duration = m_MaxDuration;
    }

    public void Execute()
    {
        if (Input.GetKeyDown(GameParameters.InputName.PLAYER_FLASHLIGHT_TOOGLE))
        {
            ToogleLight();
        }

        if (Input.GetMouseButton(GameParameters.InputName.PLAYER_FLASHLIGHT_MOVE)) 
        { 
            float inputX = Input.GetAxis(GameParameters.InputName.AXIS_MOUSE_HORIZONTAL) * m_Sensitivity;
            float inputY = Input.GetAxis(GameParameters.InputName.AXIS_MOUSE_VERTICAL) * m_Sensitivity;

            m_VerticalRotation -= inputY;
            m_VerticalRotation = Mathf.Clamp(m_VerticalRotation, -20f, 20f);

            m_HorizontalRotation += inputX;
            m_HorizontalRotation = Mathf.Clamp(m_HorizontalRotation, -40f, 40f);

            transform.localEulerAngles = Vector3.right * m_VerticalRotation + m_HorizontalRotation * Vector3.up;
        }

        if (Input.GetMouseButtonUp(GameParameters.InputName.PLAYER_FLASHLIGHT_MOVE))
        {
            m_VerticalRotation = 0;
            m_HorizontalRotation = 0;
            transform.localEulerAngles = Vector3.right * m_VerticalRotation + m_HorizontalRotation * Vector3.up;
        }

        if (m_TurnOnToggled) ConsumeDuration();
    }

    private void ToogleLight()
    {
        if (m_Duration <= 0) return;

        m_TurnOnToggled = !m_TurnOnToggled;
        GetComponent<Light>().enabled = m_TurnOnToggled;
    }

    private void ConsumeDuration()
    {
        if (m_Duration > 0)
        {   
            m_Duration -= Time.deltaTime;
            PlayerController.Instance.LightDurationNofity(m_Duration / m_MaxDuration);
        }
        else
        {
            m_TurnOnToggled = false;
            GetComponent<Light>().enabled = false;
        }


    }
}
