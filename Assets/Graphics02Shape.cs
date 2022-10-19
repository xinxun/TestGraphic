using UnityEngine;

public class Graphics02Shape : MonoBehaviour
{
    public enum DrawingType
    {
        DRAW_CIRCLE,
        DRAW_TRIANGLE,
        DRAW_QUAD,
        DRAW_CIRCLE_SURFACE,
        DRAW_CUBE
    }

    public DrawingType type = DrawingType.DRAW_CIRCLE;

    public int circleCount = 6;
    public int circleRadius = 3;
    public int triangleSize = 2;
    public int quadSize = 2;
    public bool _ShowViewPort = true;
    public bool _bRotate = false;

    private Material _shapeMaterial;

    private void SetMaterialPass()
    {
        if (_shapeMaterial == null)
        {
            _shapeMaterial = new Material(Shader.Find("Hidden/Internal-Colored"));
        }

        _shapeMaterial.SetPass(0);
    }

    private void Update()
    {
        if(_bRotate)
        {
            transform.RotateAround(transform.position, Vector3.up, 0.2f);
        }
        
    }

    private void OnRenderObject()
    {
        SetMaterialPass();

        GL.PushMatrix();
        //GL.Viewport(new Rect(0, 0, Screen.width, Screen.height));
        //GL.LoadPixelMatrix();
        //GL.MultMatrix(transform.localToWorldMatrix);
        switch (type)
        {
            case DrawingType.DRAW_CIRCLE:
                DrawCircle();
                break;
            case DrawingType.DRAW_TRIANGLE:
                DrawTriangle();
                break;
            case DrawingType.DRAW_QUAD:
                DrawQuad();
                break;
            case DrawingType.DRAW_CIRCLE_SURFACE:
                DrawCircleSurface();
                break;
            case DrawingType.DRAW_CUBE:
                DrawCube();
                break;
        }
        GL.PopMatrix();

        if (_ShowViewPort)
        {
            //画十字线
            DrawViewPortLine();

            
            GL.PushMatrix();

            //左上角
            GL.Viewport(new Rect(0, Screen.height / 2, Screen.width / 2, Screen.height / 2));
            DrawCircle();
    
            //右上角
            GL.Viewport(new Rect(Screen.width / 2, Screen.height / 2, Screen.width / 2, Screen.height / 2));
            GL.PushMatrix();
            //坐标系发生变化
            GL.MultMatrix(transform.localToWorldMatrix);
            DrawTriangle();
            GL.PopMatrix();

            //左下角
            GL.Viewport(new Rect(0, 0, Screen.width / 2, Screen.height / 2));
            DrawQuad();
           

            //右下角
            GL.Viewport(new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height / 2));
            DrawCircleSurface();


            GL.PopMatrix();
        }

    }

    private void DrawViewPortLine()
    {
        GL.PushMatrix();
        GL.LoadPixelMatrix();
        GL.Begin(GL.LINES);
        GL.Color(Color.blue);
        GL.Vertex3(0, Screen.height/2, 0);
        GL.Vertex3(Screen.width, Screen.height / 2, 0);
        GL.Vertex3(Screen.width/2, Screen.height, 0);
        GL.Vertex3(Screen.width/2, 0, 0);

        GL.End();

        GL.PopMatrix();
    }

    private void DrawCircle()
    {
        float angleDelta = 2 * Mathf.PI / circleCount;

        GL.Begin(GL.LINES);
        GL.Color(Color.red);

        for (int i = 0; i < circleCount; i++)
        {
            float angle = angleDelta * i;
            float angleNext = angle + angleDelta;

            GL.Vertex3(Mathf.Cos(angle) * circleRadius, Mathf.Sin(angle) * circleRadius, 0);
            GL.Vertex3(Mathf.Cos(angleNext) * circleRadius, Mathf.Sin(angleNext) * circleRadius, 0);
        }

        GL.End();
    }

    private void DrawTriangle()
    {
        GL.Begin(GL.TRIANGLES);
        GL.Color(Color.green);
        //顺时针
        GL.Vertex3(-triangleSize, -triangleSize, 0);//左下
        GL.Color(Color.black);
        GL.Vertex3(triangleSize, -triangleSize, 0);//右下
        GL.Color(Color.red);
        GL.Vertex3(0, triangleSize, 0);//上

        GL.End();
    }

    private void DrawQuad()
    {
        GL.Begin(GL.QUADS);
        GL.Color(Color.blue);

        GL.Vertex3(-quadSize, -quadSize, 0);//左下
        GL.Vertex3(-quadSize, quadSize, 0);//左上
        GL.Vertex3(quadSize, quadSize, 0);//右上
        GL.Vertex3(quadSize, -quadSize, 0);//右下

        GL.End();
    }

    private void DrawCircleSurface()
    {//使用多个三角形画圆
        float angleDelta = 2 * Mathf.PI / circleCount;

        GL.Begin(GL.TRIANGLES);
        GL.Color(Color.yellow);

        for (int i = 0; i < circleCount; i++)
        {
            float angle = angleDelta * i;
            float angleNext = angle + angleDelta;

            GL.Vertex3(0, 0, 0);
            GL.Vertex3(Mathf.Cos(angle) * circleRadius, Mathf.Sin(angle) * circleRadius, 0);
            GL.Vertex3(Mathf.Cos(angleNext) * circleRadius, Mathf.Sin(angleNext) * circleRadius, 0);
        }

        GL.End();
    }

    private void DrawCube()
    {
        GL.Begin(GL.QUADS);
        GL.Color(Color.white);

        GL.Vertex3(-quadSize, -quadSize, -quadSize);
        GL.Vertex3(quadSize, -quadSize, -quadSize);
        GL.Vertex3(quadSize, quadSize, -quadSize);
        GL.Vertex3(-quadSize, quadSize, -quadSize);

        GL.Vertex3(-quadSize, -quadSize, quadSize);
        GL.Vertex3(quadSize, -quadSize, quadSize);
        GL.Vertex3(quadSize, quadSize, quadSize);
        GL.Vertex3(-quadSize, quadSize, quadSize);

        GL.Vertex3(quadSize, -quadSize, -quadSize);
        GL.Vertex3(quadSize, -quadSize, quadSize);
        GL.Vertex3(quadSize, quadSize, quadSize);
        GL.Vertex3(quadSize, quadSize, -quadSize);

        GL.Vertex3(-quadSize, -quadSize, -quadSize);
        GL.Vertex3(-quadSize, -quadSize, quadSize);
        GL.Vertex3(-quadSize, quadSize, quadSize);
        GL.Vertex3(-quadSize, quadSize, -quadSize);

        GL.Vertex3(-quadSize, -quadSize, -quadSize);
        GL.Vertex3(quadSize, -quadSize, quadSize);
        GL.Vertex3(quadSize, -quadSize, quadSize);
        GL.Vertex3(-quadSize, -quadSize, quadSize);

        GL.Vertex3(-quadSize, quadSize, -quadSize);
        GL.Vertex3(quadSize, quadSize, -quadSize);
        GL.Vertex3(quadSize, quadSize, quadSize);
        GL.Vertex3(-quadSize, quadSize, quadSize);

        GL.End();
    }
}