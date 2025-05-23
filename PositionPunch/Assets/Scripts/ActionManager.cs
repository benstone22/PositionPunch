
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    private static Controls _playerControl;
    private static OpponentScript _opponent;
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


    public static Controls GetPlayer() { return _playerControl; }
    public static OpponentScript GetOpponent() { return _opponent; }

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
        bool exchangeBool = !((_playerAnim.parameters.Equals(_opponentAnim.parameters) && (PlayerIsGuarding() || PlayerIsIdle()) || (PlayerIsIdle() && OpponentIsGuarding() || PlayerIsGuarding() && OpponentIsIdle())));
        if (exchangeBool)
        {
            Exchange();
        }

    }
    //public void updatePlayerAction(ActionManager.Action action)
    //{
    //    _playerControl.SetNextAction(action);
    //    RepeatStringFunction();
    //    _playerControl.SetCurrentAction(action);
    //    UpdateUI();
    //}

    //private void UpdateUI() //TODO: Refactor into UI Manager
    //{
    //    uiText.text = (_playerControl.getNextAction().ToString()) + repeatCounterString;
    //}
    //}
    public void RepeatStringFunction()
    {
        if (_playerControl.getNextAction() == _playerControl.GetCurrentAction() && _playerControl.getNextAction().ToString() != "Guard")
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
        if (PlayerIsJabbing())
        {
            #region opponent jab
            if (OpponentIsJabbing())
            {
                if (_playerControl.GetCharge() > _opponent.GetCharge())
                {
                    PlayerWinsExchange();
                    return;
                }
                else
                {
                    OpponentWinsExchange();
                    return;
                }
            }
            #endregion
            if (OpponentIsFeinting() || PlayerIsIdle())
            {
                PlayerWinsExchange();
                return;
            }
            if (OpponentIsGuarding())
            {

                _opponentAnim.SetBool("Success", true);
                return;
            }
            if (OpponentIsSlipping())
            {
                OpponentWinsExchange();
                return;
            }

        }
        if (PlayerIsFeinting())
        {
            if (OpponentIsJabbing())
            {
                OpponentWinsExchange();
                return;
            }
            if (OpponentIsGuarding() || OpponentIsSlipping())
            {
                PlayerWinsExchange();
                return;
            }

        }
        if (PlayerIsGuarding())
        {
            if (OpponentIsJabbing())
            {
                _playerAnim.SetBool("Success", true);
                return;
            }
            if (OpponentIsFeinting())
            {
                OpponentWinsExchange();
                return;

            }

        }
        if (PlayerIsSlipping())
        {
            if (OpponentIsJabbing())
            {
                PlayerWinsExchange();
                return;
            }
            if (OpponentIsFeinting())
            {
                OpponentWinsExchange();
                return;
            }
        }
        if (PlayerIsIdle())
        {
            if (OpponentIsJabbing())
            {
                OpponentWinsExchange();
                return;
            }
        }
        return;
    }




    private void PlayerWinsExchange()
    {
        _playerAnim.SetBool("Success", true);
        _opponentAnim.SetBool("Fail", true);

    }
    private void OpponentWinsExchange()
    {
        _opponentAnim.SetBool("Success", true);
        _playerAnim.SetBool("Fail", true);

    }
    #region State Machine Getters
    #region Player
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool PlayerIsJabbing()
    {
        return _playerAnim.GetBool("Jab");
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool PlayerIsFeinting()
    {
        return _playerAnim.GetBool("Feint");
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool PlayerIsGuarding()
    {
        return _playerAnim.GetBool("Guard");
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool PlayerIsSlipping()
    {
        return _playerAnim.GetBool("Slip");
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool PlayerIsIdle()
    {
        return _playerAnim.GetBool("Idle");
    }
    #endregion
    #region Opponent 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool OpponentIsJabbing()
    {
        return _opponentAnim.GetBool("Jab");
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool OpponentIsFeinting()
    {
        return _opponentAnim.GetBool("Feint");
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool OpponentIsGuarding()
    {
        return _opponentAnim.GetBool("Guard");
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool OpponentIsSlipping()
    {
        return _opponentAnim.GetBool("Slip");
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool OpponentIsIdle()
    {
        return _opponentAnim.GetBool("Idle");
    }
    #endregion
    #endregion
}
