using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class Controls : Fighter
{

    private Animator _anim;
    //ActionManager _actionManager = GameObject.FindGameObjectWithTag("ActionManager").GetComponent<ActionManager>();
    protected override void Start()
    {

        base.Start();
        _anim = GetComponentInChildren<Animator>();
                

    }


    // Update is called once per frame
    void Update()
    {
        _anim.SetBool("Guard", Input.GetKeyDown(KeyCode.L));
        #region Inputs
        if (Input.GetKeyDown(KeyCode.J))
        {   
            _actionManager.updatePlayerAction(ActionManager.Action.Jab);
            _actionManager.Exchange();
            _anim.SetTrigger("Jab");
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            _actionManager.updatePlayerAction(ActionManager.Action.Feint);
            _actionManager.Exchange();
            _anim.SetTrigger("Feint");
            

        }
        if (Input.GetKey(KeyCode.L))
        {
            _actionManager.updatePlayerAction(ActionManager.Action.Guard);
            _actionManager.Exchange();
            _anim.SetTrigger("Guard");
        }
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            _actionManager.updatePlayerAction(ActionManager.Action.Slip);
            _actionManager.Exchange();
            _anim.SetTrigger("Slip");
        }
        #endregion
    }



  

}
