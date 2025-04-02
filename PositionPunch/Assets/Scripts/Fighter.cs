using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
   
    [Header("Stats")]
    [SerializeField] private int MaxHealth;
    [SerializeField] private int CurrentHealth;
    [SerializeField] private int MaxCharge;
    [SerializeField] private int CurrentCharge;
    [SerializeField] private bool inAction;
    [SerializeField] private ActionManager.Action currentAction;
    private ActionManager.Action nextAction;
    public ActionManager _actionManager;




    private void Start()
    {
        _actionManager = GameObject.FindGameObjectWithTag("ActionManager").GetComponent<ActionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IncreaseCharge() 
    {
        if (CurrentCharge<MaxCharge)
        {
            CurrentCharge++;
        }
    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
    }
    #region Getters and Setters
    public ActionManager.Action GetCurrentAction() { return currentAction; }
    public void SetCurrentAction(ActionManager.Action action) { currentAction = action; }

    public ActionManager.Action getNextAction() { return nextAction; }
    public void SetNextAction(ActionManager.Action action) { nextAction = action; }
    public int GetHealth() { return CurrentHealth; }
    public int GetCharge() { return CurrentCharge; }
    public void AddCharge() { CurrentCharge++; }
    public void SpendCharge() { CurrentCharge = 0; }
    #endregion
}
