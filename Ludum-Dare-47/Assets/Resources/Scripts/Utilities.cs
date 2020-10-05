using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities
{
    public static Vector2 DirToVec2(Direction dir)
    {
        if (dir == Direction.UP)
            return Vector2.up;
        else if (dir == Direction.DOWN)
            return Vector2.down;
        else if (dir == Direction.LEFT)
            return Vector2.left;
        else if (dir == Direction.RIGHT)
            return Vector2.right;
        else
            return Vector2.zero;
    }

    public static float DirToAngle(Direction dir)
    {
        if (dir == Direction.UP)
            return 180f;
        else if (dir == Direction.DOWN)
            return 0f;
        else if (dir == Direction.LEFT)
            return 270f;
        else if (dir == Direction.RIGHT)
            return 90f;
        else
            return -1;
    }

    public static float VecToAngle(Vector2 dir)
    {
        if (dir == Vector2.up)
            return 180f;
        else if (dir == Vector2.down)
            return 0f;
        else if (dir == Vector2.left)
            return 270f;
        else if (dir == Vector2.right)
            return 90f;
        else
            return -1f;
    }
}
