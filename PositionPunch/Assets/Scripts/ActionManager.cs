using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Dictionary<ExchangeCode, HashSet<Tuple<Action,Action>>> Exchanges;


    

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
    
    enum ExchangeCode
    {
        
        #region jab codes
        jj,
        jf,
        jg,
        js,
        jn,
        #endregion
        #region feint codes
        ff,
        fg,
        fs,
        fn,
        #endregion
        #region guard codes
        gg,
        gs,
        gn,
        #endregion
        #region slip codes
        ss,
        sn,
        #endregion
        #region none codes
        nn
        #endregion
        
    }


    void Start()
    {
        InitializeReactionDictionary();
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
    
    private void InitializeReactionDictionary()
    {
        Exchanges = new Dictionary<ExchangeCode, HashSet<Tuple<Action, Action>>> ();

        HashSet<Tuple<Action,Action>> jabjab = new HashSet<Tuple<Action, Action>>();
        jabjab.Add(Tuple.Create(Action.Jab,Action.Jab));
        Exchanges.Add(ExchangeCode.jj,jabjab);


        HashSet<Tuple<Action, Action>> jabfeint = new HashSet<Tuple<Action, Action>>();
        jabfeint.Add(Tuple.Create(Action.Jab, Action.Feint));
        jabfeint.Add(Tuple.Create(Action.Feint, Action.Jab));
        Exchanges.Add(ExchangeCode.jf, jabfeint);


        HashSet<Tuple<Action, Action>> jabguard = new HashSet<Tuple<Action, Action>>();
        jabfeint.Add(Tuple.Create(Action.Jab, Action.Guard));
        jabfeint.Add(Tuple.Create(Action.Guard, Action.Jab));
        Exchanges.Add(ExchangeCode.jg, jabguard);

        HashSet<Tuple<Action, Action>> jabslip = new HashSet<Tuple<Action, Action>>();
        jabfeint.Add(Tuple.Create(Action.Jab, Action.Slip));
        jabfeint.Add(Tuple.Create(Action.Slip, Action.Jab));
        Exchanges.Add(ExchangeCode.js, jabslip);

        HashSet<Tuple<Action, Action>> jabnone = new HashSet<Tuple<Action, Action>>();
        jabfeint.Add(Tuple.Create(Action.Jab, Action.None));
        jabfeint.Add(Tuple.Create(Action.None, Action.Jab));
        Exchanges.Add(ExchangeCode.jn, jabnone);

        HashSet<Tuple<Action, Action>> feintfeint = new HashSet<Tuple<Action, Action>>();
        jabfeint.Add(Tuple.Create(Action.Feint, Action.Feint));
        Exchanges.Add(ExchangeCode.ff, feintfeint);

        HashSet<Tuple<Action, Action>> feintguard = new HashSet<Tuple<Action, Action>>();
        jabfeint.Add(Tuple.Create(Action.Feint, Action.Guard));
        jabfeint.Add(Tuple.Create(Action.Guard, Action.Feint));
        Exchanges.Add(ExchangeCode.fg, feintguard);

        HashSet<Tuple<Action, Action>> feintslip = new HashSet<Tuple<Action, Action>>();
        jabfeint.Add(Tuple.Create(Action.Feint, Action.Slip));
        jabfeint.Add(Tuple.Create(Action.Slip, Action.Feint));
        Exchanges.Add(ExchangeCode.fs, feintslip);

        HashSet<Tuple<Action, Action>> feintnone = new HashSet<Tuple<Action, Action>>();
        jabfeint.Add(Tuple.Create(Action.Feint, Action.None));
        jabfeint.Add(Tuple.Create(Action.None, Action.Feint));
        Exchanges.Add(ExchangeCode.fn, feintnone);

        HashSet<Tuple<Action, Action>> guardguard = new HashSet<Tuple<Action, Action>>();
        jabfeint.Add(Tuple.Create(Action.Guard, Action.Guard));
        Exchanges.Add(ExchangeCode.gg, guardguard);

        HashSet<Tuple<Action, Action>> guardslip = new HashSet<Tuple<Action, Action>>();
        jabfeint.Add(Tuple.Create(Action.Slip, Action.Guard));
        jabfeint.Add(Tuple.Create(Action.Guard, Action.Slip));
        Exchanges.Add(ExchangeCode.gs, guardslip);


        HashSet<Tuple<Action, Action>> guardnone = new HashSet<Tuple<Action, Action>>();
        jabfeint.Add(Tuple.Create(Action.None, Action.Guard));
        jabfeint.Add(Tuple.Create(Action.Guard, Action.None));
        Exchanges.Add(ExchangeCode.gn, guardnone);

        HashSet<Tuple<Action, Action>> slipslip = new HashSet<Tuple<Action, Action>>();
        jabfeint.Add(Tuple.Create(Action.Slip, Action.Slip));
        Exchanges.Add(ExchangeCode.ss, slipslip);

        HashSet<Tuple<Action, Action>> slipnone = new HashSet<Tuple<Action, Action>>();
        jabfeint.Add(Tuple.Create(Action.None, Action.Slip));
        jabfeint.Add(Tuple.Create(Action.Slip, Action.None));
        Exchanges.Add(ExchangeCode.sn, slipnone);

        HashSet<Tuple<Action, Action>> nonenone = new HashSet<Tuple<Action, Action>>();
        jabfeint.Add(Tuple.Create(Action.None, Action.None));
        Exchanges.Add(ExchangeCode.nn, nonenone);

    }

    



}
