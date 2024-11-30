using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AICube : BattleCube
{    
    private List<OrientPoint> _orientPoints;
    private OrientPoint _player;
    
    private OrientPoint _currentTargetPoint;
    
    private bool _isWrongDirection = false;
    private bool _isPlayerFinded = false;

    void Update()
    {
        if (_isGameStarted)
        {
            CorrectAngle();
            if (_isPlayerFinded == false)
                FindPlayer();
        }            
    }  

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == _currentTargetPoint.name)
            FindNewPoint();
    }

    public void Initialize(List<OrientPoint> orientPoints, OrientPoint player, Vector3 startPosition)
    {
        _orientPoints = orientPoints;
        _player = player;
        base.Initialize(startPosition);
    }

    public override void StartGame()
    {
        base.StartGame();
        FindNewPoint();                             
    }

    private void FindNewPoint()
    {
        List<float> angles = new List<float>();

        foreach (OrientPoint point in _orientPoints)
        {
            Vector3 targetDir = point.transform.position - transform.position;
            float angle = Vector3.Angle(targetDir, transform.forward);
            point.Angle = angle;
            point.Distance = Vector3.Distance(transform.position, point.transform.position);
        }

        List<OrientPoint> SortedList = _orientPoints.OrderBy(o => o.Angle).ToList();
        List<OrientPoint> possiblePoints = new List<OrientPoint>();

        foreach (OrientPoint point in SortedList)         
            if (point.Distance < 20 && point != _currentTargetPoint)
            {
                if(IsOrientPointCanReached(point, "Wall") == false)    
                    possiblePoints.Add(point);                
            }        

        System.Random random = new System.Random();
        _currentTargetPoint = possiblePoints[random.Next(0, possiblePoints.Count/2)];
        FullForward();
        _isWrongDirection = true;
    }

    private void CorrectAngle()
    {        
        Vector3 targetDir = _currentTargetPoint.transform.position - transform.position;
        float angle = Vector3.SignedAngle(targetDir, transform.forward, transform.up);

        Vector3 vector = Vector3.Cross(transform.forward, targetDir);        
        
        if (angle > 3 )
            RotateLeft();
        else if(angle < -3)
            RotateRight();
        else
        {
            StopRotate();
            _isWrongDirection=false;
        }            
    }

    private void FindPlayer()
    {
        if (IsOrientPointCanReached(_player, "Player"))
        {
            _isPlayerFinded = true;
            _currentTargetPoint = _player;
            FireToEnemy();
        }
    }

    private bool IsOrientPointCanReached(OrientPoint point, string nameContain)
    {
        Vector3 direct = point.transform.position - transform.position;
        Ray ray = new Ray(transform.position, direct);
        //Debug.DrawRay(transform.position, direct, UnityEngine.Color.black, 25);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))        
            if (hitInfo.transform.gameObject.name.Contains(nameContain))            
                return true;
            
        return false;        
    }

    private void FireToEnemy()
    {
        StartCoroutine(PushBullets());
    }

    IEnumerator PushBullets()
    {
        yield return IsPlayerRightAhead();
        if (IsOrientPointCanReached(_player, "Player"))        
            for (int i = 0; i < 4; i++)
            {
                Fire();
                yield return new WaitForSeconds(0.3f);
            }        
        
        FindNewPoint();
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(3);
        _isPlayerFinded = false;
    }

    private IEnumerator IsPlayerRightAhead()
    {
        _isWrongDirection = true;
        yield return new WaitUntil(() => _isWrongDirection == false);
    }
}
