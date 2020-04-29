using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PythonConnexion : MonoBehaviour
{
    public EquipementMesures equipementMesures { get; private set; }
    
    //void Update()
    //{
    //    print("bpm:" + equipementMesures.bpm);
    //}

    void Start()
    {
        equipementMesures = new EquipementMesures();
        equipementMesures.Start();
        print("Creating python receiver");
    }

    void OnDestroy()
    {
        equipementMesures.Stop();
    }
}
