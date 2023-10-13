using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : MoveBase
{
    public override void EnterState(Player_V2 movement)
    {
        movement.anima.SetBool("Run", true);
    }

    public override void UpdateState(Player_V2 movement)
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ExitState(movement, movement.walk);
        }
        else if (movement.dir.magnitude < 0.1f)
        {
            ExitState(movement, movement.idle);
        }
        if (movement.inY < 0)
        {
            movement.spd = movement.runbackspd;
        }
        else
        {
            movement.spd = movement.runspd;
        }
    }

    void ExitState(Player_V2 movement, MoveBase state)
    {
        movement.anima.SetBool("Run", false);
        movement.SwitchState(state);
    }
}