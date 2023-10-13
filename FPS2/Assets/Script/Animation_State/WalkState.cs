using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MoveBase
{
    public override void EnterState(Player_V2 movement)
    {
        movement.anima.SetBool("Walk", true);
    }

    public override void UpdateState(Player_V2 movement)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ExitState(movement, movement.run);
        }
        else if (movement.dir.magnitude < 0.1f)
        {
            ExitState(movement, movement.idle);
        }

        if (movement.inY < 0)
        {
            movement.spd = movement.walkbackspd;
        }
        else
        {
            movement.spd = movement.walkspd;
        }
    }

    void ExitState(Player_V2 movement, MoveBase state)
    {
        movement.anima.SetBool("Walk", false);
        movement.SwitchState(state);
    }
}