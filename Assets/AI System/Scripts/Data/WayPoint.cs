using UnityEngine;

[System.Serializable]
public class WayPoint
{
    [System.Serializable]
    public struct WayPointData
    {
        public Vector3 Position;
    }
    
    public WayPointData[] WayPoints;
    public int WayPointIndex;
    
    public WayPointData CurrentWayPoint;
    
    public Vector3 NextWayPoint()
    {
        if (WayPointIndex >= WayPoints.Length)
            WayPointIndex = 0;

        CurrentWayPoint = WayPoints[WayPointIndex];
        WayPointIndex++;

        return CurrentWayPoint.Position;
    }
    
    public void GizmosDraw(Transform transform)
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.25f);
        
        Gizmos.color = Color.red;

        foreach (var way in WayPoints) 
            Gizmos.DrawSphere(way.Position, 0.25f);
        
    }
}