using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IceMaker : Skill
{
    [Header("아이스메이커 데이터")]
    public float iceMakerDistance = 50.0f;

    Queue<Transform> usingIceQueue;
    Queue<Transform> readyIceQueue;

    Transform preview;

    Vector3 createPosition = Vector3.zero;

    Vector3 iceSize = Vector3.zero;

    Vector3 waterCheckBoxHalfSize = Vector3.zero;

    const float WaterCheckBoxHeight = 0.1f;

    bool isPossible = false;

    protected override void Awake()
    {
        base.Awake();
        Transform ices = transform.GetChild(1);

        usingIceQueue = new Queue<Transform>(ices.childCount);
        readyIceQueue = new Queue<Transform>(ices.childCount);
        for(int i = 0; i < ices.childCount; i++)
        {
            readyIceQueue.Enqueue(ices.GetChild(i));
        }
        
        preview = transform.GetChild(2);

        iceSize = preview.GetChild(0).lossyScale;

        waterCheckBoxHalfSize = iceSize * 0.5f;
        waterCheckBoxHalfSize.y = WaterCheckBoxHeight;
    }
    protected override void Start()
    {
        base.Start();

        if (owner != null)
        {
            owner.onScroll += (_) => ScrollDown();
        }

    }

    protected override void OnEnable()
    {
        base.OnEnable();

        StopAllCoroutines();
        StartCoroutine(CreateCheck());
    }
    IEnumerator CreateCheck()
    {
        while (true)
        {
            isPossible = false;
            Ray ray = Camera.main.ViewportPointToRay(Center);

            // 해당 위치가 물인지 체크
            if (Physics.Raycast(ray, out RaycastHit hit, iceMakerDistance))
            {
                // 물의 범위가 얼음 크기만큼 되는지 체크
                Collider[] colliders = Physics.OverlapBox(hit.point, waterCheckBoxHalfSize);
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.layer == LayerMask.NameToLayer("Water"))
                    {
                        Vector3 point = (hit.point + Vector3.up * preview.lossyScale.y * 0.5f);
                        if (!Physics.CheckBox(point, preview.lossyScale * 0.5f, Quaternion.identity, LayerMask.GetMask("Ground")))
                        {
                            isPossible = true;
                            createPosition = hit.point;
                            preview.gameObject.SetActive(true);
                            preview.position = createPosition;
                        }
                        else
                        {
                            preview.gameObject.SetActive(false);
                            //Debug.Log("생성 불가");
                        }
                    }
                    else
                    {
                        preview.gameObject.SetActive(false);
                        //Debug.Log("물 범위가 좁음");
                    }
                }
            }
            else
            {
                preview.gameObject.SetActive(false);
                //Debug.Log("물이 아님");
            }

            yield return null;
        }
    }

    protected override void UseSkillAction()
    {
        base.UseSkillAction();
        if(isPossible)
        {
            StopAllCoroutines();
        }
    }

    void ScrollDown()
    {

    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Ray ray = Camera.main.ViewportPointToRay(Center);
        Physics.Raycast(ray, out RaycastHit hit, iceMakerDistance);
        // 레이캐스트 보여주는 기즈모
        if (hit.transform != null)
        {
            Handles.color = Color.green;
            Vector3 vec = Camera.main.ViewportToWorldPoint(Center);
            Handles.DrawLine(vec, hit.point, 2);
            if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Water"))
            {
                Handles.DrawWireCube(hit.point, waterCheckBoxHalfSize * 2);
            }
        }

    }
#endif

}
