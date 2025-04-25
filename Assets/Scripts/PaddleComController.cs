
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PaddleComController : PaddleController
{
    private enum DifficultyLevel{ EASY,NORMAL,HARD }

    [SerializeField, HideInInspector] private DifficultyLevel difficulty;

    [SerializeField] private bool isCom;
    [SerializeField] private float _difficultyOffset;
    private Rigidbody2D _rbBall;
    private bool _isMoving;
    
    void Start()
    {
        _rbBall = GameObject.FindWithTag("Ball").GetComponent<Rigidbody2D>();
        return;
        if(difficulty == DifficultyLevel.EASY)
            _difficultyOffset = transform.localScale.y + 3f;
        if(difficulty == DifficultyLevel.NORMAL)
            _difficultyOffset = transform.localScale.y + 1.5f;
        if(difficulty == DifficultyLevel.HARD)
            _difficultyOffset = 0f;
    }

    protected override void Update()
    {
        if(!isCom)
            base.Update();
    }
    
    protected override void FixedUpdate()
    {
        if(isCom)
            CPUMove();
        
        base.FixedUpdate();
    }

    private void CPUMove()
    {
        if (_rbBall.velocity.x > 0f)
        {
            var ballPositionY = _rbBall.position.y;
            var paddlePositionY = _rbPaddle.position.y;
            if (!_isMoving)
            {
                _isMoving = true;
                if (difficulty != DifficultyLevel.HARD)
                    paddlePositionY = Random.Range(paddlePositionY - _difficultyOffset, paddlePositionY + _difficultyOffset);
                
            }
            
            if (ballPositionY > paddlePositionY)
                SetVelocity(Vector2.up);
            else if (ballPositionY < paddlePositionY)
                SetVelocity(Vector2.down);
        }
        else
        {
            _isMoving = false;
            var paddlePosition = _rbPaddle.position; 
            _rbPaddle.position = Vector2.Lerp(paddlePosition, new Vector2(paddlePosition.x, 0f), speed * Time.fixedDeltaTime);
        }
    }


    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(PaddleComController))]
    public class PaddleComControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            SerializedProperty isCom = serializedObject.FindProperty("isCom");
            SerializedProperty difficulty = serializedObject.FindProperty("difficulty");

            if (isCom.boolValue)
            {
                difficulty.isExpanded = true;
                EditorGUILayout.PropertyField(difficulty);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
    #endregion
}