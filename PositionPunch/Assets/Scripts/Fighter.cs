using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{

    [Header("Stats")]
    [SerializeField] public int MaxHealth = 10;
    [SerializeField] public int CurrentHealth = 10;
    [SerializeField] public int MaxCharge;
    [SerializeField] public int CurrentCharge;
    [SerializeField] private bool inAction;
    [SerializeField] private ActionManager.Action currentAction;
    private ActionManager.Action nextAction;
    public ActionManager _actionManager;
    private Transform resetTransform;
    public Animator anim;


    [SerializeField] protected bool isJabbing;
    [SerializeField] protected float jabDuration;
    [SerializeField] protected float jabTime;


    [SerializeField] protected bool isFeinting;
    [SerializeField] protected float feintDuration;
    [SerializeField] protected float feintTime;


    [SerializeField] protected bool isSlipping;
    [SerializeField] protected float slipTime;
    [SerializeField] protected float slipDuration;






    protected virtual void Start()
    {
        
        _actionManager = GameObject.FindGameObjectWithTag("ActionManager").GetComponent<ActionManager>();
        anim = GetComponentInChildren<Animator>();
        SetCurrentAction(ActionManager.Action.None);

        isJabbing = false;

        isFeinting = false;

        isSlipping = false;

    }

    // Update is called once per frame
    
    public void IncreaseCharge()
    {
        Debug.Log(gameObject.name + " Increased Charge!");
        if (CurrentCharge < MaxCharge)
        {
            CurrentCharge++;
        }
    }
    public void ResetPosition()
    {
        GetComponentInChildren<Transform>().SetPositionAndRotation(resetTransform.position, resetTransform.rotation);
    }

    #region Moves
    //private bool IsActing() => anim.GetBool("Jab") || anim.GetBool("Feint") || anim.GetBool("Slip") || anim.GetBool("Guard");
    public void Jab()
    {
        //if (!IsActing())
        anim.SetBool("Jab", true);
        isJabbing = true;
    }
    public void Feint()
    {
        //if (!IsActing())
        anim.SetBool("Feint", true);
        isFeinting = true;
    }

    public void Guard(bool isGuarding)
    {
        anim.SetBool("Guard", isGuarding);

    }
    public void Slip()
    {
        //if (!IsActing())
        anim.SetBool("Slip", true);
        isSlipping = true;
    }
    protected virtual private void MoveTime()
    {
        if (isJabbing)
        {
            jabDuration -= Time.deltaTime;
            if (jabDuration < 0)
            {
                isJabbing = false;
                jabDuration = jabTime;
            }
        }
        if (isFeinting)
        {
            feintDuration -= Time.deltaTime;
            if (feintDuration < 0)
            {
                isFeinting = false;
                feintDuration = feintTime;

            }
        }
        if (isSlipping)
        {
            slipDuration -= Time.deltaTime;
            if (slipDuration < 0)
            {
                isSlipping = false;
                slipDuration = slipTime;

            }
        }
    }


    #endregion
    #region Getters and Setters
    public ActionManager.Action GetCurrentAction() { return currentAction; }
    public void SetCurrentAction(ActionManager.Action action) { currentAction = action; }

    public ActionManager.Action getNextAction() { return nextAction; }
    public void SetNextAction(ActionManager.Action action) { nextAction = action; }

    public void TakeDamage(Fighter fighter)
    {

        
        CurrentHealth = Mathf.Max(CurrentHealth - 1 + fighter.GetCharge(), 0);
        fighter.SpendCharge();

        Debug.Log(gameObject.name + " Took Damage!" + " Current health: " + CurrentHealth);

        DeathCheck();
        return;
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
