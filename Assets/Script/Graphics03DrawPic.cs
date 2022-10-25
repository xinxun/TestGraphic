using UnityEngine;

public class Graphics03DrawPic : MonoBehaviour
{
    public enum SpaceType
    {
        WORLD_LOCAL,
        ORTHO_PIXEL,
        VIEWPORT
    }

    private Material _lineMaterial;//视口十字线
    public Material _materialPic;//画图所使用的Pic
    public float _PicSize = 5;//图片的大小

    public float circleRadiusPic = 5.0f;//圆形大小
    public int circleTrianCount = 10;//所使用的三角形个数
    public int showCircleTrianCount = 10;//显示的三角形个数

    public Material _materialFrontPic; //正面的图形
    public Material _materialBackPic; //背面的图形
    public float _doubleFaceQuadsSize = 5;//


    

    private void Start()
    {
        InitMaterial();
    }

    private void Update()
    {
        transform.RotateAround(transform.position, transform.up, 2.0f);
    }

    private void InitMaterial()
    {
        if (_lineMaterial == null)
        {
            _lineMaterial = new Material(Shader.Find("Hidden/Internal-Colored"));
        }

    }

    private void OnRenderObject()
    {
        DrawViewPortLine();
        //左上角视口
        GL.Viewport(new Rect(0, Screen.height / 2, Screen.width / 2, Screen.height / 2));
        //绘制正方形图片
        DrawPic();

        //右上角
        GL.Viewport(new Rect(Screen.width / 2, Screen.height / 2, Screen.width / 2, Screen.height / 2));
        //绘制圆形图片
        DrawCirclePic();


        //左下角
        GL.Viewport(new Rect(0, 0, Screen.width / 2, Screen.height / 2));
        DrawSignelFacePic();
        


        //右下角
        GL.Viewport(new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height / 2));
        DrawDoubleFacePic();


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


    //画图
    private void DrawPic()
    {

        _materialPic.SetPass(0);
       
        GL.PushMatrix();
        GL.Begin(GL.QUADS);

        GL.Color(Color.white);
        GL.TexCoord2(0, 0);
        GL.Vertex3( -_PicSize, -_PicSize, 0);
        GL.TexCoord2(0, 1);
        GL.Vertex3(-_PicSize, _PicSize, 0);
        GL.TexCoord2(1, 1);
        GL.Vertex3(_PicSize, _PicSize, 0);
        GL.TexCoord2(1, 0);
        GL.Vertex3(_PicSize, -_PicSize, 0);
   
        GL.End();
        GL.PopMatrix();

    }


    //画圆形图片
    private void DrawCirclePic()
    {
        if(_materialPic == null)
        {
            return;
        }
        _materialPic.SetPass(0);
        GL.PushMatrix();

        GL.Begin(GL.TRIANGLES);
        GL.Color(Color.white);

        float angleDelta = 2 * Mathf.PI / circleTrianCount;
        for (int i = 1; i <= showCircleTrianCount; i++)
        {
            float angle = angleDelta * i;
            float angleNext = angle - angleDelta;
            
            float u1 = Mathf.Cos(angle) / 2 + 0.5f;
            float v1 = Mathf.Sin(angle) / 2 + 0.5f;
            float x1 = Mathf.Cos(angle) * circleRadiusPic;
            float y1 = Mathf.Sin(angle) * circleRadiusPic;
            
            float u2 = Mathf.Cos(angleNext) / 2 + 0.5f;
            float v2 = Mathf.Sin(angleNext) / 2 + 0.5f;
            float x2 = Mathf.Cos(angleNext) * circleRadiusPic;
            float y2 = Mathf.Sin(angleNext) * circleRadiusPic;
           
            GL.TexCoord2(0.5f, 0.5f);
            GL.Vertex3(0f, 0f, 0);
            //UV映射1
            GL.TexCoord2(u1, v1);
            GL.Vertex3(x1, y1, 0);
            //UV映射2
            GL.TexCoord2(u2, v2);
            GL.Vertex3(x2, y2, 0);

            Debug.Log(i+"uv:" + angleDelta + "uv1(" + u1 + "," + v1 + "),uv2(" + u2 + "," + v2 + ")");
            Debug.Log(i+"angleDelta:" + angleDelta + "p1:(0,0)" + "p2(" + x1 + "," + y1 + "),p3(" + x2 + "," + y2 + ")");
        }
        GL.End();
        GL.PopMatrix();
    }

    private void DrawSignelFacePic()
    {
        if (_materialFrontPic != null)
        {
            _materialFrontPic.SetPass(0);
        }
        GL.PushMatrix();
        // 正面
        GL.Begin(GL.QUADS);
        //使用世界坐标系，相当于脚本挂接对象的子对象
        GL.MultMatrix(transform.localToWorldMatrix);

        GL.TexCoord2(0, 0);
        GL.Vertex3(-_doubleFaceQuadsSize, -_doubleFaceQuadsSize, 0);
        GL.TexCoord2(0, 1);
        GL.Vertex3(-_doubleFaceQuadsSize, _doubleFaceQuadsSize, 0);
        GL.TexCoord2(1, 1);
        GL.Vertex3(_doubleFaceQuadsSize, _doubleFaceQuadsSize, 0);
        GL.TexCoord2(1, 0);
        GL.Vertex3(_doubleFaceQuadsSize, -_doubleFaceQuadsSize, 0);

        GL.End();
        GL.PopMatrix();

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

    private void DrawDoubleFacePic()
    {

        if(_materialFrontPic != null)
        {
            _materialFrontPic.SetPass(0);
        }
        GL.PushMatrix();
        // 正面
        GL.Begin(GL.QUADS);
        GL.MultMatrix(transform.localToWorldMatrix);

        GL.TexCoord2(0, 0);
        GL.Vertex3(-_doubleFaceQuadsSize, -_doubleFaceQuadsSize, 0);
        GL.TexCoord2(0, 1);
        GL.Vertex3(-_doubleFaceQuadsSize, _doubleFaceQuadsSize, 0);
        GL.TexCoord2(1, 1);
        GL.Vertex3(_doubleFaceQuadsSize, _doubleFaceQuadsSize, 0);
        GL.TexCoord2(1, 0);
        GL.Vertex3(_doubleFaceQuadsSize, -_doubleFaceQuadsSize, 0);

        GL.End();
        GL.PopMatrix();


        if (_materialBackPic != null)
        {
            _materialBackPic.SetPass(0);
        }
        GL.PushMatrix();
        //反面
        GL.Begin(GL.QUADS);
        GL.MultMatrix(transform.localToWorldMatrix);
        GL.Color(Color.white);
        GL.TexCoord2(0, 0);
        GL.Vertex3(-_doubleFaceQuadsSize, -_doubleFaceQuadsSize, 0);
        GL.TexCoord2(1, 0);
        GL.Vertex3(_doubleFaceQuadsSize, -_doubleFaceQuadsSize, 0);
        GL.TexCoord2(1, 1);
        GL.Vertex3(_doubleFaceQuadsSize, _doubleFaceQuadsSize, 0);
        GL.TexCoord2(0, 1);
        GL.Vertex3(-_doubleFaceQuadsSize, _doubleFaceQuadsSize, 0);
        GL.End();

        GL.PopMatrix();
    }

}

