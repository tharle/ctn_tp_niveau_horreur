using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private LookAtManager m_LookAtManager;

    private float m_MoveSpeed = 5f;
    private float m_RunningSpeed = 8f;

    private static PlayerMove m_Instance;
    public static PlayerMove Instance { get => m_Instance; }
    private void Awake()
    {
        if(m_Instance != null) Destroy(gameObject);

        m_Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_LookAtManager = LookAtManager.Instance;
    }

    private bool IsRuning()
    {
        return Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift);
    }

    public void Move()
    {
        float axisH = Input.GetAxis(GameParameters.InputName.AXIS_HORIZONTAL);
        float axisV = Input.GetAxis(GameParameters.InputName.AXIS_VERTICAL);

        float speed = IsRuning() ? m_RunningSpeed : m_MoveSpeed;


        if (axisH == 0 && axisV == 0) return;

        //Vector3 velocity = m_Rigidbody.velocity;

        Vector3 velocity = m_LookAtManager.transform.forward.normalized;
        velocity.x *= speed;
        velocity.y = 0;
        velocity.z *= speed;

        m_Rigidbody.velocity = velocity;

    }    
}
