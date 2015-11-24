using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(FruitCurve))]
public class FruitCurveEditor : Editor
{

    private FruitCurve curve;
    private Transform handleTransform;
    private Quaternion handleRotation;

    private const int lineSteps = 10;

    private void OnSceneGUI()
    {
        curve = target as FruitCurve;
        handleTransform = curve.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

        Vector3 p0 = ShowPoint(0);
        Vector3 p1 = ShowPoint(1);
        Vector3 p2 = ShowPoint(2);

        Fruit f1 = curve.Fruit_2;
        f1.transform.position = p0;

        Fruit f2 = curve.Fruit_3;
        f2.transform.position = p1;

        Fruit f3 = curve.Fruit_4;
        f3.transform.position = p2;

        Handles.color = Color.gray;
        Handles.DrawLine(p0, p1);
        Handles.DrawLine(p1, p2);


        Handles.color = Color.white;
        Vector3 lineStart = curve.GetPoint(0f);
        for (int i = 1; i <= lineSteps; i++)
        {
            Vector3 lineEnd = curve.GetPoint(i / (float)lineSteps);
          
            Handles.DrawLine(lineStart, lineEnd);
            lineStart = lineEnd;
        }

    }

    private Vector3 ShowPoint(int index)
    {
        Vector3 point = handleTransform.TransformPoint(curve.points[index]);
        EditorGUI.BeginChangeCheck();
        point = Handles.DoPositionHandle(point, handleRotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(curve, "Move Point");
            EditorUtility.SetDirty(curve);
            curve.points[index] = handleTransform.InverseTransformPoint(point);
        }
        return point;
    }



}
