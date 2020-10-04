using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    public Transform VertexHolder;
    private Direction currentDir = Direction.NONE;
    private Direction nextDir = Direction.NONE;
    private Vertex currentVertex;
    private bool isMoving = false;
    private bool stopped = true;
    private float speed = 3f;
    private int inventory = -1;

    private TrainPickUpDrop pickupdrop;
    
    private void Start()
    {
        pickupdrop = GetComponent<TrainPickUpDrop>();


        currentVertex = VertexHolder.GetChild(0).GetComponent<Vertex>();

        // Set up vertex neighbours
        for (int i = 0; i < VertexHolder.childCount; i++)
        {
            Vertex vert = VertexHolder.GetChild(i).GetComponent<Vertex>();
            Vector2 vertexPos = VertexHolder.GetChild(i).position;
            for (int j = 0; j < VertexHolder.childCount; j++)
            {
                if (i == j)
                    continue;

                Transform otherVecTR = VertexHolder.GetChild(j);
                Vector2 otherVertPos = otherVecTR.position;
                if (vertexPos + Vector2.up == otherVertPos)
                {
                    vert.neighbours.Add(otherVecTR.GetComponent<Vertex>());
                    vert.dirToNeighbours.Add(Direction.UP);
                }
                else if (vertexPos + Vector2.down == otherVertPos)
                {
                    vert.neighbours.Add(otherVecTR.GetComponent<Vertex>());
                    vert.dirToNeighbours.Add(Direction.DOWN);
                }
                else if (vertexPos + Vector2.left == otherVertPos)
                {
                    vert.neighbours.Add(otherVecTR.GetComponent<Vertex>());
                    vert.dirToNeighbours.Add(Direction.LEFT);
                }
                else if (vertexPos + Vector2.right == otherVertPos)
                {
                    vert.neighbours.Add(otherVecTR.GetComponent<Vertex>());
                    vert.dirToNeighbours.Add(Direction.RIGHT);
                }
            }
        }
    }

    private void GoToDir(Direction dir)
    {
        bool found = false;
        // Search for nextDir
        for (int i = 0; i < currentVertex.neighbours.Count; i++)
        {
            if (nextDir == currentVertex.dirToNeighbours[i])
            {
                found = true;
                stopped = false;
                Vertex newVertex = currentVertex.neighbours[i];
                StartCoroutine(MoveToVertexIE(newVertex));

                currentDir = nextDir;
                nextDir = Direction.NONE;
                break;
            }
        }
        if (!found)
        {
            bool found2 = false;
            for (int i = 0; i < currentVertex.neighbours.Count; i++)
            {
                if (dir == currentVertex.dirToNeighbours[i])
                {
                    found2 = true;
                    stopped = false;
                    Vertex newVertex = currentVertex.neighbours[i];
                    StartCoroutine(MoveToVertexIE(newVertex));
                    break;
                }
            }

            if (!found2)
            {
                currentDir = nextDir;
                nextDir = Direction.NONE;
                if (currentDir == Direction.NONE)
                    stopped = true;
            }
        }
    }

    private IEnumerator MoveToVertexIE(Vertex newVertex)
    {
        isMoving = true;
        Vector2 newPos = newVertex.transform.position;
        Vector2 offset = newPos - (Vector2)transform.position;
        float dist = -1;
        float prevDist;
        while ((Vector2)transform.position != newPos)
        {
            transform.position += (Vector3)offset * speed * Time.deltaTime;
            prevDist = dist;
            dist = Vector2.Distance(transform.position, newPos);
            if (dist - prevDist > 0f && prevDist != -1)
                transform.position = newPos;
            yield return null;
        }
        transform.position = newPos;
        currentVertex = newVertex;
        isMoving = false;

        pickupdrop.PickUpDrop();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (stopped)
                currentDir = Direction.UP;
            else
                nextDir = Direction.UP;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (stopped)
                currentDir = Direction.DOWN;
            else
                nextDir = Direction.DOWN;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (stopped)
                currentDir = Direction.LEFT;
            else
                nextDir = Direction.LEFT;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (stopped)
                currentDir = Direction.RIGHT;
            else
                nextDir = Direction.RIGHT;
        }

        if (Utilities.DirToVec2(currentDir) + Utilities.DirToVec2(nextDir) == Vector2.zero)
            nextDir = Direction.NONE;

        if (isMoving)
            return;

        if (currentDir != Direction.NONE)
        {
            GoToDir(currentDir);
        }
    }
}
