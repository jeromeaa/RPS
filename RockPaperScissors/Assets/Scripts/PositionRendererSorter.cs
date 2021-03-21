using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRendererSorter : MonoBehaviour
{
    [SerializeField]
    private int sortingOrderBase=5000;
    [SerializeField]
    private bool runOnlyOnce = false;

    private float timer;
    private float timeMax = .1f;
    private Renderer myRenderer;
    int order;

    private void Awake()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
        order = myRenderer.sortingOrder;
        
    }

    private void LateUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = timeMax;
            myRenderer.sortingOrder = (int)(sortingOrderBase - transform.position.y + order);
            if (runOnlyOnce)
                Destroy(this);
        }
    }
}
