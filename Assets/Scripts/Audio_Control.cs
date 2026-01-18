using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Control : MonoBehaviour
{
    [SerializeField] private AudioClip[] clipWind;
    [SerializeField] private AudioClip[] clipSea;
    [SerializeField] private AudioClip[] clipThunder;

    [SerializeField] private AudioSource audRain;
    [SerializeField] private AudioSource audSea;
    [SerializeField] private AudioSource audWind;

    [SerializeField] private AudioSource audThunder;
    [SerializeField] private int type, minThun, maxThun;


    void Start()
    {
        audSea.clip = clipSea[type];
        audSea.Play();
        audWind.clip = clipWind[type];
        audWind.Play();

        switch(type)
        {
            case 0:
                break;
            case 1:
                audRain.Play();
                StartCoroutine(waitThunder());
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }

    IEnumerator waitThunder()
    {
        int time = Random.Range(minThun, maxThun);
        yield return new WaitForSeconds(time);

        audThunder.PlayOneShot(clipThunder[Random.Range(0, clipThunder.Length)]);
        StartCoroutine(waitThunder());
    }
}
