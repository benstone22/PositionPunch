using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class Controls : Fighter
{


    //[SerializeField] private Animator _anim;
    private bool guardBool;
    
    
    protected override void Start()
    {

        base.Start();
        //_anim = GetComponentInChildren<Animator>();


    }


    // Update is called once per frame
    void Update()
    {
        base.MoveTime();
        guardBool = Input.GetKey(KeyCode.L) && !(Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Semicolon));

        #region Inputs


        if (guardBool)
        {

            //_actionManager.updatePlayerAction(ActionManager.Action.Guard);

        }

        Guard(guardBool);
        if (Input.GetKeyDown(KeyCode.J))
        {
            Jab();
            
            //_actionManager.updatePlayerAction(ActionManager.Action.Jab);



        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            //_actionManager.updatePlayerAction(ActionManager.Action.Feint);
            Feint();
            

        }

        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            //_actionManager.updatePlayerAction(ActionManager.Action.Slip);
            Slip();
            

        }
        #endregion
    }





}
