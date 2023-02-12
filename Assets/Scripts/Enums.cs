using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CESCO
{
    public enum OBJ_TYPE
    {
        PLAYER_HAS,
        TARGET,
        BUG,
    }

    public enum HIT_OBJ_TYPE
    {
        SHOW_HIT,
        CHECK_HIT,
    }

    public enum GAME_STATE
    {
        START,
        RUNNING,
        PAUSE,
        PAY,
    }

    public enum SCREEN
    {
        START = 0,
        INGAME,
        PAUSE,
        SETTING,
        PAY,
        REINFORCE,
        SHOP,
    }

    public enum TOUCH_SCREEN
    {
        LEFT = 0,
        RIGHT = 1,
    }

    public enum SOUND
    {
        BGM,
        EFFECT,
    }

    public enum BUG_TYPE
    {
        MOSQUITO,
        FLY,
        CICADA,
        COCKROACH,
        NONE,
    }

    public enum TOOL
    {
        HAND,
        FLY_SWATTER,
        INSECTICIDE,
        TRAP,
        ELECTRIC_FLY_SWATTER,
        A,
        NONE,
    }

    public enum TOOL_SPEED
    {
        SUPER_SLOW,     // 매우 느림
        SLOW,           //   느림
        NORMAL,         //   보통
        FAST,           //   빠름
        SUPER_FAST      // 매우 빠름
    }

    public enum TOOL_RADIUS
    {
        SMALL,          // 작음
        MEDIUM,         // 보통
        LARGE,          //  큼
    }
}
