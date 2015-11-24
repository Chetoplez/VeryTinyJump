using UnityEngine;


public static class Bezier {

    /* B(t) =(1 - t)2 P0 + 2 (1 - t) t P1 + t2 P2 */
    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            oneMinusT * oneMinusT * p0 +
            2f * oneMinusT * t * p1 +
            t * t * p2;
    }

    /* B'(t) = 2 (1 - t) (P1 - P0) + 2 t (P2 - P1).  first derivative for bezier */
    public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        return
            2f * (1f - t) * (p1 - p0) +
            2f * t * (p2 - p1);
    }

}
