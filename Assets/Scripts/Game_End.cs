using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Game_End : MonoBehaviour
{
    private Build_Base scrBase;
    private Main_Control scrControl;
    public Vector3 escape;

    void Start()
    {
        StartCoroutine(waitEnd());
    }

    IEnumerator waitEnd()
    {
        yield return new WaitForSeconds(1f);
        scrBase = GetComponent<Build_Base>();
        scrControl = scrBase.scrControl;
        scrControl.EndGame();

        yield return new WaitForSeconds(12f);
        transform.DOMove(escape, 30f).SetEase(Ease.InOutQuad);
    }
}
