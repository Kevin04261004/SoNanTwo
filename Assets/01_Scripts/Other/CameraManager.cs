using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> cams;
    private int index = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            cams[index].SetActive(false);
            index++;
            if (index > cams.Count - 1)
            {
                index = 0;
            }
            cams[index].SetActive(true);
        }
    }
}
