using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private PlayerCamera m_LookAtManager;
    private AudioSource m_AudioMove;
    private EAudio m_AudioId;

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

        Vector3 direction = m_LookAtManager.transform.forward * axisV + m_LookAtManager.transform.right * axisH;
        direction.y = 0;

        float speed = m_MoveSpeed;
        if (IsRuning())
        {
            speed = m_RunningSpeed;
            ConsumeStamina();

            if (direction.magnitude > 0 && (m_AudioId != EAudio.SFXRunDirty || !m_AudioMove.isPlaying) ) 
            {
                m_AudioMove?.Stop();
                m_AudioId = EAudio.SFXRunDirty;
                m_AudioMove = AudioManager.GetInstance().Play(m_AudioId, transform.position, true);
            } 
        } else
        {
            RegenStamina();
            if (direction.magnitude > 0 && (m_AudioId != EAudio.SFXWalkDirty || !m_AudioMove.isPlaying))
            {
                m_AudioMove?.Stop();
                m_AudioId = EAudio.SFXWalkDirty;
                m_AudioMove = AudioManager.GetInstance().Play(m_AudioId, transform.position, true);
            }
        }

        if (direction.magnitude == 0) m_AudioMove?.Stop();


        if (axisH == 0 && axisV == 0) return;

        //Vector3 velocity = m_Rigidbody.velocity;

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
