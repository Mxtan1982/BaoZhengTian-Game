using System.Collections.Generic;
using UnityEngine;

namespace BaoZhengTian.Game
{
    /// <summary>
    /// 对话/视觉小说系统
    /// </summary>
    public class DialogueSystem : MonoBehaviour
    {
        [System.Serializable]
        public class DialogueNode
        {
            public int nodeId;
            public string speakerName;              // 说话人名称
            public string dialogueText;             // 对话内容
            public List<DialogueChoice> choices;    // 选择项
            public int nextNodeId;                  // 下一个节点 ID
        }

        [System.Serializable]
        public class DialogueChoice
        {
            public int choiceId;
            public string choiceText;               // 选择文字
            public int targetNodeId;                // 目标节点
        }

        private Dictionary<int, DialogueNode> dialogueDatabase = new Dictionary<int, DialogueNode>();
        private int currentNodeId = -1;
        private DialogueNode currentNode;

        private void Start()
        {
            InitializeDialogues();
        }

        /// <summary>
        /// 初始化对话系统
        /// </summary>
        private void InitializeDialogues()
        {
            // 创建默认对话
            CreateDefaultDialogues();
            Debug.Log($"对话系统已初始化，共 {dialogueDatabase.Count} 个对话节点");
        }

        /// <summary>
        /// 创建默认对话
        /// </summary>
        private void CreateDefaultDialogues()
        {
            // 开场对话
            DialogueNode node1 = new DialogueNode
            {
                nodeId = 1,
                speakerName = "包青天",
                dialogueText = "本官包青天，今日又有新案需要审理...",
                choices = new List<DialogueChoice>
                {
                    new DialogueChoice { choiceId = 1, choiceText = "开始查案", targetNodeId = 2 },
                    new DialogueChoice { choiceId = 2, choiceText = "查看档案", targetNodeId = 3 }
                },
                nextNodeId = -1
            };

            DialogueNode node2 = new DialogueNode
            {
                nodeId = 2,
                speakerName = "包青天",
                dialogueText = "本案涉及多个嫌疑人，我需要仔细调查...",
                choices = new List<DialogueChoice>
                {
                    new DialogueChoice { choiceId = 1, choiceText = "讯问嫌疑人", targetNodeId = 4 },
                    new DialogueChoice { choiceId = 2, choiceText = "搜集证据", targetNodeId = 5 }
                },
                nextNodeId = -1
            };

            DialogueNode node3 = new DialogueNode
            {
                nodeId = 3,
                speakerName = "包青天",
                dialogueText = "让我再仔细看看这些档案...",
                choices = new List<DialogueChoice>
                {
                    new DialogueChoice { choiceId = 1, choiceText = "返回", targetNodeId = 1 }
                },
                nextNodeId = -1
            };

            DialogueNode node4 = new DialogueNode
            {
                nodeId = 4,
                speakerName = "包青天",
                dialogueText = "我要亲自讯问这些可疑人物...",
                choices = new List<DialogueChoice>
                {
                    new DialogueChoice { choiceId = 1, choiceText = "返回", targetNodeId = 2 }
                },
                nextNodeId = -1
            };

            DialogueNode node5 = new DialogueNode
            {
                nodeId = 5,
                speakerName = "包青天",
                dialogueText = "每一件证据都是破案的关键...",
                choices = new List<DialogueChoice>
                {
                    new DialogueChoice { choiceId = 1, choiceText = "返回", targetNodeId = 2 }
                },
                nextNodeId = -1
            };

            dialogueDatabase[1] = node1;
            dialogueDatabase[2] = node2;
            dialogueDatabase[3] = node3;
            dialogueDatabase[4] = node4;
            dialogueDatabase[5] = node5;
        }

        /// <summary>
        /// 开始对话
        /// </summary>
        public void StartDialogue(int startNodeId)
        {
            if (dialogueDatabase.ContainsKey(startNodeId))
            {
                currentNodeId = startNodeId;
                currentNode = dialogueDatabase[startNodeId];
                Debug.Log($"[{currentNode.speakerName}] {currentNode.dialogueText}");
            }
            else
            {
                Debug.LogWarning($"对话节点 {startNodeId} 不存在");
            }
        }

        /// <summary>
        /// 获取当前对话内容
        /// </summary>
        public DialogueNode GetCurrentNode()
        {
            return currentNode;
        }

        /// <summary>
        /// 选择对话选项
        /// </summary>
        public void SelectChoice(int choiceId)
        {
            if (currentNode == null)
            {
                Debug.LogWarning("没有正在进行的对话");
                return;
            }

            DialogueChoice selectedChoice = null;
            foreach (DialogueChoice choice in currentNode.choices)
            {
                if (choice.choiceId == choiceId)
                {
                    selectedChoice = choice;
                    break;
                }
            }

            if (selectedChoice != null)
            {
                int targetNodeId = selectedChoice.targetNodeId;
                if (targetNodeId > 0 && dialogueDatabase.ContainsKey(targetNodeId))
                {
                    currentNodeId = targetNodeId;
                    currentNode = dialogueDatabase[targetNodeId];
                    Debug.Log($"[{currentNode.speakerName}] {currentNode.dialogueText}");
                }
            }
            else
            {
                Debug.LogWarning($"选择项 {choiceId} 不存在");
            }
        }

        /// <summary>
        /// 结束对话
        /// </summary>
        public void EndDialogue()
        {
            currentNodeId = -1;
            currentNode = null;
            Debug.Log("对话已结束");
        }

        /// <summary>
        /// 添加对话节点
        /// </summary>
        public void AddDialogueNode(DialogueNode node)
        {
            dialogueDatabase[node.nodeId] = node;
        }

        /// <summary>
        /// 获取对话节点
        /// </summary>
        public DialogueNode GetDialogueNode(int nodeId)
        {
            if (dialogueDatabase.ContainsKey(nodeId))
            {
                return dialogueDatabase[nodeId];
            }
            return null;
        }
    }
}
