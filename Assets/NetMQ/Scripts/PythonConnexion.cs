using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PythonConnexion : MonoBehaviour
{
    private EquipementMesures pr;

    void Update()
    {
        print("bpm:" + pr.bpm);
    }

    void Start()
    {
        pr = new EquipementMesures();
        pr.Start();
        print("Creating python receiver");
    }

    void OnDestroy()
    {
        pr.Stop();
    }
}
