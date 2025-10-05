using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(CatmullRomSpline))]
public class CatmullRomSplineEditor : Editor
{
    public void OnSceneGUI()
    {
        CatmullRomSpline spline = (CatmullRomSpline)target;
        Transform t = spline.transform;
        List<Vector2> points = spline.ControlPoints;

        if (points.Count == 0) return;

        EditorGUI.BeginChangeCheck();
        for (int i = 0; i < points.Count; i++)
        {
            Vector3 worldPoint = t.position + new Vector3(points[i].x, 0, points[i].y);
            Vector3 newWorldPoint = Handles.PositionHandle(worldPoint, Quaternion.identity);
            if (worldPoint != newWorldPoint)
            {
                points[i] = new Vector2(newWorldPoint.x - t.position.x, newWorldPoint.z - t.position.z);
            }
        }
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(spline, "Move Spline Control Point");
            spline.SetSpline(points);
            EditorUtility.SetDirty(spline);
        }
        
    }
}
#endif
public class CatmullRomSpline : MonoBehaviour
{
    [SerializeField] private List<Vector2> controlPoints;
    [SerializeField] private Color splineColor = Color.black;
    [SerializeField][Range(0.01f, 1f)] private float alpha = 0.5f;
    [SerializeField] private int gizmoResolution = 10;
    public List<Vector2> ControlPoints => controlPoints;
    public void SetSpline(List<Vector2> points)
    {
        controlPoints = points;
    }
    public Vector2 GetPoint(float t)
    {
        // knots
        List<float> k = new List<float> { 0f };
        for (int i = 1; i < controlPoints.Count; i++)
        {
            k.Add(k[i - 1] + GetT(controlPoints[i - 1], controlPoints[i]));
        }

        // evaluate the point
        float u = Mathf.LerpUnclamped(k[1], k[k.Count - 2], t);
        return RemapRecursive(k[0], k[k.Count - 1], controlPoints.ToArray(), u);
    }

    public Vector3 GetWorldPoint(float t)
    {
        Vector2 localPoint = GetPoint(t);
        return transform.position + new Vector3(localPoint.x, 0, localPoint.y);
    }

    float GetT(Vector2 a, Vector2 b)
    {
        return Mathf.Pow(Vector2.SqrMagnitude(a - b), alpha * 0.5f);
    }

    static Vector2 RemapRecursive(float a, float b, Vector2[] p, float u)
    {
        for (int r = 1; r < p.Length; r++)
        {
            for (int i = 0; i < p.Length - r; i++)
            {
                p[i] = Remap(a, b, p[i], p[i + 1], u);
            }
        }
        return p[0];
    }
    static Vector2 Remap(float a, float b, Vector2 p0, Vector2 p1, float u)
    {
        return Vector2.LerpUnclamped(p0, p1, (u - a) / (b - a));
    }
    private void OnDrawGizmos()
    {
        if (controlPoints.Count < 2) return;

        Gizmos.color = splineColor;
        Vector3 prevPoint = GetWorldPoint(0f);
        for (int i = 1; i <= gizmoResolution; i++)
        {
            float t = i / (float)gizmoResolution;
            Vector3 point = GetWorldPoint(t);
            Gizmos.DrawLine(prevPoint, point);
            prevPoint = point;
        }

        // Draw direction indicators
        Vector3 p0 = GetWorldPoint(0f);
        Vector3 p1 = GetWorldPoint(0.01f);
        Vector3 d = p0 - p1;
        Gizmos.DrawLine(p0, p0 + d + new Vector3(-d.z, 0, d.x));
        Gizmos.DrawLine(p0, p0 + d + new Vector3(d.z, 0, -d.x));

        p0 = GetWorldPoint(1f);
        p1 = GetWorldPoint(0.99f);
        d = p1 - p0;
        Gizmos.DrawLine(p0, p0 + d + new Vector3(-d.z, 0, d.x));
        Gizmos.DrawLine(p0, p0 + d + new Vector3(d.z, 0, -d.x));
    }
    private void OnDrawGizmosSelected()
    {
        if (controlPoints.Count < 2) return;

        Vector3 localPos = transform.position;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < controlPoints.Count - 1; i++)
        {
            Gizmos.DrawLine(localPos + new Vector3(controlPoints[i].x, 0, controlPoints[i].y), localPos + new Vector3(controlPoints[i + 1].x, 0, controlPoints[i + 1].y));
        }

        Gizmos.color = new Color(1, 0.5f, 0);
        foreach (var p in controlPoints)
        {
            Gizmos.DrawSphere(localPos + new Vector3(p.x, 0, p.y), 0.05f);
        }
    }
}
