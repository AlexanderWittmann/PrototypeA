﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTargetBlock: MonoBehaviour
{
    public LayerMask hitLayers;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, hitLayers))
            {
                this.transform.position = hit.point + new Vector3(0f,.5f,0f);
            }
        }
    }
}