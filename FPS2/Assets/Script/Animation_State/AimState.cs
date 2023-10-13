using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimState : AimBase
{
    public override void EnterState(AimManager aim)
    {
        aim.anim.SetBool("Aim", true);
        aim.CurFov = aim.AdsFov;
    }

    public override void UpdateState(AimManager aim)
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            aim.SwitchState(aim.Hip);
        }
    }
}
