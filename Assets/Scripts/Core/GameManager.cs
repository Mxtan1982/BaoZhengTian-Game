using UnityEngine;
using BaoZhengTian.Data;

namespace BaoZhengTian.Core
{
    /// <summary>
    /// 游戏全局管理器 - 单例模式
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private PlayerProgress playerProgress;
        [SerializeField] private DataManager dataManager;

        private void Awake()
        {
            // 单例模式初始化
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 初始化数据管理器
            dataManager = GetComponent<DataManager>();
            if (dataManager == null)
            {
                dataManager = gameObject.AddComponent<DataManager>();
            }

            // 加载玩家存档
            LoadPlayerProgress();
        }

        /// <summary>
        /// 加载玩家进度
        /// </summary>
        private void LoadPlayerProgress()
        {
            string json = PlayerPrefs.GetString("PlayerProgress", "");
            
            if (string.IsNullOrEmpty(json))
            {
                // 创建新玩家
                playerProgress = new PlayerProgress
                {
                    currentRank = 1,
                    totalPoints = 0,
                    casesCompleted = 0,
                    casesCorrect = 0,
                    completedCaseIds = new System.Collections.Generic.List<int>(),
                    lastPlayedCaseId = -1
                };
                SavePlayerProgress();
            }
            else
            {
                playerProgress = JsonUtility.FromJson<PlayerProgress>(json);
            }

            Debug.Log($"玩家进度已加载 - 品级: {playerProgress.currentRank}, 积分: {playerProgress.totalPoints}");
        }

        /// <summary>
        /// 保存玩家进度
        /// </summary>
        public void SavePlayerProgress()
        {
            string json = JsonUtility.ToJson(playerProgress, true);
            PlayerPrefs.SetString("PlayerProgress", json);
            PlayerPrefs.Save();
            Debug.Log("玩家进度已保存");
        }

        /// <summary>
        /// 获取玩家进度
        /// </summary>
        public PlayerProgress GetPlayerProgress()
        {
            return playerProgress;
        }

        /// <summary>
        /// 更新玩家积分和品级
        /// </summary>
        public void UpdatePlayerStats(int pointsChange, bool caseCorrect)
        {
            playerProgress.totalPoints += pointsChange;
            playerProgress.casesCompleted++;

            if (caseCorrect)
            {
                playerProgress.casesCorrect++;
                // 升级判定逻辑
                CheckRankUp();
            }
            else
            {
                // 降级判定逻辑
                CheckRankDown();
            }

            SavePlayerProgress();
        }

        /// <summary>
        /// 检查升级
        /// </summary>
        private void CheckRankUp()
        {
            int successRate = (playerProgress.casesCorrect * 100) / playerProgress.casesCompleted;
            
            if (playerProgress.currentRank < 5 && successRate >= 80)
            {
                playerProgress.currentRank++;
                Debug.Log($"升级! 新品级: {playerProgress.currentRank}");
            }
        }

        /// <summary>
        /// 检查降级
        /// </summary>
        private void CheckRankDown()
        {
            int successRate = (playerProgress.casesCorrect * 100) / playerProgress.casesCompleted;
            
            if (playerProgress.currentRank > 1 && successRate < 50)
            {
                playerProgress.currentRank--;
                Debug.Log($"降级! 新品级: {playerProgress.currentRank}");
            }
        }

        /// <summary>
        /// 开始新案件
        /// </summary>
        public void StartCase(int caseId)
        {
            playerProgress.lastPlayedCaseId = caseId;
            SavePlayerProgress();
            Debug.Log($"开始案件 ID: {caseId}");
        }

        /// <summary>
        /// 完成案件
        /// </summary>
        public void CompleteCase(int caseId, bool isCorrect)
        {
            if (!playerProgress.completedCaseIds.Contains(caseId))
            {
                playerProgress.completedCaseIds.Add(caseId);
            }

            CaseData caseData = dataManager.GetCaseData(caseId);
            int points = isCorrect ? caseData.rewardPoints : -caseData.penaltyPoints;

            UpdatePlayerStats(points, isCorrect);
            Debug.Log($"案件完成 - ID: {caseId}, 正确: {isCorrect}, 积分变化: {points}");
        }
    }
}
