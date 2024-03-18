using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_PlayerSpeed = 2.0f;
    [SerializeField] private float m_JumpHeight = 1.0f;
    [SerializeField] private float m_GravityValue = -9.81f;
    [SerializeField] private Transform m_Head;

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
        Cursor.visible = false;
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

        

        /*if (m_InputManager.IsLooking())
        {
            m_Head.forward = m_CameraTransform.forward;
            transform.forward = m_Head.forward;
            Cursor.visible = false;
        }*/

        /*if (m_InputManager.IsLookingReleaseThisFrame())
        {
            Mouse.current.WarpCursorPosition(new Vector2(Screen.width / 2, Screen.height / 2));
            Cursor.visible = true;
            m_CameraTransform.forward = transform.forward;
        }*/

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
