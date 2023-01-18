
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
    private float _difficultyOffset;
    private Rigidbody2D _rbBall;
    private bool _isMoving;
    
    void Start()
    {
        _rbBall = GameObject.FindWithTag("Ball").GetComponent<Rigidbody2D>();
        
        if(difficulty == DifficultyLevel.EASY)
            _difficultyOffset = 1.5f;
        if(difficulty == DifficultyLevel.NORMAL)
            _difficultyOffset = 0.75f;
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
            if (!_isMoving)
            {
                _isMoving = true;
                if (difficulty != DifficultyLevel.HARD)
                    ballPositionY = Random.Range(ballPositionY - _difficultyOffset, ballPositionY + _difficultyOffset);
                
            }
            
            if (ballPositionY > _rbPaddle.position.y)
                SetVelocity(Vector2.up);
            else if (ballPositionY < _rbPaddle.position.y)
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