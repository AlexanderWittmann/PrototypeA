using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{


    CapsuleCollider m_Collider;
    public Transform target;
    float speed = 4;
    Vector3[] path;
    int targetIndex;
    public PathRequestManager manager;
    Rigidbody rb;

    void Start()
    {
        m_Collider = GetComponent<CapsuleCollider>();
        m_Collider.radius = 1.3f;
        manager.RequestPath(transform.position, target.position, OnPathFound);
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        
        GameObject playerobj = GameObject.FindGameObjectWithTag("Player");
        bool move = playerobj.GetComponent<Player>().isMoving;

        if (move)
        {

            manager.RequestPath(transform.position, target.position, OnPathFound);
        }
        else
        {
            rb.velocity = Vector3.zero;
            manager.RequestPath(transform.position, transform.position, OnPathFound);
        }
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        GameObject playerobj = GameObject.FindGameObjectWithTag("Player");
        bool move = playerobj.GetComponent<Player>().isMoving;
        Vector3 currentWaypoint = path[0];
        while (move)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;

        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
