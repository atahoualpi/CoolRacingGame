using UnityEngine;
using System.Collections;

public class testDist : MonoBehaviour {

    public BezierSpline bz;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(ClosestPointOnLine(new Vector3(0,0,-1), new Vector3(0, 0, 1), transform.position).m_Position);
	}
    ClosestPointInfo ClosestPointOnLine(Vector3 a_StartPoint, Vector3 a_EndPoint, Vector3 a_CheckPoint) {
        Vector3 m_TempVector1 = a_CheckPoint - a_StartPoint;
        Vector3 m_TempVector2 = (a_EndPoint - a_StartPoint).normalized;

        float m_Distance = (a_StartPoint - a_EndPoint).magnitude;
        float m_DotProduct = Vector3.Dot(m_TempVector2, m_TempVector1);

        if (m_DotProduct <= 0.0f) {
            ClosestPointInfo m_ClosestPoint;
            m_ClosestPoint.m_Position = a_StartPoint;

            return m_ClosestPoint;
        }

        if (m_DotProduct >= m_Distance) {
            ClosestPointInfo m_ClosestPoint;
            m_ClosestPoint.m_Position = a_EndPoint;

            return m_ClosestPoint;
        }

        Vector3 m_TempVector3 = m_TempVector2 * m_DotProduct;

        ClosestPointInfo m_ClosestPoint1;
        m_ClosestPoint1.m_Position = a_StartPoint + m_TempVector3;

        return m_ClosestPoint1;
    }
}
