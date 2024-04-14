using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoveManager : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private LookAtManager m_LookAtManager;

    [SerializeField] private float m_MoveSpeed = 5f;
    [SerializeField] private float m_RunningSpeed = 8f;

    private static PlayerMoveManager m_Instance;
    public static PlayerMoveManager Instance { get => m_Instance; }
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

    public void Execute()
    {
        float axisH = Input.GetAxis(GameParameters.InputName.AXIS_HORIZONTAL);
        float axisV = Input.GetAxis(GameParameters.InputName.AXIS_VERTICAL);

        float speed = IsRuning() ? m_RunningSpeed : m_MoveSpeed;


        if (axisH == 0 && axisV == 0) return;

        //Vector3 velocity = m_Rigidbody.velocity;

        Vector3 direction = m_LookAtManager.transform.forward * axisV + m_LookAtManager.transform.right * axisH;
        direction.y = 0;
        //transform.forward = direction;
        Vector3 velocity = direction * speed;
        velocity.y = m_Rigidbody.velocity.y;
        m_Rigidbody.velocity = velocity;

    }    
}
