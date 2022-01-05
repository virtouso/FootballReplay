using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IArrow
{
    [SerializeField] private EasyCurvedLine.CurvedLineRenderer _curve;
    [SerializeField] private Transform _head;
    [SerializeField] private float _keepAliveTime;

    public void Show(Vector3 start, Vector3 end)
    {
        gameObject.SetActive(true);
        _curve.LinePoints[0].transform.position = start;
        _head.position = start;
        Vector3 direction = (start - end).normalized;
        _head.rotation = Quaternion.LookRotation(direction, Vector3.up);
        _curve.LinePoints[1].transform.position = end;
        StartCoroutine(Hide());
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(_keepAliveTime);
        gameObject.SetActive(false);
    }
}