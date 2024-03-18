using UnityEngine;
using Cinemachine;

public class CinemachinePOVExtension : CinemachineExtension
{
    [SerializeField] private Vector2 m_Speed = new Vector2(10f, 5f); // speed for looking around
    [SerializeField] private float m_ClampAngle = 80f;

    private InputManager m_InputManager;
    private Vector3 m_StartingRotation;

    protected override void Awake()
    {
        m_InputManager = InputManager.Instance;
        m_StartingRotation = transform.localRotation.eulerAngles;
        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        
        if (vcam.Follow && stage == CinemachineCore.Stage.Aim)
        {
            Vector2 deltaInput = m_InputManager.GetMouseDelta();
            m_StartingRotation.x += deltaInput.x * m_Speed.y * Time.deltaTime; // its rotate in AXIS, that why I put the speed Y in X and vice versa
            m_StartingRotation.y += deltaInput.y * m_Speed.x * Time.deltaTime;
            m_StartingRotation.y = Mathf.Clamp(m_StartingRotation.y, -m_ClampAngle, m_ClampAngle);
            state.RawOrientation = Quaternion.Euler(-m_StartingRotation.y,  m_StartingRotation.x, 0f); // its rotate in AXIS
        }
    }
}
