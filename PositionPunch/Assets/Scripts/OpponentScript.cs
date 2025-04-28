using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpponentScript : Fighter
{

    [SerializeField] private Animator _anim;
    public float timer;
    protected override void Start()
    {
        base.Start();
        timer = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        timer-=Time.deltaTime;
        if (timer < 0)
        {
            timer = 5;
            _anim.SetBool("Jab", true);
            _actionManager.Exchange();
        }
    }
    
}
