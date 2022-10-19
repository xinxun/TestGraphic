using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BrightnessSaturationAndContrast : PostEffectsBase
{
    //亮度 对比度 饱和度 参数
    //亮度
    [Range(0.0f, 3.0f)]
    public float brightness = 1.0f;
    //饱和度
    [Range(0.0f, 3f)]
    public float saturation = 1.0f;
    //对比度
    [Range(0.0f, 3f)]
    public float contrast = 1.0f;

    //可以拖入shader
    public Shader m_shader;
    //自定义生成material
    private Material m_material;

    public Texture _mainTexture;

    public RenderTexture _targetRenderTexture;
    private CommandBuffer _commandBuffer;

    private RenderTexture buffer1;

    //根据基类写的方法生成material
    public Material material
    {
        get
        {
            m_material = CheckShaderAndCreateMaterial(m_shader, m_material);
            return m_material;
        }
    }

    //定义OnRenderImage函数来进特效处理
    //当OnRenderImage函数被调用的时候，他会检查材质球是否可用。如果可用，就把参数传递给材质，
    //再调用Graphics.Blit进行处理；否则，直接把原图像显示到屏幕上，不做任何处理。
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (material != null)
        {
            material.SetFloat("_brightness", brightness);
            material.SetFloat("_saturation", saturation);
            material.SetFloat("_contrast", contrast);
            //通过Blit把material加上
            Graphics.Blit(src, dest, material);

            buffer1 = RenderTexture.GetTemporary(src.width, src.height, 0);

            Graphics.Blit(src, buffer1, material);
            //RecoreTarget(src);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }

    private void Start()
    {
        _commandBuffer = new CommandBuffer() { name = "Recodr_RenderTextute" };
    }

    private void OnRenderObject()
    {//GuiTexture.Draw中进行绘制
        DrawTexture();
    }
    public void DrawTexture()
    {
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, Screen.width, Screen.height, 0);
        Graphics.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _mainTexture);
        GL.PopMatrix();
    }

    public void RecoreTarget(RenderTexture src)
    {
        if(_targetRenderTexture == null)
        {
            return;
        }

        Graphics.SetRenderTarget(_targetRenderTexture);
        _commandBuffer.Clear();
        _commandBuffer.ClearRenderTarget(true, true, Color.blue);
        Graphics.ExecuteCommandBuffer(_commandBuffer);
        Graphics.Blit(_targetRenderTexture, src);
    }
}