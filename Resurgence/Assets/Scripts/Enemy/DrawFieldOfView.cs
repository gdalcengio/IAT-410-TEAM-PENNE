using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawFieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public float meshResolution;
    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "ViewMesh";
        viewMeshFilter.mesh = viewMesh;
    }

    void LateUpdate()
    {
        RenderFieldOfView();
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal) {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0);
    }

    void RenderFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;

        float prevX = this.gameObject.GetComponent<EnemyMovement>().getPrevX();
        float currX = this.gameObject.GetComponent<EnemyMovement>().getCurrX();

        List<Vector3> viewPoints = new List<Vector3>();
        for (int i=0; i<=stepCount; i++) {
            float angle = transform.eulerAngles.y - viewAngle/2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);
            viewPoints.Add(newViewCast.point);
            // if (prevX < currX) {
            //     Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle, true) * viewRadius, Color.yellow);
            // } else if (prevX > currX) {
            //     Debug.DrawLine(transform.position, transform.position - DirFromAngle(angle, true) * viewRadius, Color.yellow);
            // }
        }
        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount-2) * 3];

        vertices[0] = Vector3.zero;
        for (int i=0; i<vertexCount-1; i++) {
            vertices[i+1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount-2) {
                triangles[i*3] = 0;
                triangles[i*3+1] = i+1;
                triangles[i*3+2] = i+2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    ViewCastInfo ViewCast(float globalAngle) {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        // float prevX = this.gameObject.GetComponent<EnemyMovement>().getPrevX();
        // float currX = this.gameObject.GetComponent<EnemyMovement>().getCurrX();

        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask)) {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        
        // if (prevX > currX) 
        return new ViewCastInfo(false, transform.position - dir*viewRadius, viewRadius, globalAngle);
        // else return new ViewCastInfo(false, transform.position + dir*viewRadius, viewRadius, globalAngle);
    }

    public struct ViewCastInfo {
        public bool hit;
        public Vector3 point; 
        public float dist;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dist, float _angle) {
            hit = _hit;
            point = _point;
            dist = _dist;
            angle = _angle;
        }
    }
}
