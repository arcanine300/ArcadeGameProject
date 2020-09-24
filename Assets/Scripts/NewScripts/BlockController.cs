using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class BlockController : MonoBehaviour, IMapGimmick
{
    [SerializeField]
    private GameObject _blocks;
    [SerializeField]
    private float _gapTimerLength = 5.0f;
    [SerializeField]
    private float _blockGoneTime = 5.0f;
    [SerializeField]
    private int _amountOfBlocksToDropAtATime = 4;
    [SerializeField]
    private Material _black;
    [SerializeField]
    private Material _other;


    private List<GameObject> _blockNodes;
    private bool _isActive;
    private List<GameObject> _blocksToDrop;


    private void Awake()
    {
        if (_blocks != null)
        {
            _blockNodes = new List<GameObject>();
            foreach (Transform block in _blocks.transform)
            {
                if (block.name == "Node")
                {
                    _blockNodes.Add(block.gameObject);
                }
            }
            Debug.Log($"Loaded {_blockNodes.Count} blocks.");
        }
        _isActive = false;
    }

    public void Begin()
    {
        _isActive = true;
    }

    public void Stop()
    {
        Debug.Log("abstract stop");
    }

    public void UseItem()
    {
    }

    private void Update()
    {
        if (_isActive)
        {
            _blocksToDrop = GetBlocksToDrop();
            foreach (var block in _blocksToDrop)
            {
                StartCoroutine(BlockLife(block));
                _isActive = false;
            }
        }

        if (_blocksToDrop != null)
        {
            if (_blocksToDrop.Count == 0)
            {
                _isActive = true;
            }
        }
    }

    private List<GameObject> GetBlocksToDrop()
    {
        List<GameObject> blocks = new List<GameObject>();

        for (int i = 0; i < _amountOfBlocksToDropAtATime; i++)
        {
            GameObject block = _blockNodes[Random.Range(0, _blockNodes.Count)];
            if (!block.GetComponent<AbstractBlockNode>().IsGone)
            {
                block.GetComponent<AbstractBlockNode>().IsGone = true;
                blocks.Add(block);
            }
            else
            {
                i--;
            }
        }
        return blocks;
    }

    private IEnumerator BlockLife(GameObject block)
    {
        GameObject nodeBlock = block.GetComponent<AbstractBlockNode>().Block;
        Renderer nodeBlockRenderer = nodeBlock.GetComponent<Renderer>();
        Animator nodeBlockAnimator = block.GetComponent<AbstractBlockNode>().BlockAnimator;
        GameObject obstacle = block.GetComponent<AbstractBlockNode>().Obstacle;
        bool isBlack = true;


        for (int i = 0; i < 10; i++)
        {
            if (isBlack)
            {
                nodeBlockRenderer.material = _other;
                isBlack = false;
            }
            else
            {
                nodeBlockRenderer.material = _black;
                isBlack = true;
            }

            if (i < 5)
            {
                yield return new WaitForSeconds(0.3f);
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }

        StartCoroutine(StartBlock(nodeBlock, nodeBlockRenderer, nodeBlockAnimator, obstacle)); ;
        yield return new WaitForSeconds(_blockGoneTime + 1);
        StartCoroutine(FinishBlock(block, nodeBlock, nodeBlockAnimator, obstacle)); ;

        // switch materials to show indicator it will disappear
        // animation to drop the block
        // disable the block and enable the nav mesh obstacle
        // wait the alloted time
        // enable the block and animation to place block down
        // disable the nav mesh obstacle
    }

    private IEnumerator StartBlock(GameObject nodeBlock, Renderer nodeBlockRenderer, Animator nodeBlockAnimator, GameObject obstacle)
    {
        nodeBlockRenderer.material = _black;
        obstacle.GetComponent<NavMeshObstacle>().enabled = true;
        nodeBlockAnimator.Play("BlockFall");        
        yield return new WaitForSeconds(1.0f);
        nodeBlock.SetActive(false);
    }

    private IEnumerator FinishBlock(GameObject block, GameObject nodeBlock, Animator nodeBlockAnimator, GameObject obstacle)
    {
        nodeBlock.SetActive(true);
        nodeBlockAnimator.Play("BlockPlace");
        yield return new WaitForSeconds(1.0f);
        obstacle.GetComponent<NavMeshObstacle>().enabled = false;
        block.GetComponent<AbstractBlockNode>().IsGone = false;
        _blocksToDrop.Remove(block);
    }
}
