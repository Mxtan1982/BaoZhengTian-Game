using System;
using System.Collections.Generic;
using UnityEngine;

namespace BaoZhengTian.Data
{
    /// <summary>
    /// 案件数据定义
    /// </summary>
    [System.Serializable]
    public class CaseData
    {
        public int caseId;                          // 案件ID
        public string caseName;                     // 案件名称
        public string caseDescription;              // 案件描述
        public int difficultyLevel;                 // 难度等级 (1-3)
        public List<string> suspects;               // 嫌疑人列表
        public string correctSuspect;               // 真凶
        public List<EvidenceData> evidenceList;     // 证据列表
        public string caseStory;                    // 案件故事/背景
        public int rewardPoints;                    // 破案奖励积分
        public int penaltyPoints;                   // 破案失败惩罚积分
    }

    /// <summary>
    /// 证据数据定义
    /// </summary>
    [System.Serializable]
    public class EvidenceData
    {
        public int evidenceId;                      // 证据ID
        public string evidenceName;                 // 证据名称
        public string evidenceDescription;          // 证据描述
        public string relatedSuspect;               // 相关嫌疑人
        public bool isFound;                        // 是否已找到
        public int findLocation;                    // 发现位置ID
    }

    /// <summary>
    /// 玩家进度数据
    /// </summary>
    [System.Serializable]
    public class PlayerProgress
    {
        public int currentRank;                     // 当前品级 (1-5)
        public int totalPoints;                     // 总积分
        public int casesCompleted;                  // 完成案件数
        public int casesCorrect;                    // 正确破案数
        public List<int> completedCaseIds;          // 已完成案件列表
        public int lastPlayedCaseId;                // 最后玩的案件ID
    }
}
