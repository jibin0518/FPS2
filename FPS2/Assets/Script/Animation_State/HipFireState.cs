using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipFireState : AimBase
{
    public override void EnterState(AimManager aim)
    {
        aim.anim.SetBool("Aim", false);
        aim.CurFov = aim.HipFov;
    }

    public override void UpdateState(AimManager aim)
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            aim.SwitchState(aim.Aim);
        }
    }
}
