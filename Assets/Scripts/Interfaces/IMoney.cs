using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoney{

    public abstract float GetMoney();
    public abstract void IncreaseMoney(float money);

    public abstract void DecreaseMoney(float money);

}
