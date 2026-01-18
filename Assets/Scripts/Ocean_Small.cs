using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean_Small : MonoBehaviour
{
    private Material myMat;
    [SerializeField] private Transform cam;
    private Camera_Control scrCam;

    [SerializeField] private float dist;

    void Start()
    {
        cam = Camera.main.transform;
        myMat = GetComponent<Renderer>().material;
        scrCam = cam.GetComponent<Camera_Control>();
    }

    void LateUpdate()
    {
        if(scrCam.isZooming)
        {
            dist = cam.position.y - transform.position.y;
            dist = dist / 30;
            dist = Mathf.Clamp(dist, 0f, 1f);
            myMat.SetFloat("_Alpha", dist);
        }
    }
}
