using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public static GameConfig Instance;
    private void Awake()
    {
        Instance = this;
    }

    public int[] Lv_MaxHP;

    public int[] Lv_NextExp;

    public int getNextExp(int level)
    {
        level--;
        if (level  < Lv_NextExp.Length)
            return Lv_NextExp[level];
        else
            return Lv_NextExp[Lv_NextExp.Length - 1];
    }

    public int getMaxHp(int level)
    {
        level--;
        if (level < Lv_MaxHP.Length)
            return Lv_MaxHP[level];
        else
            return Lv_MaxHP[Lv_MaxHP.Length - 1];
    }
}
