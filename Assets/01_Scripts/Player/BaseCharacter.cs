using System;
using System.Collections;
using UnityEngine;


namespace Kevin
{
    public class BaseCharacter : BaseObject
    {
        [Header("초당 체력 재생")]
        [ReadOnly(false)]
        [Tooltip("기본 체력재생 (레벨에 따라 변경됨)")]
        [SerializeField]
        protected float BaseHpRegeneration;
        [ReadOnly(false)]
        [Tooltip("추가 체력재생 (아이템, 스킬 등등에 따라 변경됨)")]
        [SerializeField]
        protected float AdditionalHpRegeneration;
        [ReadOnly(false)]
        [Tooltip("(기본 + 추가) 체력재생")]
        [SerializeField]
        protected float MaxHpRegeneration;
        [ReadOnly(false)]
        [Tooltip("현재 체력재생")]
        [SerializeField]
        protected float CurHpRegeneration;
        
        [Header("그 외 스텟들")]
        [ReadOnly(false)]
        [Tooltip("치유감소%")]
        [Range(0,100)]
        [SerializeField]
        protected int DecreasedHeal;

        [Header("그 외")]
        private WaitForSeconds zeroPointOneSeconds = new WaitForSeconds(0.1f);
        /// <summary>
        /// 피해를 받았을때 공식을 통해 데미지를 입는다.
        /// </summary>
        /// <param name="damage">상대방이 입히는 피해량</param>
        public void TakeDamage(float damage)
        {
            float temp = damage * (100 / (100 + CurArmor)); // 방어력 공식
            
            UpdateCurHp(-temp);
        }
        /// <summary>
        /// 피해를 받았을때 공식을 통해 데미지를 입는다.
        /// 데미지의 일정퍼센테이지를 고정데미지로 입는다. 
        /// </summary>
        /// <param name="damage">상대방이 입히는 피해량</param>
        /// <param name="trueDamagePercentage">damage 중에 몇 퍼센트를 고정피해로 입음.</param>
        public void TakeDamage(float damage, float trueDamagePercentage)
        {
            
        }
        /// <summary>
        /// 캐릭터가 회복을 할때 공식을 통해 회복을 한다.
        /// </summary>
        /// <param name="amount">힐량</param>
        public void Heal(float amount)
        {
            float temp = amount * (1 - DecreasedHeal / 100); // 치유감소 공식
            
            UpdateCurHp(temp);
        }

        private IEnumerator HpRegenRoutine()
        {
            while (isAlive)
            {
                Heal(CurHpRegeneration / 10); 
                yield return zeroPointOneSeconds;
            }
        }
    }   
}
