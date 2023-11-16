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
        
        [Header("Scriptable Obj")]
        [ReadOnly(true)]
        [SerializeField]
        protected CharacterData Data;
        
        [Header("그 외")]
        private WaitForSeconds zeroPointOneSeconds = new WaitForSeconds(0.1f);

        protected virtual void Awake()
        {
            SetData();
            SetCurStat();
            TakeDamage(90);
        }

        protected virtual void Start()
        {
            HpRegen();
        }
        
        /// <summary>
        /// 초기화 또는 레벨이 1 증가할때마다 호출. <br />
        /// SetMaxStat() 함수를 호출함.
        /// </summary>
        protected virtual void SetData()
        {
            if (Data == null)
            {
                Debug.Assert(false, "Data가 존재하지 않음");
            }

            BaseHp = Data.baseHp[CurLevel];
            BaseMp = Data.baseMp[CurLevel];
            BaseAttackDamage = Data.baseAD[CurLevel];
            BaseAttackPower = Data.baseAP[CurLevel];
            BaseArmor = Data.baseArmor[CurLevel];
            BaseMagicResistance = Data.baseMagicResistance[CurLevel];
            BaseCriticalPercent = Data.baseCriticalPercent[0];
            BaseMovementSpeed = Data.characterSpeed[0];
            BaseHpRegeneration = Data.hpRegeneration[CurLevel];
            
            SetMaxStat();
        }

        /// <summary>
        /// 최대 스텟을 설정할 때 호출.<br />
        /// (예) 레벨업 후, 스텟 변경, 아이템 구매, 아이템 판매<br />
        /// 크리티컬 확률은 100을 넘을 수 없게 작성됨.
        /// </summary>
        protected virtual void SetMaxStat()
        {
            MaxHp = BaseHp + AdditionalHp;
            MaxMp = BaseMp + AdditionalMp;
            MaxAttackDamage = BaseAttackDamage + AdditionalAttackDamage;
            MaxAttackPower = BaseAttackPower + AdditionalAttackPower;
            MaxArmor = BaseArmor + AdditionalArmor;
            MaxMagicResistance = BaseMagicResistance + AdditionalMagicResistance;
            float tempCritical = (BaseCriticalPercent + AdditionalCriticalPercent);
            MaxCriticalPercent =  tempCritical <= 100 ? tempCritical : 100;
            MaxMovementSpeed = BaseMovementSpeed + AdditionalMovementSpeed; 
            MaxHpRegeneration = BaseHpRegeneration + AdditionalHpRegeneration;
        }
        
        /// <summary>
        /// 죽고나서 태어났을때, 맨 처음 시작할때 maxStat을 초기화 후 호출하여, curstat을 maxStat으로 변경.
        /// </summary>
        protected virtual void SetCurStat()
        {
            CurHp = MaxHp;
            CurMp = MaxMp;
            CurAttackDamage = MaxAttackDamage;
            CurAttackPower = MaxAttackPower;
            CurArmor = MaxArmor;
            CurMagicResistance = MaxMagicResistance;
            CurCriticalPercent = MaxCriticalPercent;
            CurMovementSpeed = MaxMovementSpeed;
            CurHpRegeneration = MaxHpRegeneration;
        }
        /// <summary>
        /// 레벨업이 되었을때 호출한다. SetData()가 호출됨.
        /// </summary>
        protected void LevelUp()
        {
            CurLevel++;
            if (CurLevel > MaxLevel)
            {
                CurLevel = MaxLevel;
            }
            SetData();
        }
        
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
            float temp = 0;
            if (DecreasedHeal != 0)
            {
                temp = amount * (1 - DecreasedHeal / 100); // 치유감소 공식   
            }
            else
            {
                temp = amount;
            }
            UpdateCurHp(temp);
        }

        /// <summary>
        /// StartCoroutine(nameof(HpRegenRoutine));
        /// </summary>
        protected void HpRegen()
        {
            StartCoroutine(nameof(HpRegenRoutine));
        }
        
        /// <summary>
        /// 0.1초마다 체력을 (CurHpRegen/10)씩 회복한다.
        /// </summary>
        /// <returns></returns>
        protected IEnumerator HpRegenRoutine()
        {
            while (isAlive)
            {
                Heal(CurHpRegeneration / 10); 
                yield return zeroPointOneSeconds;
            }
        }
    }   
}
