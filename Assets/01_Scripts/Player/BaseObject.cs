using UnityEngine;

namespace Kevin
{
    public class BaseObject : MonoBehaviour
    {
        [field: ReadOnly(false)]
        [Tooltip("기본 체력 (레벨에 따라 변경됨)")]
        [field: SerializeField]
        public bool isAlive { get; protected set; } = true;
        
        [Header("-----스텟-----")]
        [Header("레벨")]
        [ReadOnly(false)]
        [Tooltip("현재 레벨")]
        [SerializeField]
        protected int CurLevel;
        [ReadOnly(false)]
        [Tooltip("현재 레벨")]
        [SerializeField]
        protected int MaxLevel;
        
        [Header("체력")]
        [ReadOnly(false)]
        [Tooltip("기본 체력 (레벨에 따라 변경됨)")]
        [SerializeField]
        protected float BaseHp;
        [ReadOnly(false)]
        [Tooltip("추가 체력 (아이템, 스킬 등등에 따라 변경됨)")]
        [SerializeField]
        protected float AdditionalHp;
        [ReadOnly(false)]
        [Tooltip("최대 체력 (Base + Additional)")]
        [SerializeField]
        protected float MaxHp;
        [ReadOnly(false)]
        [Tooltip("현재 체력")]
        [SerializeField]
        protected float CurHp;
        
        [Header("마나")]
        [ReadOnly(false)]
        [Tooltip("기본 마나 (레벨에 따라 변경됨)")]
        [SerializeField]
        protected float BaseMp;
        [ReadOnly(false)]
        [Tooltip("추가 마나 (아이템, 스킬 등등에 따라 변경됨)")]
        [SerializeField]
        protected float AdditionalMp;
        [ReadOnly(false)]
        [Tooltip("최대 마나 (Base + Additional)")]
        [SerializeField]
        protected float MaxMp;
        [ReadOnly(false)]
        [Tooltip("현재 마나")]
        [SerializeField]
        protected float CurMp;
        
        [Header("공격력")]
        [ReadOnly(false)]
        [Tooltip("기본 공격력 (레벨에 따라 변경됨)")]
        [SerializeField]
        protected float BaseAttackDamage;
        [ReadOnly(false)]
        [Tooltip("추가 공격력 (아이템, 스킬 등등에 따라 변경됨)")]
        [SerializeField]
        protected float AdditionalAttackDamage;
        [ReadOnly(false)]
        [Tooltip("최대 공격력 (Base + Additional)")]
        [SerializeField]
        protected float MaxAttackDamage;
        [ReadOnly(false)]
        [Tooltip("현재 공격력")]
        [SerializeField]
        protected float CurAttackDamage;
        
        [Header("마법력")]
        [ReadOnly(false)]
        [Tooltip("기본 마법력 (레벨에 따라 변경됨)")]
        [SerializeField]
        protected float BaseAttackPower;
        [ReadOnly(false)]
        [Tooltip("추가 마법력 (아이템, 스킬 등등에 따라 변경됨)")]
        [SerializeField]
        protected float AdditionalAttackPower;
        [ReadOnly(false)]
        [Tooltip("최대 마법력 (Base + Additional)")]
        [SerializeField]
        protected float MaxAttackPower;
        [ReadOnly(false)]
        [Tooltip("현재 마법력")]
        [SerializeField]
        protected float CurAttackPower;
        
        [Header("방어력")]
        [ReadOnly(false)]
        [Tooltip("기본 방어력 (레벨에 따라 변경됨)")]
        [SerializeField]
        protected float BaseArmor;
        [ReadOnly(false)]
        [Tooltip("추가 방어력 (아이템, 스킬 등등에 따라 변경됨)")]
        [SerializeField]
        protected float AdditionalArmor;
        [ReadOnly(false)]
        [Tooltip("최대 방어력 (Base + Additional)")]
        [SerializeField]
        protected float MaxArmor;
        [ReadOnly(false)]
        [Tooltip("현재 방어력")]
        [SerializeField]
        protected float CurArmor;
        
        [Header("마법저항력")]
        [ReadOnly(false)]
        [Tooltip("기본 마법저항력 (레벨에 따라 변경됨)")]
        [SerializeField]
        protected float BaseMagicResistance;
        [ReadOnly(false)]
        [Tooltip("추가 마법저항력 (아이템, 스킬 등등에 따라 변경됨)")]
        [SerializeField]
        protected float AdditionalMagicResistance;
        [ReadOnly(false)]
        [Tooltip("최대 마법저항력 (Base + Additional)")]
        [SerializeField]
        protected float MaxMagicResistance;
        [ReadOnly(false)]
        [Tooltip("현재 마법저항력")]
        [SerializeField]
        protected float CurMagicResistance;

        [Header("치명타")]
        [ReadOnly(false)]
        [Tooltip("기본 치명타")]
        [SerializeField]
        protected float BaseCriticalPercent;
        [ReadOnly(false)]
        [Tooltip("추가 치명타 (아이템, 스킬 등등에 따라 변경됨)")]
        [SerializeField]
        protected float AdditionalCriticalPercent;
        [ReadOnly(false)]
        [Tooltip("(기본 + 추가)치명타")]
        [SerializeField]
        protected float MaxCriticalPercent;
        [ReadOnly(false)]
        [Tooltip("현재 치명타")]
        [SerializeField]
        protected float CurCriticalPercent;
        
        [Header("이동속도")]
        [ReadOnly(false)]
        [Tooltip("기본 이동속도")]
        [SerializeField]
        protected float BaseMovementSpeed;
        [ReadOnly(false)]
        [Tooltip("추가 이동속도 (아이템, 스킬 등등에 따라 변경됨)")]
        [SerializeField]
        protected float AdditionalMovementSpeed;
        [ReadOnly(false)]
        [Tooltip("(기본 + 추가)이동속도")]
        [SerializeField]
        protected float MaxMovementSpeed;
        [ReadOnly(false)]
        [Tooltip("현재 이동속도")]
        [SerializeField]
        protected float CurMovementSpeed;
        /// <summary>
        /// CurHp를 줄이거나, 회복(늘릴때)할때 사용하는 함수.<br />
        /// 외부에서 직속으로 사용이 불가능하다. 무조건 어떠한 함수로 인해 호출이 가능하다.<br />
        /// (예) public TakeDamage()함수를 통해 방어력등 여러 공식을 거친 후, 직접적으로 체력에 데미지가 들어올때 사용.
        /// </summary>
        /// <param name="amount"> 데미지(-), 회복(+) </param>
        protected virtual void UpdateCurHp(float amount)
        {
            CurHp += amount;
            if (CurHp <= 0)
            {
                CurHp = 0;
                Died();
            }
            else if (CurHp >= MaxHp)
            {
                CurHp = MaxHp;
            }
        }
        
        /// <summary>
        /// CurMp를 줄이거나, 회복(늘릴때)할때 사용하는 함수.<br />
        /// 사용하기 전에 amount가 CurMp보다 크거나 작을때의 예외처리를 해줘야한다.<br />
        /// 외부에서 직속으로 사용이 불가능하다. 무조건 어떠한 함수로 인해 호출이 가능하다.<br />
        /// (예) public HealMpForSecond()함수를 통해 마나 재생력등 여러 공식을 거친 후, 직접적으로 체력에 데미지가 들어올때 사용.
        /// </summary>
        /// <param name="amount"> 사용(-), 회복(+) </param>
        protected virtual void UpdateCurMp(float amount)
        {
            CurMp += amount;
            if (CurMp <= 0)
            {
                CurMp = 0;
            }
            else if (CurHp >= MaxHp)
            {
                CurHp = MaxHp;
            }
        }

        protected virtual void Died()
        {
            isAlive = false;
            Debug.Log("죽음");
        }
        
    }
}