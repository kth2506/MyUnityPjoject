using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : Object
{
    
    public override void Initialize()
    {
        base.Value = 1;
        base.Key = "Person";
        Debug.Log(base.Key + " : " + base.Value);
    }

    public override void Progress()
    {
    }

    public override void Release()
    {
    }

    public override string GetKey()
    {
        return base.Key;
    }

}
