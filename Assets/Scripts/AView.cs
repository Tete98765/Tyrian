using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Initialize();
    public virtual void DoHide()
    {
        gameObject.SetActive(false);
    }
    public virtual void DoShow(object args)
    {
        gameObject.SetActive(true);
    }

}
