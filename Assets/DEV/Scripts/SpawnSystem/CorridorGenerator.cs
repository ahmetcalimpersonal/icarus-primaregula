﻿
using UnityEngine;

public class CorridorGenerator : MonoBehaviour
{
    
    public Vector3 currPosition;
    public Transform allTunnel;
    public NodeGraph nodeGraph;
    public Rigidbody rb;
    public Player player;
    public void Create(KNode k,bool isHeadNode)
    {
        if (isHeadNode)
        {
            k.gameObject.transform.localPosition = new Vector3(currPosition.x, currPosition.y, currPosition.z + k.scaleFactor);
            currPosition = k.gameObject.transform.localPosition;
            nodeGraph.tailNode.nextNode = k;
            k.prevNode = nodeGraph.tailNode;
            nodeGraph.tailNode = k;
            nodeGraph.headNode = nodeGraph.headNode.nextNode;
            nodeGraph.tailNode.nextNode = null;
        }
        else
        {
            GameObject g = Instantiate(k.gameObject,allTunnel);            
            currPosition = new Vector3(currPosition.x, currPosition.y, currPosition.z + k.scaleFactor);
            g.transform.localPosition = currPosition;            
            KNode currentNode= g.GetComponent<KNode>();
            nodeGraph.tailNode.nextNode = currentNode;
            currentNode.prevNode = nodeGraph.tailNode;
            nodeGraph.tailNode = currentNode;
            nodeGraph.headNode = nodeGraph.headNode.nextNode;
            Destroy(nodeGraph.headNode.prevNode.gameObject);
            nodeGraph.tailNode.nextNode = null;
            SpawnObstacle(g.transform,k.scaleFactor);
            SpawnObstacle(g.transform, k.scaleFactor);
        }
    }

    void FixedUpdate()
    {
        MoveTunnel();
    }
    void MoveTunnel()
    { 
        rb.velocity = Player.IsGameRunning
            ? -transform.forward * Player.VelocityZBase * FindObjectOfType<PowerUpController>().phaseMaxTimeMultipleValue
            : Vector3.zero;

    }
    public GameObject laserObstacle_Vertical_Red, laserObstacle_Horizantal_Red,
        laserObstacle_Vertical_Green, laserObstacle_Horizantal_Green,powerUp_phase,powerUp_roket, powerUp_bulletTime;

    void SpawnObstacle(Transform parentTransform,float nodeScale)
    {
        int i = Random.Range(0,16);

        switch (i)
        {
            case 0:
                break;
            case 1:
                InstantiateLaser(laserObstacle_Vertical_Red,true,parentTransform,nodeScale);
                break;
            case 2:
                InstantiateLaser(laserObstacle_Horizantal_Red, false, parentTransform, nodeScale);
                break;
            case 3:
                InstantiateLaser(laserObstacle_Vertical_Green, true, parentTransform, nodeScale);
                break;
            case 4:
                InstantiateLaser(laserObstacle_Horizantal_Green, false, parentTransform, nodeScale);
                break;
            case 5:
                InstantiateObstacle(powerUp_phase, parentTransform);
                break;
            case 6:
                InstantiateObstacle(powerUp_roket, parentTransform);
                break;
            case 7:
                InstantiateObstacle(powerUp_bulletTime, parentTransform);
                break;
                break;
            default:
                break;

        }
    }

    void InstantiateLaser(GameObject laser,bool isVerticalAxis,Transform parentTransform,float nodeScale)
    {
        if (isVerticalAxis)
        {
            GameObject laserObj = Instantiate(laser, parentTransform);
            laserObj.transform.localPosition = new Vector3(Random.Range(6, 21), laserObj.transform.localPosition.y, -nodeScale / 2);          
        }
        else
        {
            GameObject laserObj = Instantiate(laser, parentTransform);
            laserObj.transform.localPosition = new Vector3(laserObj.transform.localPosition.x, Random.Range(3, 17), -nodeScale / 2);
        }

    }
    void InstantiateObstacle(GameObject powerUp, Transform parentTransform)
    {
        if (Random.Range(0,2)==1)
        {
            GameObject laserObj = Instantiate(powerUp, parentTransform);
            laserObj.transform.localPosition = new Vector3(Random.Range(6, 20), Random.Range(6, 10), 0);
        }    
    }


}
