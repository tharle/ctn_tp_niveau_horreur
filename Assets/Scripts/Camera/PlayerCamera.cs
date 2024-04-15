using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private static PlayerCamera m_Instance;
    public static PlayerCamera Instance { get => m_Instance; }

    [SerializeField] private float m_Sensitivity = 2f;
    float m_VerticalRotation = 0f;
    float m_HorizontalRotation = 0f;

    private void Awake()
    {
        if (m_Instance != null) Destroy(gameObject);

        m_Instance = this;
    }

    public void Execute()
    {
        if (Input.GetMouseButton(GameParameters.InputName.PLAYER_FLASHLIGHT_MOVE)) return;

        float inputX = Input.GetAxis(GameParameters.InputName.AXIS_MOUSE_HORIZONTAL) * m_Sensitivity;
        float inputY = Input.GetAxis(GameParameters.InputName.AXIS_MOUSE_VERTICAL) * m_Sensitivity;

        Debug.Log("FL : " + inputX + "," + inputY);

        m_VerticalRotation -= inputY;
        m_VerticalRotation = Mathf.Clamp(m_VerticalRotation, -20f, 20f);

        m_HorizontalRotation += inputX;
        //m_HorizontalRotation = Mathf.Clamp(m_HorizontalRotation, -40f, 40f);

        transform.localEulerAngles = Vector3.right * m_VerticalRotation + m_HorizontalRotation * Vector3.up;
        PlayerController.Instance.transform.forward = transform.forward;
        //transform.Rotate(Vector3.up * inputX);
    }
}
