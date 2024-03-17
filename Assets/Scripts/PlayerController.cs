using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_PlayerSpeed = 2.0f;
    [SerializeField] private float m_JumpHeight = 1.0f;
    [SerializeField] private float m_GravityValue = -9.81f;

    private CharacterController m_Controller;
    private Vector3 m_PlayerVelocity;
    private bool m_GroundedPlayer;
    private InputManager m_InputManager;
    private Transform m_CameraTransform;

    private void Start()
    {
        m_Controller = GetComponent<CharacterController>();
        m_InputManager = InputManager.Instance;
        m_CameraTransform = Camera.main.transform;
    }

    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        Vector2 movement = m_InputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = m_CameraTransform.right * move.x + m_CameraTransform.forward * move.z;
        move.y = 0f;
        m_Controller.Move(move * Time.deltaTime * m_PlayerSpeed);

        if (m_InputManager.IsPressLooking())
        {
            transform.forward = m_CameraTransform.forward;
            Cursor.visible = false;
        }
        else
        {
           /* Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            Mouse.current.WarpCursorPosition(screenPoint);*/
            Cursor.visible = true;
        }

        /*if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }*/
    }

    private void Jump()
    {
        m_GroundedPlayer = m_Controller.isGrounded;
        if (m_GroundedPlayer && m_PlayerVelocity.y < 0)
        {
            m_PlayerVelocity.y = 0f;
        }

        // Changes the height position of the player..
        if (m_InputManager.IsPlayerJumpedThisFrame() && m_GroundedPlayer)
        {
            m_PlayerVelocity.y += Mathf.Sqrt(m_JumpHeight * -3.0f * m_GravityValue);
        }

        m_PlayerVelocity.y += m_GravityValue * Time.deltaTime;
        m_Controller.Move(m_PlayerVelocity * Time.deltaTime);
    }
}
