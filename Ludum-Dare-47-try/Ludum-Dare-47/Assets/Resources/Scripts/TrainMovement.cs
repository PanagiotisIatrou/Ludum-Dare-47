using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    public Transform VertexHolder;
    private Wagon wagon;
    public Direction currentDir = Direction.NONE;
    private Direction prevDir = Direction.NONE;
    public Direction nextDir = Direction.NONE;
    public bool stopped = true;

    private Boost Boost;
    private TrainPickUpDrop pickupdrop;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Wagon") && col.gameObject != wagon.previousWagon.previousWagon.gameObject)
        {
            TrailDestroyer.Instance.GameOver();
        }
    }

    private void Start()
    {
        wagon = GetComponent<Wagon>();
        pickupdrop = GetComponent<TrainPickUpDrop>();
        Boost = GetComponent<Boost>();

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
        prevDir = currentDir;
        // Search for nextDir
        for (int i = 0; i < wagon.currentVertex.neighbours.Count; i++)
        {
            if (nextDir == wagon.currentVertex.dirToNeighbours[i])
            {
                found = true;
                stopped = false;
                Vertex newVertex = wagon.currentVertex.neighbours[i];
                wagon.MoveToVertex(newVertex);

                currentDir = nextDir;
                nextDir = Direction.NONE;
                break;
            }
        }
        if (!found)
        {
            bool found2 = false;
            for (int i = 0; i < wagon.currentVertex.neighbours.Count; i++)
            {
                if (dir == wagon.currentVertex.dirToNeighbours[i])
                {
                    found2 = true;
                    stopped = false;
                    Vertex newVertex = wagon.currentVertex.neighbours[i];
                    wagon.MoveToVertex(newVertex);
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (stopped)
            {
                if (prevDir != Direction.DOWN)
                    currentDir = Direction.UP;
            }
            else
                nextDir = Direction.UP;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (stopped)
            {
                if (prevDir != Direction.UP)
                    currentDir = Direction.DOWN;
            }
            else
                nextDir = Direction.DOWN;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (stopped)
            {
                if (prevDir != Direction.RIGHT)
                    currentDir = Direction.LEFT;
            }
            else
                nextDir = Direction.LEFT;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (stopped)
            {
                if (prevDir != Direction.LEFT)
                    currentDir = Direction.RIGHT;
            }
            else
                nextDir = Direction.RIGHT;
        }

        if (Utilities.DirToVec2(currentDir) + Utilities.DirToVec2(nextDir) == Vector2.zero)
            nextDir = Direction.NONE;

        if (wagon.isMoving)
            return;

        if (currentDir != Direction.NONE)
        {
            GoToDir(currentDir);
        }
    }
}
