using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress{

    public class OnProgressChangedEventArgs:EventArgs {  public float progress; }

    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    
}
