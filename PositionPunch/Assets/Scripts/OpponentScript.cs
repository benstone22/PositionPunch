using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentScript : Fighter
{
    // Start is called before the first frame update
    List<List<ActionManager.Action>> combos;
    
    void Start()
    {
        SetCurrentAction(ActionManager.Action.None);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
