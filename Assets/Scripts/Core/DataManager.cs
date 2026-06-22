using System.Collections.Generic;
using UnityEngine;
using BaoZhengTian.Data;

namespace BaoZhengTian.Core
{
    /// <summary>
    /// 数据管理器 - 处理所有案件和证据数据
    /// </summary>
    public class DataManager : MonoBehaviour
    {
        [SerializeField] private List<CaseData> allCases = new List<CaseData>();
        private Dictionary<int, CaseData> caseDatabase = new Dictionary<int, CaseData>();

        private void Start()
        {
            InitializeCaseDatabase();
        }

        /// <summary>
        /// 初始化案件数据库
        /// </summary>
        private void InitializeCaseDatabase()
        {
            // 这里可以从 JSON 文件或其他资源加载案件数据
            // 现在使用默认案件初始化
            CreateDefaultCases();

            foreach (CaseData caseData in allCases)
            {
                caseDatabase[caseData.caseId] = caseData;
            }

            Debug.Log($"案件数据库已初始化，共 {caseDatabase.Count} 个案件");
        }

        /// <summary>
        /// 创建默认测试案件
        /// </summary>
        private void CreateDefaultCases()
        {
            // 案件一：失窃案
            CaseData case1 = new CaseData
            {
                caseId = 1,
                caseName = "丞相府珠宝失窃案",
                caseDescription = "丞相府一夜之间失窃了价值连城的珍珠宝石",
                difficultyLevel = 1,
                suspects = new List<string> { "管家李三", "侍女小翠", "园丁老王" },
                correctSuspect = "侍女小翠",
                caseStory = "昨夜丞相府珠宝失窃，三个可疑人物出现在现场...",
                rewardPoints = 100,
                penaltyPoints = 50,
                evidenceList = new List<EvidenceData>
                {
                    new EvidenceData { evidenceId = 101, evidenceName = "钥匙", evidenceDescription = "打开宝库的钥匙", relatedSuspect = "管家李三", isFound = false },
                    new EvidenceData { evidenceId = 102, evidenceName = "绣花针", evidenceDescription = "侍女小翠的绣花针", relatedSuspect = "侍女小翠", isFound = false },
                    new EvidenceData { evidenceId = 103, evidenceName = "泥土", evidenceDescription = "园丁老王靴子上的泥土", relatedSuspect = "园丁老王", isFound = false }
                }
            };

            // 案件二：投毒案
            CaseData case2 = new CaseData
            {
                caseId = 2,
                caseName = "宴会投毒案",
                caseDescription = "官员在宴会中被下毒，三人进入过厨房",
                difficultyLevel = 2,
                suspects = new List<string> { "厨师王二", "仆人小六", "客人赵四" },
                correctSuspect = "仆人小六",
                caseStory = "官员赵大人在宴会中突然毒发身亡，经查有三人进出过厨房...",
                rewardPoints = 150,
                penaltyPoints = 75,
                evidenceList = new List<EvidenceData>
                {
                    new EvidenceData { evidenceId = 201, evidenceName = "毒酒瓶", evidenceDescription = "装毒液的玻璃瓶", relatedSuspect = "仆人小六", isFound = false },
                    new EvidenceData { evidenceId = 202, evidenceName = "菜单", evidenceDescription = "宴会菜单", relatedSuspect = "厨师王二", isFound = false },
                    new EvidenceData { evidenceId = 203, evidenceName = "信件", evidenceDescription = "客人赵四的信件", relatedSuspect = "客人赵四", isFound = false }
                }
            };

            allCases.Add(case1);
            allCases.Add(case2);
        }

        /// <summary>
        /// 获取案件数据
        /// </summary>
        public CaseData GetCaseData(int caseId)
        {
            if (caseDatabase.ContainsKey(caseId))
            {
                return caseDatabase[caseId];
            }
            Debug.LogWarning($"案件 ID {caseId} 不存在");
            return null;
        }

        /// <summary>
        /// 获取所有案件
        /// </summary>
        public List<CaseData> GetAllCases()
        {
            return new List<CaseData>(allCases);
        }

        /// <summary>
        /// 获取指定难度的案件
        /// </summary>
        public List<CaseData> GetCasesByDifficulty(int difficulty)
        {
            List<CaseData> result = new List<CaseData>();
            foreach (CaseData caseData in allCases)
            {
                if (caseData.difficultyLevel == difficulty)
                {
                    result.Add(caseData);
                }
            }
            return result;
        }

        /// <summary>
        /// 添加新案件
        /// </summary>
        public void AddCase(CaseData newCase)
        {
            if (!caseDatabase.ContainsKey(newCase.caseId))
            {
                allCases.Add(newCase);
                caseDatabase[newCase.caseId] = newCase;
                Debug.Log($"新案件已添加: {newCase.caseName}");
            }
            else
            {
                Debug.LogWarning($"案件 ID {newCase.caseId} 已存在");
            }
        }

        /// <summary>
        /// 获取案件的证据
        /// </summary>
        public List<EvidenceData> GetCaseEvidence(int caseId)
        {
            CaseData caseData = GetCaseData(caseId);
            if (caseData != null)
            {
                return caseData.evidenceList;
            }
            return new List<EvidenceData>();
        }
    }
}
