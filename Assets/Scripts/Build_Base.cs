using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_Base : MonoBehaviour
{
    public bool isCont;
    public int resCreate, resAmount;
    public float timer;
    public Main_Control scrControl;

    public int[] bldRes;
    public int[] bldAmount;

    public string strName, strDescr;

    void Start()
    {
        if(isCont)
        {
            InvokeRepeating("CycleRes", timer, timer);
        }
        else
        {
            scrControl.ManageRes(resCreate, resAmount);
        }
    }

    private void CycleRes()
    {
        scrControl.ManageRes(resCreate, resAmount);
    }

    public virtual void Destroy()
    {
        for(int i = 0; i < bldRes.Length; i++)
        {
            scrControl.ManageRes(bldRes[i], bldAmount[i]/2);
        }

        Destroy(gameObject);
    }
}
