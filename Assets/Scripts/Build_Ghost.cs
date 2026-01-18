using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_Ghost : MonoBehaviour
{
    [SerializeField] private bool isBuilding, canBuild, colCheck;
    [SerializeField] private Color colRed, colGreen;
    private Material myMat;

    private RaycastHit Hit;
    public LayerMask rayLayer, bldLayer, colLayer;
    private Vector3 newPos;

    public Transform objMain;
    private Control_Build scrBuild;

    public float radius;

    void Start()
    {
        myMat = GetComponent<Renderer>().material;
        scrBuild = objMain.GetComponent<Control_Build>();
    }

    public void Activate()
    {
        myMat.color = colRed;
        isBuilding = true;
    }

    void Update()
    {
        if(isBuilding)
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(myRay, out Hit, 200f, rayLayer, QueryTriggerInteraction.Ignore))
            {
                newPos = Hit.point;
                newPos.y += 1.5f;
                transform.position = newPos;
            }

            canBuild = Physics.CheckSphere(transform.position, 2, bldLayer);
            colCheck = Physics.CheckSphere(transform.position, radius, colLayer);
            if (canBuild && !colCheck)
            {
                myMat.color = colGreen;

                if (Input.GetButtonUp("Fire1"))
                {
                    Build();
                }
            }
            else
            {
                myMat.color = colRed;
            }

            if (Input.GetButtonUp("Fire2"))
            {
                scrBuild.StopBuild();
                //Deactivate();
            }
        }
    }

    private void Build()
    {
        Deactivate();
        scrBuild.Build(transform.position, transform.rotation);
    }

    public void Deactivate()
    {
        myMat.color = Color.clear;
        isBuilding = false;
    }
}
