using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Car : MonoBehaviour
{
    private int step = 0;
    public void SetData(Sprite cl , string slot)
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = cl;
        transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
    }
    private void OnMouseDown()
    {
        if (GameManager.Instance.canDrag)
        {
            CheckAndMove();
        }
    }

    void CheckAndMove()
    {
        GameManager.Instance.canDrag = false;
        Move(transform.GetChild(0),GameManager.Instance.targetLock);
        Move(transform.GetChild(1),GameManager.Instance.targetThen);
        GetComponent<BoxCollider2D>().enabled = false;
    }
    
    public void Move(Transform tr,Transform target)
    {
        StartCoroutine(MoveToTarget(tr,target));
    }

    IEnumerator MoveToTarget(Transform tr,Transform target)
    {
        var dis = Vector3.Distance(target.position , tr.position);
        var dir = target.position - tr.position;
        while (dis > 0.1f)
        {
            yield return new WaitForEndOfFrame();
            tr.position = tr.position + dir * 0.013f;
            dis = Vector3.Distance(target.position , tr.position);
        }

        tr.position = target.position + new Vector3( Random.Range(-0.2f,0.2f),Random.Range(-0.2f,0.2f));
        CheckOnMoveDone();
        var particleVFXs = GameManager.Instance.particleVFXs;
        GameObject explosion = Instantiate(particleVFXs[Random.Range(0,particleVFXs.Count)], tr.position, tr.rotation);
        Destroy(explosion, .75f);
    }

    void CheckOnMoveDone()
    {
        step++;
        if (step == 2)
        {
            GameManager.Instance.GetCurLevel().RemoveObject(gameObject);
            GameManager.Instance.EnableDrag();
        }
        
    }
}
