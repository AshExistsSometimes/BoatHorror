using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    [SerializeField] private Transform[] _LinkTransforms;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        
    }

    private void LateUpdate()
    {
        _lineRenderer.positionCount = _LinkTransforms.Length;
        for (int i = 0; i < _LinkTransforms.Length; i++)
        {
            _lineRenderer.SetPosition(i, _LinkTransforms[i].position);
        }
    }
}
