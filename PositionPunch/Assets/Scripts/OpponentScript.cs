using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpponentScript : Fighter
{


    public float timer;
    public float TimeToMove;
    private int moveNumber;
    protected override void Start()
    {
        base.Start();
        timer = TimeToMove;
        moveNumber = PickMove();
    }

    // Update is called once per frame
    private void Update()
    {
        base.MoveTime();
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = TimeToMove;
            anim.SetBool("Jab",false);
            anim.SetBool("Feint",false );
            anim.SetBool("Guard",false);
            anim.SetBool("Slip",false) ;
            moveNumber = PickMove();
            Move();
            
        }
    }
    private void Move()
    {
        
        if (moveNumber == 0)
        {
            Jab();
        }
        if (moveNumber == 1)
        {
            Feint();
        }
        if (moveNumber == 2)
        {
            Guard(true);
        }
        if (moveNumber == 3)
        {
            Slip();
        }
    }

    private static int PickMove()
    {
        return Random.Range(0, 3);
    }
}
