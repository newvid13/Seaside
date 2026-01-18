using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Control_Build : MonoBehaviour
{
    private Main_Control scrMain;

    [SerializeField] private int selectedBld;
    [SerializeField] private Transform[] bldGhost;
    private Build_Ghost scrGhost;
    [SerializeField] private GameObject[] bldPrefab;
    private Build_Base scrPrefab;

    [SerializeField] private UI_Group scrGroup, clickGroup;

    [SerializeField] private bool isBuilding;
    private RaycastHit Hit;
    public LayerMask bldLayer;
    private Build_Base scrBuilding;

    [SerializeField] private TextMeshProUGUI[] txtClick;
    [SerializeField] private string[] resNames;

    void Start()
    {
        scrMain = GetComponent<Main_Control>();
    }

    void Update()
    {
        if (isBuilding)
            return;

        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(myRay, out Hit, 200f, bldLayer, QueryTriggerInteraction.Ignore))
        {
            if(Hit.transform.GetComponent<Build_Base>())
            {
                if (Input.GetButtonUp("Fire1"))
                {
                    scrBuilding = Hit.transform.GetComponent<Build_Base>();
                    UIBld();
                }
            }
        }
    }

    public void StartBuild(int type)
    {
        isBuilding = true;
        selectedBld = type;
        StartCoroutine(waitActivate());
    }

    IEnumerator waitActivate()
    {
        yield return new WaitForSeconds(0.1f);
        CheckBuild();
    }

    private void CheckBuild()
    {
        bool canBuild = true;
        scrPrefab = bldPrefab[selectedBld].GetComponent<Build_Base>();

        for (int i = 0; i < scrPrefab.bldRes.Length; i++)
        {
            int typeRes = scrPrefab.bldRes[i];
            int amtRes = scrPrefab.bldAmount[i];

            if (scrMain.resources[typeRes] < amtRes)
                canBuild = false;
        }

        if(canBuild)
        {
            scrMain.PlayAudio(0);
            scrGroup.Stim();
            scrGhost = bldGhost[selectedBld].GetComponent<Build_Ghost>();
            scrGhost.Activate();
        }
        else
        {
            scrMain.PlayAudio(1);
            isBuilding = false;
        }
    }

    public void Build(Vector3 pos, Quaternion rot)
    {
        scrPrefab = bldPrefab[selectedBld].GetComponent<Build_Base>();

        for (int i = 0; i < scrPrefab.bldRes.Length; i++)
        {
            int typeRes = scrPrefab.bldRes[i];
            int amtRes = scrPrefab.bldAmount[i];
            scrMain.ManageRes(typeRes, -amtRes);
        }

        GameObject newB = Instantiate(bldPrefab[selectedBld], pos, rot);
        Build_Base scrNew = newB.transform.GetComponent<Build_Base>();
        scrNew.scrControl = scrMain;

        scrMain.PlayAudio(2);
        StartCoroutine(waitCanclick());
    }

    IEnumerator waitCanclick()
    {
        yield return new WaitForSeconds(0.1f);
        isBuilding = false;
    }

    public void StopBuild()
    {
        isBuilding = false;

        foreach (Transform gh in bldGhost)
        {
            scrGhost = gh.GetComponent<Build_Ghost>();
            scrGhost.Deactivate();
        }
    }

    private void UIBld()
    {
        string returnRes = "ADDS";
        for (int i = 0; i < scrBuilding.bldRes.Length; i++)
        {
            int tempAmt = scrBuilding.bldAmount[i] / 2;
            tempAmt = Mathf.Clamp(tempAmt, 1, 20);
            returnRes += " " + tempAmt.ToString() + " " + resNames[scrBuilding.bldRes[i]];
        }

        txtClick[0].text = scrBuilding.strName;
        txtClick[1].text = scrBuilding.strDescr;
        txtClick[2].text = returnRes;
        clickGroup.Stim();

        scrMain.PlayAudio(0);
    }

    public void DestroyBld()
    {
        scrBuilding.Destroy();
        clickGroup.Stim();

        scrMain.PlayAudio(2);
    }
}
