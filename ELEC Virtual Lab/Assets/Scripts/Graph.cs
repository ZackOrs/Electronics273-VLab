using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField]
    Transform pointPrefab = default;

    [SerializeField, Range(1,1000)]
    int resolution = 1;

    Transform[] points;

    void Awake()
    {
        float step = 2f/resolution;
        var position = Vector3.zero;
        var scale = Vector3.one * step;
        points = new Transform[resolution];
		for (int i = 0; i < resolution; i++) {
			Transform point = Instantiate(pointPrefab);
			position.x = (i + 0.5f) * step - 1f;
			point.localPosition = position;
			point.localScale = scale;
            point.SetParent(transform,false);
            points[i] = point;

		}
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time;
        for (int i = 0; i< points.Length; i++){
			Transform point = points[i];
			Vector3 position = point.localPosition;
			position.y = Mathf.Sin(Mathf.PI * (position.x + time));

            //y = sin(pi*(x+t));
			point.localPosition = position;
        }
    }
}
