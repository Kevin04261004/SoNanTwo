using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kevin
{
    public class DashSkill : Skill
    {
        public override void UseSkill(GameObject parent)
        {
            StartCoroutine(nameof(DashRoutine));
            parent.transform.position += new Vector3(1, 0, 0);
        }

        private IEnumerator DashRoutine()
        {
            for (int i = 0; i < 100; i++)
            {
                yield return new WaitForSeconds(0.1f);
                print(i);
            }
        }
    }   
}
