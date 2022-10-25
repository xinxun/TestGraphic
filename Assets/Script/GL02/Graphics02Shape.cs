using UnityEngine;

public class Graphics02Shape : MonoBehaviour
{
    public enum DrawingType
    {
        DRAW_CIRCLE,
        DRAW_TRIANGLE,
        DRAW_TRINAGLES,
        DRAW_CUBE
    }

    public DrawingType type = DrawingType.DRAW_CIRCLE; 
    public int circleRadius = 3;

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
            transform.RotateAround(transform.position, Vector3.up, 0.6f);
        }
        
    }

    public int circleCount = 6;
    //使用LINE_STRIP
    private void DrawCircle()
    {
        float angleDelta = 2 * Mathf.PI / circleCount;

        GL.Begin(GL.LINE_STRIP);


        for (int i = 0; i < circleCount + 1; i++)
        {
            float angle = angleDelta * i;
            float angleNext = angle + angleDelta;
            if (i % 2 == 0)
            {
                GL.Color(Color.red);
            }
            else
            {
                GL.Color(Color.green);
            }

            GL.Vertex3(Mathf.Cos(angle) * circleRadius, Mathf.Sin(angle) * circleRadius, 0);
        }

        GL.End();
    }


    public int triangleSize = 2;
    //使用TRIANGLES进行绘制三角形
    private void DrawTriangle()
    {
        GL.Begin(GL.TRIANGLES);
        //顺时针
        GL.Color(Color.red);
        GL.Vertex3(-triangleSize, -triangleSize, 0);//左下
        GL.Color(Color.green);
        GL.Vertex3(0, triangleSize, 0);//上     
        GL.Color(Color.blue);
        GL.Vertex3(triangleSize, -triangleSize, 0);//右下
        GL.End();
    }


    private void DrawTriangles()
    {
        GL.Begin(GL.TRIANGLE_STRIP);
        GL.Color(Color.red);
        GL.Vertex3(-triangleSize, -triangleSize, 0);
        GL.Color(Color.green);
        GL.Vertex3(0, triangleSize, 0);
        GL.Color(Color.blue);
        GL.Vertex3(triangleSize, -triangleSize, 0);
        GL.Color(Color.yellow);
        GL.Vertex3(triangleSize, triangleSize, 0);
        /*GL.Color(Color.gray);
        GL.Vertex3(2*triangleSize, triangleSize, 0);*/
        GL.End();
    }

    public int quadSize = 2;
    //使用QUADS绘制四边形
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

    //画立方体
    private void DrawCube()
    {
        GL.Begin(GL.QUADS);

        //正面
        GL.Color(Color.red);
        GL.Vertex3(-quadSize, -quadSize, -quadSize);
        GL.Vertex3(quadSize, -quadSize, -quadSize);
        GL.Vertex3(quadSize, quadSize, -quadSize);
        GL.Vertex3(-quadSize, quadSize, -quadSize);

        GL.Color(Color.green);
        GL.Vertex3(-quadSize, -quadSize, quadSize);
        GL.Vertex3(quadSize, -quadSize, quadSize);
        GL.Vertex3(quadSize, quadSize, quadSize);
        GL.Vertex3(-quadSize, quadSize, quadSize);

        GL.Color(Color.blue);
        GL.Vertex3(quadSize, -quadSize, -quadSize);
        GL.Vertex3(quadSize, -quadSize, quadSize);
        GL.Vertex3(quadSize, quadSize, quadSize);
        GL.Vertex3(quadSize, quadSize, -quadSize);

        GL.Color(Color.yellow);
        GL.Vertex3(-quadSize, -quadSize, -quadSize);
        GL.Vertex3(-quadSize, -quadSize, quadSize);
        GL.Vertex3(-quadSize, quadSize, quadSize);
        GL.Vertex3(-quadSize, quadSize, -quadSize);

        //底面
        GL.Color(Color.gray);
        GL.Vertex3(-quadSize, -quadSize, -quadSize);
        GL.Vertex3(-quadSize, -quadSize, quadSize);
        GL.Vertex3(quadSize, -quadSize, quadSize);
        GL.Vertex3(quadSize, -quadSize, -quadSize);


        //顶面
        GL.Color(Color.black);
        GL.Vertex3(-quadSize, quadSize, -quadSize);
        GL.Vertex3(quadSize, quadSize, -quadSize);
        GL.Vertex3(quadSize, quadSize, quadSize);
        GL.Vertex3(-quadSize, quadSize, quadSize);

        GL.End();
    }


    private void OnRenderObject()
    {
        SetMaterialPass();

        GL.PushMatrix();
        GL.MultMatrix(transform.localToWorldMatrix);
        switch (type)
        {
            case DrawingType.DRAW_CIRCLE:
                DrawCircle();
                break;
            case DrawingType.DRAW_TRIANGLE:
                DrawTriangle();
                break;
            case DrawingType.DRAW_TRINAGLES:
                DrawTriangles();
                break;
            case DrawingType.DRAW_CUBE:
                DrawCube();
                break;
        }
        GL.PopMatrix();
    }

     
}