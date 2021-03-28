using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGridRenderer : Graphic
{

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;

        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = new Vector3(0,0);
        vh.AddVert(vertex);

        vertex.position = new Vector3(0, height);
        vh.AddVert(vertex);

        vertex.position = new Vector3(width,height);
        vh.AddVert(vertex);

        vertex.position = new Vector3(width,0);
        vh.AddVert(vertex);

        vh.AddTriangle(0,1,2);
        vh.AddTriangle(2,3,0);
    }

}
