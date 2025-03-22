using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class Controls : MonoBehaviour
{

    [Header("Game Stats")]
    [SerializeField] private float _maxGuardTime;
    private float guardTimer;
    [SerializeField] private float _maxFeintTime;
    private float feintTimer;
    [SerializeField] private float _maxJabTime;
    private float jabTimer;
    [SerializeField] private float _maxSlipTime;
    private float slipTimer;

    private float actionTimer;

    [Header("Player Stats")]
    [SerializeField] private int Health;
    [SerializeField] private int MaxCharge;
    [SerializeField] private int CurreCharge;
    [SerializeField] private bool inAction;
    [SerializeField] private Action currentAction;
    private Action nextAction;
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
    enum Action 
    {
        None,
        Jab,
        Feint,
        Guard,
        Slip
    }
    private void Start()
    {
        repeatCounter = 1;
        repeatCounterString = "!";
        currentAction = Action.None;

    }

    // Update is called once per frame
    void Update()
    {
        
        #region Inputs
        if(Input.GetKeyDown(KeyCode.J))
        {   
            updateAction(Action.Jab);
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            updateAction(Action.Feint);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            updateAction(Action.Guard);
        }
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            updateAction(Action.Slip);
        }
        #endregion
    }
    
    private void updateAction(Action action)
    {
        nextAction = action;
        RepeatStringFunction();
        currentAction = action;
        uiText.text = (currentAction.ToString()) + repeatCounterString;
    }
    public void RepeatStringFunction()
    {
        
        
        if (nextAction == currentAction)
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
