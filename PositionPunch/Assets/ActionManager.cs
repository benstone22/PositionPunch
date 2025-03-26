using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    private Controls _playerControl;
    private GameObject _opponent;
    [SerializeField] private Action opponentAction;

    [Header("Game Stats")]
    [SerializeField] private float _maxGuardTime;
    private float guardTimer;
    [SerializeField] private float _maxFeintTime;
    private float feintTimer;
    [SerializeField] private float _maxJabTime;
    private float jabTimer;
    [SerializeField] private float _maxSlipTime;
    private float slipTimer;

    private string repeatCounterString;
    private int repeatCounter;
    public TextMeshProUGUI uiText;
    /// <summary>
    /// <para>inputs</para>
    /// jab = j 
    /// <para>feint = k</para>
    /// guard = l
    /// <para>slip = ;</para>
    /// </summary>
    public enum Action
    {
        None,
        Jab,
        Feint,
        Guard,
        Slip
    }
    
    void Start()
    {
        _playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<Controls>();
        _opponent = GameObject.FindGameObjectWithTag("Opponent");
        repeatCounter = 1;
        repeatCounterString = "!";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updatePlayerAction(ActionManager.Action action)
    {
        _playerControl.SetNextAction(action);
        RepeatStringFunction();
        _playerControl.SetCurrentAction(action);
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        uiText.text = (_playerControl.getNextAction().ToString()) + repeatCounterString;
    }
    public void RepeatStringFunction()
    {
        if (_playerControl.getNextAction() == _playerControl.GetCurrentAction())
        {
            repeatCounter++;
            repeatCounterString = " x" + repeatCounter + "!";
        }
        else
        {
            repeatCounter = 1;
            repeatCounterString = "!";
        }
    }
}
