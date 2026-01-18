using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Island_Control : MonoBehaviour
{
    [SerializeField] private VolumeProfile[] volProfiles;
    [SerializeField] private GameObject[] particles;
    [SerializeField] private GameObject[] resources;

    [SerializeField] private GameObject[] resourcesSpecial;

    private Volume myVol;

    void Start()
    {
        //
    }

    public void Setup(int typeWeather, int typeRes, int stg)
    {
        myVol = GetComponentInChildren<Volume>();

        myVol.profile = volProfiles[typeWeather];
        particles[typeWeather].SetActive(true);

        resources[typeRes].SetActive(true);

        if(typeWeather == 2)
        {
            resourcesSpecial[0].SetActive(true);
        }
        else if(stg == 0)
        {
            resourcesSpecial[1].SetActive(true);
        }
    }
}
