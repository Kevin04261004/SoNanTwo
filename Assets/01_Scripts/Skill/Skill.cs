using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skills",fileName = "Skill")]
public class Skill : ScriptableObject
{
    public ESkillType skillType;
    public Sprite image;
    public string info;
    public int count;
}
