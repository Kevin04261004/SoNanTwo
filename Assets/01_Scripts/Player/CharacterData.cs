using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "new Character", fileName = "StatData")]
public class CharacterData : ScriptableObject
{
      /* 만약 캐릭터가 레벨당 이동속도가 존재하거나 특별하다면 CharacterData상속받아서  */
      
      [Tooltip("캐릭터 이름")]
      public string characterName;
      
      /* 레벨에 따라 변경 */
      [Tooltip("기본 체력 (레벨 당)")]
      public float[] baseHp;
      [Tooltip("기본 마나 (레벨 당)")]
      public float[] baseMp;
      [Tooltip("기본 공격력 (레벨 당)")]
      public float[] baseAD;
      [Tooltip("기본 마법력 (레벨 당)")]
      public float[] baseAP;
      [Tooltip("기본 방어력 (레벨 당)")]
      public float[] baseArmor;
      [Tooltip("기본 마법 저항력 (레벨 당)")]
      public float[] baseMagicResistance;
      [Tooltip("기본 치명타 (0번째 인덱스만 사용. 만약 특별한 캐릭터라면 추가 가능, 그리고 BaseCharacter사용하지 말고 상속받아서 SetData 변경)")]
      public float[] baseCriticalPercent;
      [Tooltip("기본 이동속도 (0번쨰 인덱스만 사용. 만약 특별한 캐릭터라면 추가 가능, 그리고 BaseCharacter사용하지 말고 상속받아서 SetData 변경)")]
      public float[] characterSpeed;
      [Tooltip("기본 체력재생 (레벨 당)")]
      public float[] hpRegeneration;
}
