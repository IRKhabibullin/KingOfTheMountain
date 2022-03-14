using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    #region Singleton
    public static GameStateMachine Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameStateMachine>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<GameStateMachine>();
                }
            }
            return _instance;
        }
    }
    private static GameStateMachine _instance;

    void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning("Another instance of GameStateMachine tried to be created. Destroying it as only one instance should exist across all scenes.");
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }
    #endregion

    public string currentState { get; private set; }
    public List<string> gameStates;

    [GameState] [SerializeField] private string initialState;

    private Dictionary<string, Dictionary<string, StateTransition>> transitions;

    private void Start()
    {
        currentState = initialState;
        transitions = new Dictionary<string, Dictionary<string, StateTransition>>();

        // temporary solution with passing string state.
        // Possible fixes: scriptableObject, or derived from StateTransition classes for each transition
        foreach (var transition in GetComponentsInChildren<StateTransition>())
        {
            if (!transitions.ContainsKey(transition.fromState))
                transitions.Add(transition.fromState, new Dictionary<string, StateTransition>());
            transitions[transition.fromState].Add(transition.toState, transition);
        }
    }

    public void ChangeState(string newState)
    {
        if (!transitions.ContainsKey(currentState)) return;

        var transition = transitions[currentState];
        if (!transition.ContainsKey(newState)) return;

        currentState = newState;
        transition[newState].Activate();
    }
}