using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Object
{
    

    public override void Initialize()
    {
        base.Value = 10;
        base.Key = "Enemy";
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
