using System.Collections.Generic;
using UnityEngine;


public class GameStateManager : MonoBehaviour
{
    private AGameState m_CurrentState;
    private Dictionary<EGameState, AGameState> m_States;

    [SerializeField] EGameState m_CurrentStateId;


    private void OnDisable()
    {
        GameStateEvent.Instance.ClearAll();
    }

    void Start()
    {
        m_States = new Dictionary<EGameState, AGameState>();
        m_States.Add(EGameState.PauseMenu, new GameStatePauseMenu(this));
        m_States.Add(EGameState.Run, new GameStateRun(this));
        m_States.Add(EGameState.Interract, new GameStateInterract(this));
        m_States.Add(EGameState.Win, new GameStateWin(this));
        m_States.Add(EGameState.Lose, new GameStateLose(this));

        m_CurrentStateId = EGameState.Run;
        m_CurrentState = m_States[m_CurrentStateId];
    }

    private void Update()
    {
        m_CurrentState.Execute();
    }

    public void ChangeState(EGameState stateId)
    {
        m_CurrentState.Exit();
        m_CurrentState = m_States[stateId];
        m_CurrentStateId = stateId;
        m_CurrentState.Enter();
    }

}
