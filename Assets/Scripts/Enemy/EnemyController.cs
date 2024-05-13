using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    private bool m_Attacking = false;
    [SerializeField] private float m_Speed = 8f;
    [SerializeField] private float m_OffDistance = 10f;
    [SerializeField] private List<Transform> m_Poistions;
    private Rigidbody m_Rigidbody;

    private List<EAudio> m_Sons  = new List<EAudio>{ EAudio.SFXEnemy1, EAudio.SFXEnemy2 , EAudio.SFXEnemy3 , EAudio.SFXEnemy4 , EAudio.SFXEnemy5 };
    private AudioSource m_AudioPlaying;
    private EAudio m_AudioId;

    private float m_ElapseTime;


    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        ToStartPosition();
    }

    private void Update()
    {
        if (m_Attacking)
        {
            MakeNoise();

            Vector3 direction = PlayerController.Instance.transform.position - transform.position;
            direction.Normalize();
            transform.forward = direction;
            float offDistance = Vector3.Distance(PlayerController.Instance.transform.position, transform.position) / m_OffDistance;
            offDistance += 0.5f;
            m_Rigidbody.velocity = direction * m_Speed * offDistance;

            m_ElapseTime += Time.deltaTime;
            if(m_ElapseTime >= 15) ToStartPosition(false);
        }
    }

    private void MakeNoise() {

        if (m_AudioPlaying != null && m_AudioPlaying.isPlaying) return;

        m_AudioPlaying?.Stop();
        int index = Random.Range(0, m_Sons.Count);
        m_AudioId = m_Sons[index];
        //m_AudioPlaying = AudioManager.GetInstance().Play(m_AudioId, transform.position, false);
        m_AudioPlaying = AudioManager.GetInstance().Play(m_AudioId, PlayerController.Instance.transform.position, false);
    }


    private void ToStartPosition(bool waiting = true)
    {
        m_Rigidbody.velocity = Vector3.zero;
        int index = Random.Range(0, m_Poistions.Count);
        transform.position = m_Poistions[index].position;
        m_ElapseTime = 0;
        if (waiting) StartCoroutine(WaitingToAttack());

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameParameters.TagName.FLASHLIGHT) && PlayerController.Instance.IsFlashLightTurnOn())
        {
            Debug.Log("MONSTER HIT");
            // TODO: ADD son hit enemy
            ToStartPosition();
            return;
        }

        if (other.CompareTag(GameParameters.TagName.PLAYER))
        {
            PlayerController.Instance.GetHit();
            ToStartPosition();
        }
    }

    private IEnumerator WaitingToAttack()
    {
        m_Attacking = false;
        yield return new WaitForSeconds(Random.Range(2, 7));
        m_Attacking = true;
    }
}
