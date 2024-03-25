using System.Collections;
using System.Collections.Generic;
using CodeLibraries;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float viewRadius;
    [Range(0, 360)] [SerializeField] private float viewAngle;
    [SerializeField] private float viewHeight;
    [SerializeField] private float viewSize;
    [SerializeField] private float meshResolution;

    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private float visibleTargetFadeDuration = 1f;

    public HashSet<VisibleTarget> VisibleTargets { get; private set; } = new();

    public float ViewHeight => viewHeight;

    public float ViewSize
    {
        get => viewSize;
        private set => viewSize = value;
    }

    public float ViewRadius
    {
        get => viewRadius;
        private set => viewRadius = value;
    }

    public float ViewAngle
    {
        get => viewAngle;
        private set => viewAngle = value;
    }

    private void Start()
    {
        StartCoroutine(FindTargetsWithDelay(0.2f));
    }

    private IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
            foreach (var visibleTarget in VisibleTargets)
            {
                visibleTarget.OnVisible(visibleTargetFadeDuration);
            }
        }
    }

    private void DrawFieldOfView()
    {
        var stepsCount = Mathf.Round(ViewAngle * meshResolution);
        var stepAngleSize = ViewAngle / stepsCount;
        for (int i = 0; i < stepsCount; i++)
        {
            var angle = transform.eulerAngles.y - viewAngle * 0.5f + stepAngleSize * i;
        }
    }

    private void FindVisibleTargets()
    {
        foreach (var visibleTarget in VisibleTargets)
        {
            visibleTarget.OnNotVisible();
        }
        VisibleTargets.Clear();
        const int maxVisibleTargets = 10;
        var targetsInView = new Collider[maxVisibleTargets];
        Physics.OverlapSphereNonAlloc(transform.position, ViewRadius, targetsInView, targetMask);

        foreach (var target in targetsInView)
        {
            if (target is null)
                return;

            var offset = Vector3.up * ViewHeight;
            var targetPos = target.transform.position + offset;
            var currentPos = transform.position + offset;
            var dirToTarget = (targetPos - currentPos).normalized;
            
            var distToTarget = Vector3.Distance(currentPos, targetPos);
            (bool rightEye, bool leftEye) eyesCantSee = (
                Physics.Raycast(currentPos + transform.right * ViewSize, dirToTarget, distToTarget, obstacleMask),
                Physics.Raycast(currentPos - transform.right * ViewSize, dirToTarget, distToTarget, obstacleMask));
            if (eyesCantSee is { rightEye: true, leftEye: true })
                continue;
            
            target.TryGetComponent<VisibleTarget>(out var visibleTarget);
            VisibleTargets.Add(visibleTarget);
        }
    }
}