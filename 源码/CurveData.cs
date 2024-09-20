using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CurveData
{
    public GameObject curvePrefab;
    public CurveType type;

}
public enum CurveType
{
    勾,
    右点,
    左点,
    平提,
    斜提,
    平捺,
    斜捺,
    反捺,
    短斜撇,
    长斜撇,
    平撇,
    竖撇,
    竖弯撇
}