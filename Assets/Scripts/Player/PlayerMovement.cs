using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private PlayerCamera m_LookAtManager;

    [SerializeField] private float m_MoveSpeed = 5f;
    [SerializeField] private float m_RunningSpeed = 8f;
    private bool m_InCooldown = false;
    [SerializeField] private float m_StaminaMax = 1.5f;
    private float m_Stamina;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_LookAtManager = PlayerCamera.Instance;
        m_Stamina = m_StaminaMax;
    }

    private bool IsRuning()
    {
        return !m_InCooldown && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
    }

    public void Execute()
    {
        float axisH = Input.GetAxis(GameParameters.InputName.AXIS_HORIZONTAL);
        float axisV = Input.GetAxis(GameParameters.InputName.AXIS_VERTICAL);

        float speed = m_MoveSpeed;
        if (IsRuning())
        {
            speed = m_RunningSpeed;
            ConsumeStamina();
        } else
        {
            RegenStamina();
        }


        if (axisH == 0 && axisV == 0) return;

        //Vector3 velocity = m_Rigidbody.velocity;

        Vector3 direction = m_LookAtManager.transform.forward * axisV + m_LookAtManager.transform.right * axisH;
        direction.y = 0;
        //transform.forward = direction;
        Vector3 velocity = direction * speed;
        velocity.y = m_Rigidbody.velocity.y;
        m_Rigidbody.velocity = velocity;

    }

    private void RegenStamina()
    {
        if (m_Stamina >= m_StaminaMax) return;

        m_Stamina += Time.deltaTime/1.5f;
        
        if (m_Stamina >= m_StaminaMax)
        {
            m_InCooldown = false;
            m_Stamina = m_StaminaMax;
        }

        NotifyStaminaChanged();
    }

    private void ConsumeStamina()
    {
        if (m_Stamina <= 0) return;

        m_Stamina -= Time.deltaTime;

        if(m_Stamina <= 0)
        {
            m_InCooldown = true;
            m_Stamina = 0;
        }

        NotifyStaminaChanged();
    }

    private void NotifyStaminaChanged()
    {
        PlayerController.Instance.SPNofity(m_Stamina / m_StaminaMax);
    }
}
