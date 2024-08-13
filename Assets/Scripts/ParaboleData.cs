using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParaboleData" , menuName = "ScriptableObject����/ParaboleData")]
public class ParaboleData : ScriptableObject
{
    public string name;
    public int line1Num = 10;
    public float maxForce = 3f;
    public float fixedForce = 5.4f;
    public float gravityForce = 2f;
    public float rotate = 2f; //����ʱʩ�ӵ�Ť�ش�С
    public float pointsGap = 0.12f; //ʱ���ϸ�̶ֿȣ���ֵԽС��������Խ׼ȷ
}
