using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_Shelter : Build_Base
{
    public override void Destroy()
    {
        scrControl.ManageRes(resCreate, -resAmount);

        for (int i = 0; i < bldRes.Length; i++)
        {
            scrControl.ManageRes(bldRes[i], bldAmount[i] / 2);
        }

        Destroy(gameObject);
    }
}
