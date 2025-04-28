using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class Controls : Fighter
{

    
    [SerializeField] private Animator _anim;
    private bool guardBool;
    //ActionManager _actionManager = GameObject.FindGameObjectWithTag("ActionManager").GetComponent<ActionManager>();
    protected override void Start()
    {

        base.Start();
        _anim = GetComponentInChildren<Animator>();
                

    }


    // Update is called once per frame
    void Update()
    {
        guardBool = Input.GetKey(KeyCode.L) && !(Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Semicolon));
        
        #region Inputs
        
        if (guardBool)
        {
            _actionManager.updatePlayerAction(ActionManager.Action.Guard);
            _anim.SetBool("Guard", guardBool);
            _actionManager.Exchange();

        }
        else _anim.SetBool("Guard", guardBool);

        if (Input.GetKeyDown(KeyCode.J))
        {   
            _actionManager.updatePlayerAction(ActionManager.Action.Jab);
            _anim.SetBool("Jab", true);
            _actionManager.Exchange();
            
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            _actionManager.updatePlayerAction(ActionManager.Action.Feint);
            _anim.SetBool("Feint", true);
            _actionManager.Exchange();
            
        }
        
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            _actionManager.updatePlayerAction(ActionManager.Action.Slip);
            _anim.SetBool("Slip", true);
            _actionManager.Exchange();

        }
        #endregion
    }



  

}
