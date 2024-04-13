using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightManager : MonoBehaviour
{
    private static FlashLightManager m_Instance;
    public static FlashLightManager Instance { get => m_Instance; }

    private void Awake()
    {
        if (m_Instance != null) Destroy(gameObject);

        m_Instance = this;
    }

    public void Execute()
    {
        if (Input.GetMouseButtonDown(GameParameters.InputName.MOVE_FLASHLIGHT))
        {
            Vector3 mousePosition = Camera.current.ScreenToWorldPoint(Input.mousePosition);
            //mousePosition.z = Camera.current.transform.position.z + Camera.main.nearClipPlane;
            /*            Vector3 newPosition = transform.position;
                        newPosition.x = mousePosition.x;
                        newPosition.y = mousePosition.y;*/
            float moveSpeed = 5f;
            transform.position = Vector2.MoveTowards(transform.position, mousePosition, moveSpeed * Time.deltaTime);

            transform.position = mousePosition;

        }
    }
}
