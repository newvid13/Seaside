using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Main_Control : MonoBehaviour
{
    public TextMeshProUGUI txtPeopleZoom;

    public int[] resources;
    public TextMeshProUGUI[] txtResources;

    public CanvasGroup grpEnd;

    private AudioSource myAudio;
    public AudioClip[] clipAudio;

    [SerializeField] private CanvasGroup grpTut;
    [SerializeField] private GameObject[] tut;
    [SerializeField] private int[] tutTimers;
    [SerializeField] private float tutTimer;

    [SerializeField] private CanvasGroup grpEscape;
    [SerializeField] private Image imgBlk;

    public AudioSource myMusic;
    public AudioClip[] clipMusic;

    private bool isSkipped = false, timerOn = false;
    private float timerT = 60f;
    public TextMeshProUGUI txtTime;

    void Start()
    {
        Time.timeScale = 1;
        myAudio = GetComponent<AudioSource>();

        Inital();
        StartCoroutine(waitStart());
    }

    private void Inital()
    {
        for(int i = 0; i < txtResources.Length; i++)
        {
            txtResources[i].text = resources[i].ToString();
        }

        txtResources[5].text = resources[5].ToString();
    }

    public void ManageRes(int type, int amount)
    {
        resources[type] += amount;
        txtResources[type].text = resources[type].ToString();
    }

    public void ManagePeople(int amount)
    {
        resources[5] += amount;
        txtResources[5].text = resources[5].ToString();
    }

    private void Cycle()
    {
        resources[0] -= resources[5];
        resources[1] -= resources[5];
        txtResources[0].text = resources[0].ToString();
        txtResources[1].text = resources[1].ToString();

        if (resources[0] < 0 || resources[1] < -30)
            Die();
    }

    private void Die()
    {
        Time.timeScale = 0;

        grpEnd.alpha = 1;
        grpEnd.interactable = true;
    }

    public void StageUpdate(int stage)
    {
        int fin = (stage+1) * 5;
        txtPeopleZoom.text = "> " + fin.ToString();
    }

    public void PlayAudio(int snd)
    {
        myAudio.PlayOneShot(clipAudio[snd]);
    }

    public void Restart()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(0);
    }

    IEnumerator waitStart()
    {
        yield return new WaitForSeconds(2f);
        imgBlk.DOColor(Color.clear, 2f);
        yield return new WaitForSeconds(3f);
        if (isSkipped == false)
        {
            grpTut.alpha = 1;
            grpTut.interactable = true;
            grpTut.blocksRaycasts = true;
        }

        yield return new WaitForSeconds(tutTimers[0]);
        tut[0].SetActive(false);
        tut[1].SetActive(true);
        yield return new WaitForSeconds(tutTimers[1]);
        tut[1].SetActive(false);
        tut[2].SetActive(true);
        yield return new WaitForSeconds(tutTimers[2]);
        tut[2].SetActive(false);
        tut[3].SetActive(true);
        yield return new WaitForSeconds(tutTimers[3]);
        tut[3].SetActive(false);
        tut[4].SetActive(true);
        yield return new WaitForSeconds(tutTimers[4]);

        if (isSkipped == false)
        {
            grpTut.alpha = 0;
            InvokeRepeating("Cycle", 60f, 60f);
            timerOn = true;
            myMusic.PlayOneShot(clipMusic[0], 0.2f);
        }
    }

    public void EndGame()
    {
        StartCoroutine(waitEnd());
    }

    IEnumerator waitEnd()
    {
        imgBlk.raycastTarget = true;
        CancelInvoke();
        yield return new WaitForSeconds(2f);
        grpEscape.alpha = 1;
        yield return new WaitForSeconds(tutTimer);
        grpTut.alpha = 0;
        myMusic.PlayOneShot(clipMusic[1], 1f);
        yield return new WaitForSeconds(15f);
        imgBlk.DOColor(Color.black, 2f);
    }

    public void Skip()
    {
        isSkipped = true;
        grpTut.alpha = 0;
        grpTut.interactable = false;
        grpTut.blocksRaycasts = false;
        InvokeRepeating("Cycle", 60f, 60f);
        timerOn = true;
        myMusic.PlayOneShot(clipMusic[0], 0.2f);
    }

    private void Update()
    {
        if(timerOn)
        {
            if (timerT > 0f)
            {
                timerT -= Time.deltaTime;
            }
            else
            {
                timerT = 60f;
            }

            txtTime.text = timerT.ToString("F0");
        }
    }
}
