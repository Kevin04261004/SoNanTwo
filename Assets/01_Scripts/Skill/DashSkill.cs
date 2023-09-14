using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kevin
{
    public class DashSkill : Skill
    {
        public override void UseSkill(GameObject parent)
        {
            parent.transform.position += new Vector3(1, 0, 0);
        }
    }   
}
