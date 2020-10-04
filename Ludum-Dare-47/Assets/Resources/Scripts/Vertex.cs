using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex : MonoBehaviour
{
    public List<Vertex> neighbours;
    public List<Direction> dirToNeighbours;
}

public enum Direction { NONE, UP, DOWN, LEFT, RIGHT };
