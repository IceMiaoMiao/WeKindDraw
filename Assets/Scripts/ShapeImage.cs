using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeImage : Image
{
    public float offset;
    public float offsetSpeed=3f;
    public float moveSpeed = 2f;
    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        base.OnPopulateMesh(toFill);
        UIVertex vertex = new UIVertex();
        toFill.PopulateUIVertex(ref vertex,0);
        vertex.position += Vector3.right * offset;
        toFill.SetUIVertex(vertex,0);
        
        vertex = new UIVertex();
        toFill.PopulateUIVertex(ref vertex,3);
        vertex.position += Vector3.right * offset;
        toFill.SetUIVertex(vertex,3);
    }

    private void Update()
    {
        if (offset <= -5)
        {
            for (; offset <= 5; )
            {
                offset += 0.6f * offsetSpeed;
                
            }

            offset = 6;
        }
        else if (offset > 5)
        {
            for (; offset <= -5; )
            {
                offset -= 0.6f * offsetSpeed;
                
            }

            offset = -6;
            
        }
        
        
        
        

        this.rectTransform.position -= new Vector3(0.2f*moveSpeed,0,0);
        if (rectTransform.position.x<-900)
        {
            rectTransform.position = new Vector3(600, 0, 0);
            
        }
        
    }
}
