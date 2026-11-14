using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectsBlockSelector : MonoBehaviour, ISelector
{

    private List<Transform> selectables;
    [SerializeField] private float threshold = 0.97f;
    [SerializeField] private float distanceLimit = 5f;
    private Transform _selection;
    private GameObject[] selectableGameobjects;

    public string selectableTag = "Selectable";

    private void Start()
    {
        selectables = new List<Transform>();
        selectableGameobjects = GameObject.FindGameObjectsWithTag(selectableTag);
        for(int i = 0; i < selectableGameobjects.Length; i++)
        {
            selectables.Add(selectableGameobjects[i].transform);
        }
    }

    public void Check(Ray ray)
    {
        _selection = null;

        var closest = 0f;
        for (int i = 0; i < selectables.Count; i++)
        {
            Vector3 vector1 = ray.direction;
            Vector3 vector2 = selectables[i].position - ray.origin;
            float lookPercentage = Vector3.Dot(vector1.normalized, vector2.normalized);
            float totalDistance = Vector3.Distance(Vector3.zero, vector2);

            if (lookPercentage > threshold && lookPercentage > closest && totalDistance < distanceLimit)
            {
                if (Physics.Raycast(ray, out var hit))
                {
                    var test = hit.transform;
                    if (test.CompareTag(selectableTag))
                    {
                        if (test != selectables[i])
                            break;
                    }
                }
                closest = lookPercentage;
                _selection = selectables[i];
            }
        }
    }

    public Transform GetSelection()
    {
        return _selection;
    }
}
