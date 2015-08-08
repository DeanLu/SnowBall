using UnityEngine;
using System.Collections;

public class UIParticle : MonoBehaviour
{
    public bool IsUsing { get { return this.gameObject.activeInHierarchy; } }

    public void Init(float _disableTime)
    { 
        this.gameObject.SetActive(true);

        this.CancelInvoke("DisableGameObject");

        this.Invoke("DisableGameObject", _disableTime);
    }

    private void DisableGameObject()
    {
        this.gameObject.SetActive(false);
    }
}
