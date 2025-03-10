using UnityEngine;
using System.Collections;
using Microsoft.MixedReality.Toolkit;

public class SequentialAnimation : MonoBehaviour
{
    //public GameObject elbowScrews;
    //public GameObject elbowAssembly;
    public GameObject topScrews;
    public GameObject separationPlate;
    public GameObject Pin;

    public GameObject ResetButton;
    public float RotationValue = 135f;

    //private Vector3 _elbowScrewsStartPos;
    //private Vector3 _elbowAssemblyStartPos;
    private Vector3 _topScrewsStartPos;
    private Vector3 _pinStartPos;
    private Quaternion _separationPlateStartRot;

    private bool isAnimating = false;

    private Coroutine _sequence;

    void Start()
    {
        //_elbowScrewsStartPos = elbowScrews.transform.localPosition;
        //_elbowAssemblyStartPos = elbowAssembly.transform.localPosition;
        _topScrewsStartPos = topScrews.transform.localPosition;
        _separationPlateStartRot = separationPlate.transform.localRotation;
        _pinStartPos = Pin.transform.localPosition;
    }

    public void PlaySequence()
    {
        if (!isAnimating)
            _sequence = StartCoroutine(AnimateSequence());
    }

    IEnumerator AnimateSequence()
    {
        isAnimating = true;

        yield return MoveAndDisappear(Pin, new Vector3(-0.381f, 0f, 4.46f), 3f);

        //yield return MoveAndDisappear(elbowScrews, new Vector3(0.7f, 0f, 0f), 2f);

        //yield return MoveAndDisappear(elbowAssembly, new Vector3(0f, 2.5f, 0f), 3f);

        yield return MoveAndDisappear(topScrews, new Vector3(0f, 0f, 0.8f), 3f);


        yield return RotateObject(separationPlate, new Vector3(0, 0, RotationValue), 4f);

        ResetButton.SetActive(true);

        isAnimating = false;
    }

    IEnumerator MoveAndDisappear(GameObject obj, Vector3 moveOffset, float duration)
    {
        Vector3 startPos = obj.transform.localPosition;
        Vector3 targetPos = startPos + moveOffset;

        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            obj.transform.localPosition = Vector3.Lerp(startPos, targetPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.localPosition = targetPos;
        obj.SetActive(false);
    }

    IEnumerator RotateObject(GameObject obj, Vector3 rotationAngles, float duration)
    {
        Quaternion startRot = obj.transform.rotation;
        Quaternion targetRot = Quaternion.Euler(startRot.eulerAngles + rotationAngles);

        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            obj.transform.rotation = Quaternion.Slerp(startRot, targetRot, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.rotation = targetRot;
    }

    public void ResetSequence()
    {
        //if (_sequence == null)
        //    return;

        //StopCoroutine(_sequence);
        //elbowScrews.transform.localPosition = _elbowScrewsStartPos;
        //elbowAssembly.transform.localPosition = _elbowAssemblyStartPos;
        topScrews.transform.localPosition = _topScrewsStartPos;
        Pin.transform.localPosition = _pinStartPos;
        separationPlate.transform.localRotation = _separationPlateStartRot;


        //elbowScrews.SetActive(true);
        //elbowAssembly.SetActive(true);
        topScrews.SetActive(true);
        Pin.SetActive(true);
    }
}
