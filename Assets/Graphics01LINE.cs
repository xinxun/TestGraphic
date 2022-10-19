using UnityEngine;
//使用GL绘制Line，并了解Batch与SetPass在GL中的使用
public class Graphics01LINE : MonoBehaviour
{
    //使用GL绘制线条的个数
    public int _lineCount = 20;
    //GL绘制线条的半径
    public float _radius = 3;
    //是否使用单批沉浸
    public bool _useOneBatch = false;
    //坐标系使用独立Pass
    public bool _useMorePass = false;

    //画线使用的材质
    private Material _lineMaterial;
    //坐标轴材质
    private Material _coordinateMaterial;


    private void Start()
    {
        //初始化所有Material
        InitMaterial();
    }

    //OnRenderObject 在摄像机渲染场景后调用
    private void OnRenderObject()
    {
        if(_useOneBatch)
        {
            //使用一个批次画线
            DrawLinesByOneBatch();
           
        }
        else
        {
            //使用多个批次画线
            DrawLinesByMoreBatch();
        }


        //画坐标系
        DrawCoordinate();
        
       
    }

    private void InitMaterial()
    {
        if (_lineMaterial == null)
        {
            _lineMaterial = new Material(Shader.Find("Hidden/Internal-Colored"));
        }

        if (_coordinateMaterial == null)
        {
            _coordinateMaterial = new Material(Shader.Find("Unlit/Color"));
        }

    }

    
    //使用一个批次画线
    private void DrawLinesByOneBatch()
    {
        if(_lineMaterial != null)
        {
            _lineMaterial.SetPass(0);//使用lineMater
        }
        
        //开始画线
        GL.Begin(GL.LINES);

        //多少角度
        float angleDelta = 2 * Mathf.PI / _lineCount;

        for (int i = 0; i < _lineCount; i++)
        {
            float angle = angleDelta * i;
            GL.Color(new Color((float)i / _lineCount, 1 - (float)i / _lineCount, 1, 1));
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(Mathf.Cos(angle) * _radius, Mathf.Sin(angle) * _radius, 0);
        }

        GL.End();
        //Begin End之前就是使用一个批次画线
    }

    //使用多个批次画线
    private void DrawLinesByMoreBatch()
    {
        if (_lineMaterial != null)
        {
            _lineMaterial.SetPass(0);//使用lineMater
        }

        //多少角度
        float angleDelta = 2 * Mathf.PI / _lineCount;

        for (int i = 0; i < _lineCount; i++)
        {
            //开始画线
            GL.Begin(GL.LINES);
            float angle = angleDelta * i;
            GL.Color(new Color((float)i / _lineCount, 1 - (float)i / _lineCount, 1, 1));
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(Mathf.Cos(angle) * _radius, Mathf.Sin(angle) * _radius, 0);
            GL.End();
            //Begin End之前就是使用一个批次画线
        }
    }

    //绘制坐标系
    private void DrawCoordinate()
    {
        if(_useMorePass && _coordinateMaterial !=null)
        {
            _lineMaterial.SetPass(0);//使用lineMater
        }

        //开始画线
        GL.Begin(GL.LINES);

        GL.Color(Color.red);
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(0, 100, 0);

        GL.Color(Color.green);
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(100 , 0, 0);

        GL.Color(Color.blue);
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(0, 0, 100 );
        GL.End();
    }
}