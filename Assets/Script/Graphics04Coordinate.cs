using UnityEngine;

[ExecuteInEditMode]
public class Graphics04Coordinate : MonoBehaviour
{
    public enum SpaceType
    {
        WORLD_LOCAL,
        ORTHO_PIXEL,
        VIEWPORT
    }

    public Material _materialPic;//画图所使用的Pic
    public float _PicSize = 5;

    public float _PicOrthoSize = 1.0f;
    public float _PicOrthoSize2 = 0.5f;

    public float _PicPixelMatrixSizeX = 100.0f;
    public float _PicPixelMatrixSizeY = 100.0f;

    private Material _lineMaterial;
     

    private void Start()
    {
        InitMaterial();
    }

    private void InitMaterial()
    {
        if (_lineMaterial == null)
        {
            _lineMaterial = new Material(Shader.Find("Hidden/Internal-Colored"));
        }

    }

    private void Update()
    {
        transform.RotateAround(transform.position, transform.up, 2.0f);
    }

    private void OnRenderObject()
    {
        //绘制视口线
        DrawViewPortLine();

        //左上角视口
        GL.Viewport(new Rect(0, Screen.height / 2, Screen.width / 2, Screen.height / 2));
        DrawWorldQuads();
        DrawLocalQuads();


        //右上角
        GL.Viewport(new Rect(Screen.width / 2, Screen.height / 2, Screen.width / 2, Screen.height / 2));
        DrawOrthoQuads();


        //左下角
        GL.Viewport(new Rect(0, 0, Screen.width / 2, Screen.height / 2));
        DrawPixelMatrix() ;


        //右下角
        GL.Viewport(new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height / 2));
        DrawPixelMatrix2();

    }

    //只有Begin和end的Quads
    private void DrawWorldQuads()
    {
        if (_materialPic == null)
        {
            return;
        }

        _materialPic.SetPass(0);

        GL.PushMatrix();
        
        GL.Begin(GL.QUADS);

        GL.Color(Color.white);
        GL.TexCoord2(0, 0);
        GL.Vertex3(-_PicSize, -_PicSize, 0);
        GL.TexCoord2(0, 1);
        GL.Vertex3(-_PicSize, _PicSize, 0);
        GL.TexCoord2(1, 1);
        GL.Vertex3(_PicSize, _PicSize, 0);
        GL.TexCoord2(1, 0);
        GL.Vertex3(_PicSize, -_PicSize, 0);

        GL.End();
        GL.PopMatrix();

    }
    //只有Begin和end的Quads
    private void DrawLocalQuads()
    {
        if (_materialPic == null)
        {
            return;
        }

        _materialPic.SetPass(0);

        GL.PushMatrix();
        GL.MultMatrix(transform.localToWorldMatrix);

        GL.Begin(GL.QUADS);

        GL.Color(Color.white);
        GL.TexCoord2(0, 0);
        GL.Vertex3(-_PicSize, -_PicSize, 0);
        GL.TexCoord2(0, 1);
        GL.Vertex3(-_PicSize, _PicSize, 0);
        GL.TexCoord2(1, 1);
        GL.Vertex3(_PicSize, _PicSize, 0);
        GL.TexCoord2(1, 0);
        GL.Vertex3(_PicSize, -_PicSize, 0);

        GL.End();
        GL.PopMatrix();

    }

    //2D使用正交投影
    private void DrawOrthoQuads()
    {
        if (_materialPic == null)
        {
            return;
        }

        _materialPic.SetPass(0);

        GL.PushMatrix();
        GL.LoadOrtho();

        GL.Begin(GL.QUADS);

        GL.Color(Color.white);
        GL.TexCoord2(0, 0);
        GL.Vertex3(-_PicOrthoSize, -_PicOrthoSize, 0);
        GL.TexCoord2(0, 1);
        GL.Vertex3(-_PicOrthoSize, _PicOrthoSize, 0);
        GL.TexCoord2(1, 1);
        GL.Vertex3(_PicOrthoSize, _PicOrthoSize, 0);
        GL.TexCoord2(1, 0);
        GL.Vertex3(_PicOrthoSize, -_PicOrthoSize, 0);

        GL.End();
        GL.PopMatrix();


        GL.PushMatrix();
        GL.LoadOrtho();

        GL.Begin(GL.QUADS);

        GL.Color(Color.white);
        GL.TexCoord2(0, 0);
        GL.Vertex3(0, 0, 0);
        GL.TexCoord2(0, 1);
        GL.Vertex3(0, _PicOrthoSize2, 0);
        GL.TexCoord2(1, 1);
        GL.Vertex3(_PicOrthoSize2, _PicOrthoSize2, 0);
        GL.TexCoord2(1, 0);
        GL.Vertex3(_PicOrthoSize2, 0, 0);

        GL.End();
        GL.PopMatrix();

    }

    //在屏幕上以绝对的像素进行绘制
    private void DrawPixelMatrix()
    {
        GL.PushMatrix();
        GL.LoadPixelMatrix();
        GL.Begin(GL.QUADS);

        GL.Color(Color.white);
        GL.TexCoord2(0, 0);
        GL.Vertex3(0, 0, 0);
        GL.TexCoord2(0, 1);
        GL.Vertex3(0, _PicPixelMatrixSizeY, 0);
        GL.TexCoord2(1, 1);
        GL.Vertex3(_PicPixelMatrixSizeX, _PicPixelMatrixSizeY, 0);
        GL.TexCoord2(1, 0);
        GL.Vertex3(_PicPixelMatrixSizeX, 0, 0);

        GL.End();

        GL.PopMatrix();
    }

    //在屏幕上约定屏幕大小再进行绘制
    private void DrawPixelMatrix2()
    {
        GL.PushMatrix();
        GL.LoadPixelMatrix(0,1920,0,1080);
        GL.Begin(GL.QUADS);

        GL.Color(Color.white);
        GL.TexCoord2(0, 0);
        GL.Vertex3(0, 0, 0);
        GL.TexCoord2(0, 1);
        GL.Vertex3(0, _PicPixelMatrixSizeY, 0);
        GL.TexCoord2(1, 1);
        GL.Vertex3(_PicPixelMatrixSizeX, _PicPixelMatrixSizeY, 0);
        GL.TexCoord2(1, 0);
        GL.Vertex3(_PicPixelMatrixSizeX, 0, 0);

        GL.End();

        GL.PopMatrix();
    }

    //绘制十字线
    private void DrawViewPortLine()
    {
        GL.PushMatrix();
        GL.LoadPixelMatrix();
        GL.Begin(GL.LINES);
        GL.Color(Color.blue);
        GL.Vertex3(0, Screen.height / 2, 0);
        GL.Vertex3(Screen.width, Screen.height / 2, 0);
        GL.Vertex3(Screen.width / 2, Screen.height, 0);
        GL.Vertex3(Screen.width / 2, 0, 0);

        GL.End();

        GL.PopMatrix();
    }

 
}

