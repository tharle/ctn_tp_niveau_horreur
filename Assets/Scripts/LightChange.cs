using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChange : MonoBehaviour
{
    Light m_Light;
    // Start is called before the first frame update
    void Start()
    {
        m_Light = GetComponent<Light>();
        StartCoroutine(Clic()); ;
    }

    private IEnumerator Clic() 
    {
        while (true)
        {
            m_Light.color = Color.black;
            yield return new WaitForSeconds(1);
            m_Light.color = Color.red;
            yield return new WaitForSeconds(1);
        }
    }
}
