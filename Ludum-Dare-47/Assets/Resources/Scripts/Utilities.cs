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

}
