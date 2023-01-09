using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Object : MonoBehaviour, Interface
{
    protected string Key;
    protected int Value;
    
    public GameObject _Object;
    public abstract void Initialize();
   
    public abstract void Progress();

    public abstract void Release();

    public abstract string GetKey();

}
