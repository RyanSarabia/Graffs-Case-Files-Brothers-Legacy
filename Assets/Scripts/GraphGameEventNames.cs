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
    public const string ARCH1_CONFIRM_BUTTON_CLICK = "ARCH1_CONFIRM_BUTTON_CLICK";
    public const string ARCH1_PLAYER_MOVED = "ARCH1_PLAYER_MOVED";
    public const string ARCH1_WALLS_EVENT = "ARCH1_WALLS_EVENT";
    public const string ARCH1_ENERGY_ADDED = "ARCH1_ENERGY_ADDED";
    public const string WATER_CHANGED = "WATER_CHANGED";
    public const string ADJACENT_LISTED = "ADJACENT_LISTED";
    public const string GRAPH_DEVICE_CLICKED = "GRAPH_DEVICE_CLICKED";
    public const string GRAPH_DEVICE_CLICKED_CH2 = "GRAPH_DEVICE_CLICKED_CH2";
    public const string GRAPH_DEVICE_CONFIRM_OCCURRED = "GRAPH_DEVICE_CONFIRM_OCCURRED";
    public const string GRAPH_DEVICE_CONFIRM_OCCURRED_CH2 = "GRAPH_DEVICE_CONFIRM_OCCURRED_CH2";
    public const string GRAPH_DEVICE_CONFIRMED = "GRAPH_DEVICE_CONFIRMED";
    public const string GRAPH_DEVICE_CONFIRMED_CH2 = "GRAPH_DEVICE_CONFIRMED_CH2";
    public const string GRAPH_DEVICE_RETURN_CLICKED = "GRAPH_DEVICE_RETURN_CLICKED";
    public const string CAMERA_3_CALLED = "CAMERA_3_CALLED";
    public const string CAM3_TO_CAM4 = "CAM3_TO_CAM4";
    public const string CAM4_TO_CAM3 = "CAM4_TO_CAM3";
    public const string CAM3_TO_MAINCAM = "CAM3_TO_MAINCAM";
    public const string ARCH3_NODECLICKED = "ARCH3_NODECLICKED";
    public const string ARCH3_LOCKNODES = "ARCH3_LOCKNODES";
    public const string NPCS_MOVED = "NPCS_MOVED";
    public const string TIMELINE_PREVNODE_CLICKED = "TIMELINE_PREVNODE_CLICKED";
    public const string TIMELINE_CURNODE_CLICKED = "TIMELINE_CURNODE_CLICKED";
    public const string OVERWORLD_NODE_CLICKED = "OVERWORLD_NODE_CLICKED";    

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
