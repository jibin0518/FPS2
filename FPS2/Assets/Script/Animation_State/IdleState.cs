using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MoveBase
{
    public override void EnterState(Player_V2 movement)
    {

    }

    public override void UpdateState(Player_V2 movement)
    {
        if(movement.dir.magnitude > 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                movement.SwitchState(movement.run);
            }
            else
            {
                movement.SwitchState(movement.walk);
            }
        }
    }
}
