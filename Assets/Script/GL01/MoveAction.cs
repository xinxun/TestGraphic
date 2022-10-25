using UnityEngine;

public class MoveAction : MonoBehaviour
{
    public GameObject _moveObj = null;
    public float _moveSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (null == _moveObj)
        {
            return;
        }
        float fTime = Time.deltaTime;
        float fMovePosX = fTime * _moveSpeed;
        Vector3 posAdd = new Vector3(fTime * _moveSpeed, 0, 0);
        _moveObj.transform.localPosition = _moveObj.transform.localPosition + posAdd;
        if(_moveObj.transform.localPosition.x > 8)
        {
            _moveObj.transform.localPosition = Vector3.zero;
        }

    }
}
