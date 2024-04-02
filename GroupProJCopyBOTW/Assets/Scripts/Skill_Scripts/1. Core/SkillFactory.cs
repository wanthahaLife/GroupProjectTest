using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum SkillType
{
    RemoteBombPool = 0,
    RemoteBombCubePool,
    MagnetCatchPool,
    IceMakerPool
}

public class SkillFactory : Singleton<SkillFactory>
{
    RemoteBombPool remoteBombPool;
    RemoteBombCubePool remoteBombCubePool;
    MagnetCatchPool magnetCatchPool;
    IceMakerPool iceMakerPool;

    protected override void OnInitialize()
    {
        base.OnInitialize();

        remoteBombPool = GetComponentInChildren<RemoteBombPool>();
        if (remoteBombPool != null) remoteBombPool.Initialize();
        remoteBombCubePool = GetComponentInChildren<RemoteBombCubePool>();
        if (remoteBombCubePool != null) remoteBombCubePool.Initialize();
        magnetCatchPool = GetComponentInChildren<MagnetCatchPool>();
        if (magnetCatchPool != null) magnetCatchPool.Initialize();
        iceMakerPool = GetComponentInChildren<IceMakerPool>();
        if (iceMakerPool != null) iceMakerPool.Initialize();
    }
 
    /// <summary>
    /// 풀에 있는 스킬 오브젝트 하나 가져오기
    /// </summary>
    /// <param name="type">가져올 오브젝트의 종류</param>
    /// <param name="position">오브젝트가 배치될 위치</param>
    /// <param name="angle">오브젝트의 초기 각도</param>
    /// <returns>활성화된 오브젝트</returns>
    public GameObject GetObject(SkillType type, Vector3? position = null, Vector3? euler = null)
    {
        GameObject result = null;
        switch (type)
        {
            case SkillType.RemoteBombPool:
                result = remoteBombPool.GetObject(position, euler).gameObject;
                break;
            case SkillType.RemoteBombCubePool:
                result = remoteBombCubePool.GetObject(position, euler).gameObject;
                break;
            case SkillType.MagnetCatchPool:
                result = magnetCatchPool.GetObject(position, euler).gameObject;
                break;
            case SkillType.IceMakerPool:
                result = iceMakerPool.GetObject(position, euler).gameObject;
                break;
        }

        return result;
    }
    /// <summary>
    /// 리모컨폭탄을 가져오는 함수
    /// </summary>
    /// <param name="position">배치될 위치</param>
    /// <returns>활성화된 리모컨폭탄</returns>
    public RemoteBomb GetRemoteBomb(Vector3? position = null, float angle = 0.0f)
    {
        return remoteBombPool.GetObject(position, angle * Vector3.forward);
    }
    public RemoteBombCube GetRemoteBombCube(Vector3? position = null, float angle = 0.0f)
    {
        return remoteBombCubePool.GetObject(position, angle * Vector3.forward);
    }
    public MagnetCatch GetMagnetCatch(Vector3? position = null, float angle = 0.0f)
    {
        return magnetCatchPool.GetObject(position, angle * Vector3.forward);
    }
    public IceMaker GetIceMaker(Vector3? position = null, float angle = 0.0f)
    {
        return iceMakerPool.GetObject(position, angle * Vector3.forward);
    }
}