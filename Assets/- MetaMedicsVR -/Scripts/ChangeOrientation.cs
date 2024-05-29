using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ChangeOrientation : MonoBehaviour
{
    public Transform[] transforms;

    [DllImport("__Internal")]
    private static extern bool IsPortrait();

    private void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (IsPortrait())
        {
            foreach (Transform t in transforms)
            {
                if (t)
                {
                    t.eulerAngles = new Vector3(t.eulerAngles.x, t.eulerAngles.y, 90);
                }
            }
        }
#endif
    }

    private void Update()
    {

        

    }
}
