using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon : MonoBehaviour
{
    public Wagon frontWagon = null;
    public Wagon previousWagon = null;
    public Vertex currentVertex;
    private TrainPickUpDrop pickUp;
    private TrainMovement trainMovement;
    private Direction currentDir = Direction.DOWN;
    public bool isMoving = false;
    private float speed = 3f;
    public static float upgradespeed = 1f;
    public bool isFirst = false;

    private void Start()
    {
        if (isFirst)
        {
            pickUp = GetComponent<TrainPickUpDrop>();
            trainMovement = GetComponent<TrainMovement>();
        }
    }

    public static void UpgradeSpeed()
    {
        if(upgradespeed<2f)
            if (ScoreMoneyManager.HowMuchScore() < 10)
                upgradespeed += 0.2f;
            else if (ScoreMoneyManager.HowMuchScore() < 20)
                upgradespeed += 0.15f;
            else
                upgradespeed += 0.1f;
    }

    public void MoveToVertex(Vertex vert)
    {
        if (isMoving)
            return;

        if (previousWagon != null)
            previousWagon.MoveToVertex(currentVertex);
        StartCoroutine(MoveToVertexIE(vert));
    }

    private IEnumerator MoveToVertexIE(Vertex newVertex)
    {
        isMoving = true;
        Vector2 newPos = newVertex.transform.position;
        Vector2 offset = newPos - (Vector2)transform.position;
        transform.eulerAngles = new Vector3(0f, 0f, Utilities.VecToAngle(offset));
        float dist = -1;
        float prevDist;
        while ((Vector2)transform.position != newPos)
        {
            float boost = Boost.BOOST() ? 2f : 1;
            transform.position += (Vector3)offset * speed * boost * upgradespeed * Time.deltaTime;
            prevDist = dist;
            dist = Vector2.Distance(transform.position, newPos);
            if (dist - prevDist > 0f && prevDist != -1)
                transform.position = newPos;
            yield return null;
        }
        transform.position = newPos;
        currentVertex = newVertex;
        isMoving = false;

        if (isFirst)
            pickUp.PickUpDrop();
    }
}
