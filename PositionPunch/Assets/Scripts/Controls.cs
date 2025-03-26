using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class Controls : MonoBehaviour
{

    

    private float actionTimer;

    [Header("Player Stats")]
    [SerializeField] private int Health;
    [SerializeField] private int MaxCharge;
    [SerializeField] private int CurreCharge;
    [SerializeField] private bool inAction;
    [SerializeField] private ActionManager.Action currentAction;
    private ActionManager.Action nextAction;
    private ActionManager actionManager;
    

    
    private void Start()
    {
        actionManager = GameObject.FindGameObjectWithTag("ActionManager").GetComponent<ActionManager>();

        currentAction = ActionManager.Action.None;
        

    }

    // Update is called once per frame
    void Update()
    {
        
        #region Inputs
        if(Input.GetKeyDown(KeyCode.J))
        {   
            actionManager.updatePlayerAction(ActionManager.Action.Jab);

        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            actionManager.updatePlayerAction(ActionManager.Action.Feint);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            actionManager.updatePlayerAction(ActionManager.Action.Guard);
        }
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            actionManager.updatePlayerAction(ActionManager.Action.Slip);
        }
        #endregion
    }
    
    
    #region Getters and Setters
    public ActionManager.Action GetCurrentAction() { return currentAction;}
    public void SetCurrentAction(ActionManager.Action action) { currentAction = action;}

    public ActionManager.Action getNextAction() { return nextAction; }
    public void SetNextAction(ActionManager.Action action) { nextAction = action;}
    #endregion


}
