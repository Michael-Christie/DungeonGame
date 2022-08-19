using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIManager Instance { get; private set; }

    private List<BaseEnemyAI> activeAIAgents = new List<BaseEnemyAI>();

    private int agentCount = 0;

    //
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        for(int i = 0; i < agentCount; i++)
        {
            activeAIAgents[i].OnEntityUpdate(Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < agentCount; i++)
        {
            activeAIAgents[i].OnEntityFixedUpdate(Time.fixedDeltaTime);
        }
    }

    public void RegisterAgent(params BaseEnemyAI[] _agent)
    {
        for (int i = 0; i < _agent.Length; i++)
        {
            activeAIAgents.Add(_agent[i]);
        }

        agentCount = activeAIAgents.Count;
    }

    public void UnRegisterAgent(BaseEnemyAI _agent)
    {
        activeAIAgents.Remove(_agent);

        agentCount = activeAIAgents.Count;
    }
}
