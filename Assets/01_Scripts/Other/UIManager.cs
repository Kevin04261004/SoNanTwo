using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image skillsPanel;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject[] ItemCells;
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

    public void UpdateInven(ref List<Skill> list)
    {
        if (list.Count > ItemCells.Length)
        {
            Debug.Assert(true, "Bug!!!");
            return;
        }
        for (int i = 0; i < ItemCells.Length; ++i)
        {
            if (list.Count > i)
            {
                ItemCells[i].TryGetComponent(out Button btn);
                ItemCells[i].TryGetComponent(out Image color);
                btn.interactable = true;
                ItemCells[i].transform.GetChild(0).gameObject.SetActive(true);
                ItemCells[i].transform.GetChild(0).TryGetComponent(out Image image);
                image.sprite = list[i].image;
                ItemCells[i].transform.GetChild(1).TryGetComponent(out TextMeshProUGUI tmp);
                tmp.text = list[i].count.ToString();   
            }
            else
            {
                ItemCells[i].TryGetComponent(out Button btn);
                ItemCells[i].TryGetComponent(out Image color);
                ItemCells[i].transform.GetChild(0).gameObject.SetActive(false);
                btn.interactable = false;
            }
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
