using UnityEngine;
using BaoZhengTian.Data;
using BaoZhengTian.Core;

namespace BaoZhengTian.Game
{
    /// <summary>
    /// 案件系统 - 处理案件的审理逻辑
    /// </summary>
    public class CaseSystem : MonoBehaviour
    {
        private CaseData currentCase;
        private int foundEvidenceCount = 0;
        private string playerConclusion = "";

        /// <summary>
        /// 初始化案件
        /// </summary>
        public void InitializeCase(int caseId)
        {
            DataManager dataManager = GameManager.Instance.GetComponent<DataManager>();
            currentCase = dataManager.GetCaseData(caseId);

            if (currentCase != null)
            {
                foundEvidenceCount = 0;
                playerConclusion = "";
                Debug.Log($"案件已初始化: {currentCase.caseName}");
                Debug.Log($"案件背景: {currentCase.caseStory}");
                Debug.Log($"嫌疑人: {string.Join(", ", currentCase.suspects)}");
            }
            else
            {
                Debug.LogError($"无法初始化案件 ID: {caseId}");
            }
        }

        /// <summary>
        /// 获取当前案件
        /// </summary>
        public CaseData GetCurrentCase()
        {
            return currentCase;
        }

        /// <summary>
        /// 收集证据
        /// </summary>
        public bool CollectEvidence(int evidenceId)
        {
            if (currentCase == null)
            {
                Debug.LogError("没有正在进行的案件");
                return false;
            }

            foreach (EvidenceData evidence in currentCase.evidenceList)
            {
                if (evidence.evidenceId == evidenceId && !evidence.isFound)
                {
                    evidence.isFound = true;
                    foundEvidenceCount++;
                    Debug.Log($"证据已收集: {evidence.evidenceName}");
                    return true;
                }
            }

            Debug.LogWarning($"证据 ID {evidenceId} 未找到或已收集");
            return false;
        }

        /// <summary>
        /// 获取已收集的证据数量
        /// </summary>
        public int GetFoundEvidenceCount()
        {
            return foundEvidenceCount;
        }

        /// <summary>
        /// 获取总证据数量
        /// </summary>
        public int GetTotalEvidenceCount()
        {
            return currentCase?.evidenceList.Count ?? 0;
        }

        /// <summary>
        /// 提出结论（指控某个嫌疑人）
        /// </summary>
        public bool MakeConclusion(string suspectName)
        {
            if (currentCase == null)
            {
                Debug.LogError("没有正在进行的案件");
                return false;
            }

            playerConclusion = suspectName;
            bool isCorrect = (suspectName == currentCase.correctSuspect);

            Debug.Log($"玩家指控: {suspectName}");
            Debug.Log($"真凶: {currentCase.correctSuspect}");
            Debug.Log($"审理结果: {(isCorrect ? "正确" : "错误")}");

            // 通知 GameManager 更新玩家数据
            GameManager.Instance.CompleteCase(currentCase.caseId, isCorrect);

            return isCorrect;
        }

        /// <summary>
        /// 获取玩家的结论
        /// </summary>
        public string GetPlayerConclusion()
        {
            return playerConclusion;
        }

        /// <summary>
        /// 讯问嫌疑人（获取线索）
        /// </summary>
        public string InterrogateSuspect(string suspectName)
        {
            if (currentCase == null)
            {
                return "没有正在进行的案件";
            }

            if (!currentCase.suspects.Contains(suspectName))
            {
                return $"名单中没有 {suspectName}";
            }

            // 简单的讯问逻辑 - 返回相关证据提示
            string interrogationResult = "";
            foreach (EvidenceData evidence in currentCase.evidenceList)
            {
                if (evidence.relatedSuspect == suspectName)
                {
                    interrogationResult += $"发现线索: {evidence.evidenceName}\n";
                }
            }

            if (string.IsNullOrEmpty(interrogationResult))
            {
                interrogationResult = $"{suspectName}: 我什么都不知道!";
            }

            Debug.Log($"讯问 {suspectName}: {interrogationResult}");
            return interrogationResult;
        }

        /// <summary>
        /// 检查证据（获取详细信息）
        /// </summary>
        public string CheckEvidence(int evidenceId)
        {
            if (currentCase == null)
            {
                return "没有正在进行的案件";
            }

            foreach (EvidenceData evidence in currentCase.evidenceList)
            {
                if (evidence.evidenceId == evidenceId)
                {
                    return $"{evidence.evidenceName}: {evidence.evidenceDescription}";
                }
            }

            return "找不到该证据";
        }

        /// <summary>
        /// 重置案件
        /// </summary>
        public void ResetCase()
        {
            if (currentCase != null)
            {
                foreach (EvidenceData evidence in currentCase.evidenceList)
                {
                    evidence.isFound = false;
                }
                foundEvidenceCount = 0;
                playerConclusion = "";
                Debug.Log("案件已重置");
            }
        }
    }
}
