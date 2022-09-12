using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// Bake Mesh 示例
/// </summary>
public class BakeMeshTest : MonoBehaviour
{
    
    public Animation m_animation = new Animation();

    [SerializeField]
    SkinnedMeshRenderer m_skinnedMeshRenderer; 

    [SerializeField]
    string m_clipToBake = "horse1";

    List<Mesh> m_bakedMeshList = new List<Mesh>();

    /// <summary>
    /// 采样帧数
    /// </summary>
    [SerializeField]
    int m_numFramesToBake = 30;

    void Start()
    {
        // 获取要Bake的动画片段
        AnimationState clipState = m_animation[m_clipToBake];
        if (clipState == null)
        {
            Debug.LogError(string.Format("Unable to get clip '{0}'", m_clipToBake), this);
            return;
        }

        // 开始播放动画
        m_animation.Play(m_clipToBake, PlayMode.StopAll);

        // 设置动画初始时间戳
        clipState.time = 0.0f;

        // 采样帧间隔
        float deltaTime = clipState.length / (float)(m_numFramesToBake - 1);

        for (int frameIndex = 0; frameIndex < m_numFramesToBake; ++frameIndex)
        {
            string frameName = string.Format("BakedFrame{0}", frameIndex);

            // 创建Mesh
            Mesh frameMesh = new Mesh();
            frameMesh.name = frameName;

            // 动画采样
            m_animation.Sample();

            // 执行BakeMesh
            m_skinnedMeshRenderer.BakeMesh(frameMesh);
            m_bakedMeshList.Add(frameMesh);

            // 设置动画时间戳
            clipState.time += deltaTime;
        }

        // 停止播放动画
        m_animation.Stop();
    }

}

