using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraAudioManager : MonoBehaviour
{
    [SerializeField] AudioSource m_Ambiance;
    [SerializeField] AudioSource m_Win;
    [SerializeField] AudioSource m_Lose;

    private void Start()
    {
        GameStateEvent.Instance.SubscribeTo(EGameState.Win, OnWin);
        GameStateEvent.Instance.SubscribeTo(EGameState.Lose, OnLose);
    }

    private void OnLose(bool obj)
    {
        m_Ambiance.Stop();
        m_Lose.Play();
    }

    private void OnWin(bool obj)
    {
        m_Ambiance.Stop();
        m_Win.Play();
    }
}
