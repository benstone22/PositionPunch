using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    private Controls _playerControl;
    private OpponentScript _opponent;
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

    [SerializeField] private Animator _playerAnim;
    [SerializeField] private Animator _opponentAnim;
    /* ARRAY KEY
    *       jab           feint           guard           slip            none
    * jab   [jab jab]     [jab feint]     [jab guard]     [jab slip]      [jab none]
    * feint [feint jab]   [feint feint]   [feint guard]   [feint slip]    [feint none]
    * guard [guard jab]   [guard feint]   [guard guard]   [guard slip]    [guard none]
    * slip  [slip jab]    [slip feint]    [slip guard]    [slip slip]     [slip none]
    * none  [none jab]    [none feint]    [none guard]    [none slip]     [none none]
    * 
     */
    /* ARRAY KEY
    *       jab           feint           guard           slip            none
    * jab   [jab jab]     [jab feint]     [jab guard]     [jab slip]      [jab none]
    * feint               [feint feint]   [feint guard]   [feint slip]    [feint none]
    * guard                               [guard guard]   [guard slip]    [guard none]
    * slip                                                [slip slip]     [slip none]
    * none                                                                [none none]
    * 
     */
    /// <summary>
    /// 
    /// </summary>





    /// <summary>
    /// <para>inputs</para>
    /// 0 jab = j 
    /// <para>1 feint = k</para>
    /// 2 guard = l
    /// <para>3 slip = ;</para>
    /// </summary>
    public enum Action
    {
        Jab,
        Feint,
        Guard,
        Slip,
        None
    }
    
  


    void Start()
    {
        
        _playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<Controls>();
        _opponent = GameObject.FindGameObjectWithTag("Opponent").GetComponent<OpponentScript>();
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

    private void UpdateUI() //TODO: Refactor into UI Manager
    {
        uiText.text = (_playerControl.getNextAction().ToString()) + repeatCounterString;
    }
    public void RepeatStringFunction()
    {
        if (_playerControl.getNextAction() == _playerControl.GetCurrentAction() && _playerControl.getNextAction().ToString()!="Guard")
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
    
    public void Exchange()
    {
        if (_playerControl.GetCurrentAction().Equals(Action.Jab))
        {
            #region opponent jab
            if (_opponent.GetCurrentAction().Equals(Action.Jab))
            {
                if (_playerControl.GetCharge() > _opponent.GetCharge())
                {
                    _opponent.TakeDamage(DamageCalc(_playerControl, true));
                }
                else
                {
                    _playerControl.TakeDamage(DamageCalc(_opponent, true));
                }
            }
            #endregion
            if (_opponent.GetCurrentAction().Equals(Action.Feint)|| _opponent.GetCurrentAction().Equals(Action.None))
            {
                _opponent.TakeDamage(DamageCalc(_playerControl, true));
            }
            if (_opponent.GetCurrentAction().Equals(Action.Guard))
            {
                _opponent.IncreaseCharge();
            }
            if(_opponent.GetCurrentAction().Equals(Action.Slip))
            {
                _playerControl.TakeDamage(DamageCalc(_opponent, true));
            }
            
        }
        if (_playerControl.GetCurrentAction().Equals(Action.Feint))
        {
            if (_opponent.GetCurrentAction().Equals(Action.Jab))
            {
                _playerControl.TakeDamage(DamageCalc(_opponent, true));
            }
            if (_opponent.GetCurrentAction().Equals(Action.Guard)|| _opponent.GetCurrentAction().Equals(Action.Slip))
            {
                _opponent.TakeDamage(DamageCalc(_playerControl,true));
            }
            
        }
        if (_playerControl.GetCurrentAction().Equals(Action.Guard))
        {
            if (_opponent.GetCurrentAction().Equals(Action.Jab))
            {
                _playerControl.IncreaseCharge();
            }
            if (_opponent.GetCurrentAction().Equals(Action.Feint))
            {
                _playerControl.TakeDamage(DamageCalc(_opponent, false));
            }
            
        }
        if(_playerControl.GetCurrentAction().Equals(Action.Slip))
        {
            if (_opponent.GetCurrentAction().Equals(Action.Jab))
            {
                _opponent.TakeDamage(DamageCalc(_playerControl, true));
            }
            if (_opponent.GetCurrentAction().Equals(Action.Feint))
            {
                _playerControl.TakeDamage(DamageCalc(_opponent, false));
            }
        }
    }
    
    private int DamageCalc(Fighter fighter, bool expend)
    {
        
        int damage = 1 + fighter.GetCharge();
        if (expend) { fighter.SpendCharge(); }
        return damage;
    }    

}
