using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameLevel : MonoBehaviour
{
    public List<GameObject> gameObjectsPoint;
    [SerializeField] private Transform parentListObjPoint;
    private bool canCheck = true;
    [SerializeField] private List<Sprite> listRandom;
    public void Start()
    {
        canCheck = true;
        foreach (Transform tr in parentListObjPoint)
        {
            if(tr.gameObject.activeSelf)
                gameObjectsPoint.Add(tr.gameObject);
        }
        StartLevel();
    }

    public void RemoveObject(GameObject obj)
    {
        gameObjectsPoint.Remove(obj);
        if (canCheck)
        {
            if (gameObjectsPoint.Count == 0)
            {
                GameManager.Instance.CheckLevelUp();
                canCheck = false;
            }
        }
    }

    public void StartLevel()
    {
        List<GameObject> listObjRandom = new List<GameObject>();
        foreach (GameObject obj in gameObjectsPoint)
        {
            listObjRandom.Add(obj);
        }

        int slot = 0;
        var color = listRandom[Random.Range(0, listRandom.Count)];
        for (int i = 0; i < gameObjectsPoint.Count; i++)
        {
            color = listRandom[Random.Range(0, listRandom.Count)];
            var x = listObjRandom[Random.Range(0, listObjRandom.Count)];
            x.GetComponent<Car>().SetData(color,"listColor"+listRandom.IndexOf(color));
            listObjRandom.Remove(x);
            slot++;
            if (slot == 3)
                slot = 0;
        }
    }
}
