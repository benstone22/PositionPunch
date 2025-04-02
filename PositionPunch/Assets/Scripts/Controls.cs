using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class Controls : Fighter
{

    //ActionManager _actionManager = GameObject.FindGameObjectWithTag("ActionManager").GetComponent<ActionManager>();
    private void Start()
    {

        _actionManager = GameObject.FindGameObjectWithTag("ActionManager").GetComponent<ActionManager>();
        SetCurrentAction(ActionManager.Action.None);

    }


    // Update is called once per frame
    void Update()
    {
        
        #region Inputs
        if(Input.GetKeyDown(KeyCode.J))
        {   
            _actionManager.updatePlayerAction(ActionManager.Action.Jab);
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            _actionManager.updatePlayerAction(ActionManager.Action.Feint);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            _actionManager.updatePlayerAction(ActionManager.Action.Guard);
        }
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            _actionManager.updatePlayerAction(ActionManager.Action.Slip);
        }
        #endregion
    }
    
    
   


}
