using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectChangeSO",menuName ="ScriptableObjecs/ObjectChangeSO")]
public class ModelChangeResultSO  : ScriptableObject{

    public GameObject InputModel;
    public GameObject OutputModel;

    public float TimeToChange;
    public int Value;
}
