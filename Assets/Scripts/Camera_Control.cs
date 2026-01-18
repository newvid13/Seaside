using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Camera_Control : MonoBehaviour
{
    [SerializeField] private Vector3 islandPos, lakePos;
    [SerializeField] private float zoomAmount, speedIn, speedOut, timeWoosh;
    [SerializeField] private Ease myCurve, speedCurve;

    public bool isZooming;
    private int Stage = 0;
    private Camera cam;

    public Transform objMain;
    private Main_Control scrMain;
    public Transform objSpawn;
    private Island_Spawn scrSpawn;

    void Start()
    {
        cam = GetComponent<Camera>();
        scrMain = objMain.GetComponent<Main_Control>();
        scrSpawn = objSpawn.GetComponent<Island_Spawn>();
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            cam.fieldOfView -= 2;
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 30f, 70f);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            cam.fieldOfView += 2;
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 30f, 70f);
        }
    }

    public void ZoomIn()
    {
        if (isZooming || Stage > scrSpawn.numIslands-1)
            return;

        if(scrMain.resources[5] >= (Stage+1)*5)
        {
            isZooming = true;
            transform.DOMove(lakePos, 1f).SetEase(myCurve).OnComplete(ZoomIA);
        }
    }

    private void ZoomIA()
    {
        StartCoroutine(waitWoosh());
        lakePos.y -= zoomAmount;
        transform.DOMove(lakePos, speedIn).SetEase(myCurve).OnComplete(ZoomIB);
    }

    private void ZoomIB()
    {
        Stage++;
        scrMain.StageUpdate(Stage);
        islandPos.y = lakePos.y;
        transform.DOMove(islandPos, 1f).SetEase(myCurve).OnComplete(ZoomComplete);
    }

    public void ZoomOut()
    {
        if (isZooming)
            return;

        if(Stage > 0)
        {
            isZooming = true;
            transform.DOMove(lakePos, 1f).SetEase(myCurve).OnComplete(ZoomOA);
        }
    }

    private void ZoomOA()
    {
        lakePos.y += zoomAmount;
        transform.DOMove(lakePos, speedOut).SetEase(myCurve).OnComplete(ZoomOB);
    }

    private void ZoomOB()
    {
        Stage--;
        scrMain.StageUpdate(Stage);
        islandPos.y = lakePos.y;
        transform.DOMove(islandPos, 1f).SetEase(myCurve).OnComplete(ZoomComplete);
    }

    private void ZoomComplete()
    {
        isZooming = false;
    }

    IEnumerator waitWoosh()
    {
        yield return new WaitForSeconds(timeWoosh);
        scrMain.PlayAudio(3);
    }

    public void ZoomOutAll()
    {
        if (isZooming)
            return;

        if (Stage > 0)
        {
            isZooming = true;
            transform.DOMove(lakePos, 1f).SetEase(myCurve).OnComplete(ZoomOutA);
        }
    }

    private void ZoomOutA()
    {
        lakePos.y += zoomAmount*Stage;
        transform.DOMove(lakePos, (speedOut*Stage)/3).SetEase(speedCurve).OnComplete(ZoomOutB);
    }

    private void ZoomOutB()
    {
        Stage = 0;
        scrMain.StageUpdate(Stage);
        islandPos.y = lakePos.y;
        transform.DOMove(islandPos, 1f).SetEase(myCurve).OnComplete(ZoomComplete);
    }

    public void ZoomInAll()
    {
        if (isZooming)
            return;

        if (scrMain.resources[5] >= (Stage + 1) * 5)
        {
            isZooming = true;
            transform.DOMove(lakePos, 1f).SetEase(myCurve).OnComplete(ZoomInAllA);
        }
    }

    private void ZoomInAllA()
    {
        int numZoom = (scrMain.resources[5] / 5) - Stage;

        lakePos.y -= zoomAmount * numZoom;
        transform.DOMove(lakePos, (speedIn * numZoom) / 3).SetEase(speedCurve).OnComplete(ZoomInAllB);
    }

    private void ZoomInAllB()
    {
        Stage = scrMain.resources[5] / 5;
        scrMain.StageUpdate(Stage);
        islandPos.y = lakePos.y;
        transform.DOMove(islandPos, 1f).SetEase(myCurve).OnComplete(ZoomComplete);
    }
}
