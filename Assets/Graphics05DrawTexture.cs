
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class Graphics05DrawTexture : MonoBehaviour
{
    public Texture mainTexture;

    public Shader _ShaderRed;
    public Shader _ShaderGreen;
    public Shader _ShaderBlue;
    public Shader _ShaderGrey;

    private Material _materialRed;
    private Material _materialGreen;
    private Material _materialBlue;
    private Material _materialGrey;


    public bool _useBlit = false;
    public float _mainTextureSize = 1.0f;

    //分四个块进行显示对象
    public Vector2 _blockLeftTop = new Vector2(0, 0);
    public Rect _LeftTopPixelRect = new Rect(0, 0.5f, 0.5f, 0.5f);

    public Vector2 _blockLeftBottom = new Vector2(0, 290);
    public Rect _LeftBottomPixelRect = new Rect(0, 0, 0.5f, 0.5f);

    public Vector2 _blockRightTop = new Vector2(500, 0);
    public Rect _RightTopPixelRect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);

    public Vector2 _blockRightBottom = new Vector2(500, 290);
    public Rect _RigthBottomPixelRect = new Rect(0.5f, 0, 0.5f, 0.5f);


    CommandBuffer clearBuffer;

    private void Start()
    {
        //创建ClearBuffer
        clearBuffer = new CommandBuffer() { name = "Clear Buffer AA" };


        _materialRed = new Material(_ShaderRed);
        _materialGreen = new Material(_ShaderGreen);
        _materialBlue = new Material(_ShaderBlue);
        _materialGrey = new Material(_ShaderGrey);
       
    }

 

    void OnGUI()
    {
        if (Event.current.type.Equals(EventType.Repaint))
        {
           // BlitToSceen();

            if (_useBlit)
            {
                BlitRenderTexture();
            }
            else
            {
                DrawTexture();
            }
            
        }
    }


    public void DrawTexture()
    {
        
        float fBlockWidth = mainTexture.width / 4;
        float fBlockHeight = mainTexture.height / 4;
        //Rect是左上角，采样用的是左下角
        Graphics.DrawTexture(new Rect(_blockLeftTop.x, _blockLeftTop.y, fBlockWidth, fBlockHeight), mainTexture,
            _LeftTopPixelRect, 0, 0, 0, 0, null);
        Graphics.DrawTexture(new Rect(_blockLeftBottom.x, _blockLeftBottom.y, fBlockWidth, fBlockHeight), mainTexture,
            _LeftBottomPixelRect, 0, 0, 0, 0, null);
        Graphics.DrawTexture(new Rect(_blockRightTop.x, _blockRightTop.y, fBlockWidth, fBlockHeight), mainTexture,
            _RightTopPixelRect, 0, 0, 0, 0, null);
        Graphics.DrawTexture(new Rect(_blockRightBottom.x, _blockRightBottom.y, fBlockWidth, fBlockHeight), mainTexture,
            _RigthBottomPixelRect, 0, 0, 0, 0, null);
    }


    //使用Blit通过RenderTexture进行绘制
    void BlitRenderTexture()
    {
       /* if(mainTexture == null)
        {
            return;
        }

        if(_materialRed == null || _materialGreen == null || _materialBlue == null || _materialGrey == null)
        {
            return;
        }*/
        float fBlockWidth = mainTexture.width / 4;
        float fBlockHeight = mainTexture.height / 4;

        var rt = RenderTexture.GetTemporary(mainTexture.width, mainTexture.height, 0);

        RenderTexture currentRt = RenderTexture.active;
        
        Graphics.Blit(mainTexture, rt, _materialRed);
        Graphics.SetRenderTarget(null);
        Graphics.DrawTexture(new Rect(_blockLeftTop.x, _blockLeftTop.y, fBlockWidth, fBlockHeight),
            rt, _LeftTopPixelRect, 0, 0, 0, 0, null);
        
        RenderTexture.active = currentRt;

        Graphics.Blit(mainTexture, rt, _materialGreen);
        RenderTexture.active = null;
        Graphics.DrawTexture(new Rect(_blockLeftBottom.x, _blockLeftBottom.y, fBlockWidth, fBlockHeight),
            rt, _LeftBottomPixelRect, 0, 0, 0, 0, null);

        Graphics.Blit(mainTexture, rt, _materialBlue);
        RenderTexture.active = null;
        Graphics.DrawTexture(new Rect(_blockRightTop.x, _blockRightTop.y, fBlockWidth, fBlockHeight),
            rt, _RightTopPixelRect, 0, 0, 0, 0, null);

        Graphics.Blit(mainTexture, rt, _materialGrey);
        RenderTexture.active = null;
        Graphics.DrawTexture(new Rect(_blockRightBottom.x, _blockRightBottom.y, fBlockWidth, fBlockHeight),
            rt, _RigthBottomPixelRect, 0, 0, 0, 0, null);
        
        RenderTexture.ReleaseTemporary(rt);
    }

    void BlitToSceen()
    {
        //RenderTexture rt = RenderTexture.GetTemporary(mainTexture.width, mainTexture.height);
        Graphics.Blit(mainTexture, null, _materialGrey);
        //RenderTexture.ReleaseTemporary(rt);
    }

   

    
}
