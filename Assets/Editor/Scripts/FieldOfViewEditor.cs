using CodeLibraries;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        var fow = (FieldOfView)target;
        if (!fow.enabled)
        {
            return;
        }

        Handles.color = Color.white;
        var fowPosition = fow.transform.position;
        Handles.DrawWireArc(fowPosition, Vector3.up, Vector3.forward, 360, fow.ViewRadius);

        // adding eulerAngles because it is not global angle passed 
        var eulerAngles = fow.transform.eulerAngles;
        var viewAngleA = Utils.GetVector3FromAngle(-fow.ViewAngle * 0.5f + eulerAngles.y);
        var viewAngleB = Utils.GetVector3FromAngle(fow.ViewAngle * 0.5f + eulerAngles.y);

        Handles.DrawLine(fowPosition, fowPosition + viewAngleA * fow.ViewRadius, 1);
        Handles.DrawLine(fowPosition, fowPosition + viewAngleB * fow.ViewRadius, 1);


        Handles.color = Color.red;
        var offset = fow.ViewHeight * Vector3.up;
        foreach (var visibleTarget in fow.VisibleTargets)
        {
            Handles.DrawLine(fowPosition + offset + fow.transform.right * fow.ViewSize,
                visibleTarget.transform.position + offset);
            Handles.DrawLine(fowPosition + offset - fow.transform.right * fow.ViewSize,
                visibleTarget.transform.position + offset);
        }
    }
}