using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_Control : MonoBehaviour
{
    private Main_Control scrMain;

    [SerializeField] private Transform[] bldGhost;
    private Build_Ghost scrGhost;

    public int resA, Aamt, resB, Bamt;

    public CanvasGroup bldGroup;

    void Start()
    {
        scrMain = GetComponent<Main_Control>();
    }

    public void StimGroup()
    {
        scrMain.PlayAudio(0);

        if (bldGroup.alpha == 0)
        {
            bldGroup.alpha = 1;
            bldGroup.interactable = true;
        }
        else
        {
            bldGroup.alpha = 0;
            bldGroup.interactable = false;
        }
    }

    public void Build(int type)
    {
        switch(type)
        {
            case 0:
                //shelter
                if(scrMain.resources[2] > 2)
                {
                    resA = 2;
                    Aamt = 3;
                    Bamt = 0;

                    StimGroup();
                    scrGhost = bldGhost[type].GetComponent<Build_Ghost>();
                    scrGhost.Activate();
                }
                else
                {
                    scrMain.PlayAudio(1);
                }
                break;
            case 1:
                //rain
                if (scrMain.resources[3] > 1)
                {
                    resA = 3;
                    Aamt = 2;
                    Bamt = 0;

                    StimGroup();
                    scrGhost = bldGhost[type].GetComponent<Build_Ghost>();
                    scrGhost.Activate();
                }
                else
                {
                    scrMain.PlayAudio(1);
                }
                break;
            case 2:
                //farm
                if (scrMain.resources[2] > 1)
                {
                    resA = 2;
                    Aamt = 2;
                    Bamt = 0;

                    StimGroup();
                    scrGhost = bldGhost[type].GetComponent<Build_Ghost>();
                    scrGhost.Activate();
                }
                else
                {
                    scrMain.PlayAudio(1);
                }
                break;
            case 3:
                //mill
                if (scrMain.resources[2] > 1 && scrMain.resources[3] > 0)
                {
                    resA = 2;
                    Aamt = 2;
                    resB = 3;
                    Bamt = 1;

                    StimGroup();
                    scrGhost = bldGhost[type].GetComponent<Build_Ghost>();
                    scrGhost.Activate();
                }
                else
                {
                    scrMain.PlayAudio(1);
                }
                break;
            case 4:
                //quarry
                if (scrMain.resources[2] > 2)
                {
                    resA = 2;
                    Aamt = 3;
                    Bamt = 0;

                    StimGroup();
                    scrGhost = bldGhost[type].GetComponent<Build_Ghost>();
                    scrGhost.Activate();
                }
                else
                {
                    scrMain.PlayAudio(1);
                }
                break;
            case 5:
                //mine
                if (scrMain.resources[2] > 1 && scrMain.resources[3] > 4)
                {
                    resA = 2;
                    Aamt = 2;
                    resB = 3;
                    Bamt = 5;

                    StimGroup();
                    scrGhost = bldGhost[type].GetComponent<Build_Ghost>();
                    scrGhost.Activate();
                }
                else
                {
                    scrMain.PlayAudio(1);
                }
                break;
            case 6:
                //boat
                if (scrMain.resources[2] > 9 && scrMain.resources[4] > 9)
                {
                    resA = 2;
                    Aamt = 30;
                    resB = 4;
                    Bamt = 15;

                    StimGroup();
                    scrGhost = bldGhost[type].GetComponent<Build_Ghost>();
                    scrGhost.Activate();
                }
                else
                {
                    scrMain.PlayAudio(1);
                }
                break;
            default:
                break;
        }
    }

    public void Spend()
    {
        scrMain.ManageRes(resA, -Aamt);
        scrMain.ManageRes(resB, -Bamt);

        scrMain.PlayAudio(2);
    }

    public void Create(int Res, int Amt)
    {
        scrMain.ManageRes(Res, Amt);
    }

    public void StopBuild()
    {
        foreach(Transform gh in bldGhost)
        {
            scrGhost = gh.GetComponent<Build_Ghost>();
            scrGhost.Deactivate();
        }
    }

    public void EndRelay()
    {
        scrMain.EndGame();
    }
}
