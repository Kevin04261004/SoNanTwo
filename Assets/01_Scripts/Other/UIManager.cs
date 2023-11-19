using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image skillsPanel;
    private int _toggleSpeed = 6000;
    public void ToggleSkillsPanel()
    {
        if (skillsPanel.rectTransform.position.y <= -400)
        {
            //열기
            StartCoroutine(nameof(OpenSkillsPanel));
        }
        else
        {
            // 닫기
            StartCoroutine(nameof(CloseSkillsPanel));
        }
    }

    private IEnumerator OpenSkillsPanel()
    {
        while (skillsPanel.rectTransform.position.y < 0)
        {
            skillsPanel.rectTransform.position += Vector3.up * _toggleSpeed*Time.deltaTime;
            yield return new WaitForSeconds(0.02f);
        }
    }
    private IEnumerator CloseSkillsPanel()
    {
        while (skillsPanel.rectTransform.position.y > -400)
        {
            skillsPanel.rectTransform.position += Vector3.down * _toggleSpeed*Time.deltaTime;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
