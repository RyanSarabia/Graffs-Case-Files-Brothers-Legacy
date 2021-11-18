using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphGameEventNames
{
    //Events
    public const string LIGHT_UP = "LIGHT_UP";
    public const string BFS_BUTTON_CLICK = "BFS_BUTTON_CLICK";
    public const string DFS_BUTTON_CLICK = "DFS_BUTTON_CLICK";
    public const string PLUS_BUTTON_CLICK = "PLUS_BUTTON_CLICK";
    public const string MINUS_BUTTON_CLICK = "MINUS_BUTTON_CLICK";
    public const string WATER_CHANGED = "WATER_CHANGED";
    public const string ADJACENT_LISTED = "ADJACENT_LISTED";
    public const string GRAPH_DEVICE_CLICKED = "GRAPH_DEVICE_CLICKED";
    public const string ARCH3_NODECLICKED = "ARCH3_NODECLICKED";

    //Archetype1 Actions
    public const string TILE_SELECT = "TILE_SELECT";

    //Strings
    public const string NORTH = "NORTH";
    public const string SOUTH = "SOUTH";
    public const string EAST = "EAST";
    public const string WEST = "WEST";
    public const string LOCAL_NORTH = "LOCAL_NORTH";
    public const string LOCAL_SOUTH = "LOCAL_SOUTH";
    public const string LOCAL_EAST = "LOCAL_EAST";
    public const string LOCAL_WEST = "LOCAL_WEST";
    public const string DIRECTION_UP = "DIRECTION_UP";
    public const string DIRECTION_DOWN = "DIRECTION_DOWN";
    public const string DIRECTION_LEFT = "DIRECTION_LEFT";
    public const string DIRECTION_RIGHT = "DIRECTION_RIGHT";

    public static string oppositeDirection(string direction)
    {
        switch (direction)
        {
            case DIRECTION_UP:
                return DIRECTION_DOWN;
            case DIRECTION_DOWN:
                return DIRECTION_UP;
            case DIRECTION_LEFT:
                return DIRECTION_RIGHT;
            case DIRECTION_RIGHT:
                return DIRECTION_LEFT;
            default: return null;
        }
    }
}
