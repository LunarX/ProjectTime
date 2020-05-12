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
    // getcomponent<pythonconnection>
    // trouver le script

    private static PythonConnexion instance;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            equipementMesures = new EquipementMesures();
            equipementMesures.Start();
            print("Creating python receiver");
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void OnDestroy()
    {
        if(equipementMesures != null)
        {
            equipementMesures.Stop();
        }
    }
}
