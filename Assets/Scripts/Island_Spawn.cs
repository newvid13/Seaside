using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island_Spawn : MonoBehaviour
{
    public GameObject prefabIsland;
    private Island_Control scrIsland;
    public int Stage, numIslands;

    [SerializeField] private int typeWeather, typeRes;

    private Vector3 objPos;

    private bool hadLand = false, hadLumber = false, hadStone = false;

    void Start()
    {
        for(int i = 0; i < numIslands; i++)
        {
            objPos = transform.position;
            objPos.y = objPos.y - (500 * i);
            Stage = i;
            RanSpawn();

            GameObject objPrefab = Instantiate(prefabIsland, objPos, transform.rotation, transform);
            scrIsland = objPrefab.GetComponent<Island_Control>();
            scrIsland.Setup(typeWeather, typeRes, Stage);
        }
    }

    private void RanSpawn()
    {
        if (Stage > 16)
        {
            typeRes = Random.Range(0, 5);
            typeWeather = Random.Range(0, 9);
        }
        else if (Stage > 12)
        {
            typeRes = Random.Range(0, 4);
            typeWeather = Random.Range(0, 9);
        }
        else if (Stage > 2)
        {
            typeRes = Random.Range(0, 4);
            typeWeather = Random.Range(0, 4);
            if(typeWeather == 3)
            {
                typeWeather = 0;
            }

            SafetyNet();
            CheckBools();
        }
        else if (Stage == 1)
        {
            typeWeather = 2;
            typeRes = 0;
        }
        else
        {
            typeWeather = 0;
            typeRes = 0;
        }
    }

    private void SafetyNet()
    {
        if (Stage == 8)
        {
            if (!hadLand)
                typeRes = 1;
        }
        else if (Stage == 9)
        {
            if (!hadLumber)
                typeRes = 2;
        }
        else if (Stage == 11)
        {
            if (!hadLumber)
                typeRes = 3;
        }
    }

    private void CheckBools()
    {
        if (typeRes == 1)
            hadLand = true;

        if (typeRes == 2)
            hadLumber = true;

        if (typeRes == 3)
            hadStone = true;
    }

}
