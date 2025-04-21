using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
   
    [Header("Stats")]
    [SerializeField] public int MaxHealth = 10;
    [SerializeField] public int CurrentHealth = 10;
    [SerializeField] private int MaxCharge;
    [SerializeField] private int CurrentCharge;
    [SerializeField] private bool inAction;
    [SerializeField] private ActionManager.Action currentAction;
    private ActionManager.Action nextAction;
    public ActionManager _actionManager;
    [SerializeField] private Animator _animator;
    private Transform resetTransform;


    protected virtual void Start()
    {
        resetTransform = GetComponentInChildren<Transform>();
        _actionManager = GameObject.FindGameObjectWithTag("ActionManager").GetComponent<ActionManager>();
        SetCurrentAction(ActionManager.Action.None);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IncreaseCharge() 
    {
        Debug.Log(gameObject.name + " Increased Charge!");
        if (CurrentCharge<MaxCharge)
        {
            CurrentCharge++;
        }
    }
    public void ResetPosition()
    {
        GetComponentInChildren  <Transform>().SetPositionAndRotation(resetTransform.position,resetTransform.rotation);
    }

    
    #region Getters and Setters
    public ActionManager.Action GetCurrentAction() { return currentAction; }
    public void SetCurrentAction(ActionManager.Action action) { currentAction = action; }

    public ActionManager.Action getNextAction() { return nextAction; }
    public void SetNextAction(ActionManager.Action action) { nextAction = action; }
    public void TakeDamage(int damage)
    {

        CurrentHealth -= damage;
        Debug.Log(gameObject.name + " Took Damage!" + " Current health: " + CurrentHealth);
        DeathCheck();
    }
    public void DeathCheck()
    {
        if (CurrentHealth <= 0)
        {
            Debug.Log(gameObject.name + " is dead!");
        }
    }
    public int GetCharge() { return CurrentCharge; }
    public void AddCharge() { CurrentCharge++; }
    public void SpendCharge() { CurrentCharge = 0; }
    #endregion
}
