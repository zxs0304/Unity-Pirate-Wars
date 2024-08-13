using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParaboleData" , menuName = "ScriptableObject数据/ParaboleData")]
public class ParaboleData : ScriptableObject
{
    public string name;
    public int line1Num = 10;
    public float maxForce = 3f;
    public float fixedForce = 5.4f;
    public float gravityForce = 2f;
    public float rotate = 2f; //发射时施加的扭矩大小
    public float pointsGap = 0.12f; //时间的细分刻度，该值越小，抛物线越准确
}
