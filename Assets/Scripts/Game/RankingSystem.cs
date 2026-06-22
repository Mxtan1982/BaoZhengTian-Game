using UnityEngine;

namespace BaoZhengTian.Game
{
    /// <summary>
    /// 品级系统 - 管理玩家的官职升降
    /// 品级: 1-5 分别代表：捕头、县令、刺史、尚书、丞相
    /// </summary>
    public class RankingSystem : MonoBehaviour
    {
        [System.Serializable]
        public class Rank
        {
            public int rankId;                      // 品级ID (1-5)
            public string rankName;                 // 品级名称
            public string rankDescription;          // 品级描述
            public int minPoints;                   // 该品级所需的最低积分
            public int maxPoints;                   // 该品级的最高积分
            public float minSuccessRate;            // 维持该品级的最低成功率 (百分比)
        }

        private Rank[] ranks = new Rank[5];

        private void Start()
        {
            InitializeRanks();
        }

        /// <summary>
        /// 初始化品级系统
        /// </summary>
        private void InitializeRanks()
        {
            ranks[0] = new Rank
            {
                rankId = 1,
                rankName = "捕头",
                rankDescription = "初出茅庐的捕头，还要多多努力",
                minPoints = 0,
                maxPoints = 499,
                minSuccessRate = 40
            };

            ranks[1] = new Rank
            {
                rankId = 2,
                rankName = "县令",
                rankDescription = "有能力的县令，开始展露头角",
                minPoints = 500,
                maxPoints = 999,
                minSuccessRate = 60
            };

            ranks[2] = new Rank
            {
                rankId = 3,
                rankName = "刺史",
                rankDescription = "精明能干的刺史，成为一方主官",
                minPoints = 1000,
                maxPoints = 1999,
                minSuccessRate = 70
            };

            ranks[3] = new Rank
            {
                rankId = 4,
                rankName = "尚书",
                rankDescription = "才华横溢的尚书，位高权重",
                minPoints = 2000,
                maxPoints = 4999,
                minSuccessRate = 80
            };

            ranks[4] = new Rank
            {
                rankId = 5,
                rankName = "丞相",
                rankDescription = "执掌天下的丞相，权倾朝野",
                minPoints = 5000,
                maxPoints = 99999,
                minSuccessRate = 90
            };

            Debug.Log("品级系统已初始化");
        }

        /// <summary>
        /// 获取指定品级的信息
        /// </summary>
        public Rank GetRankInfo(int rankId)
        {
            if (rankId >= 1 && rankId <= 5)
            {
                return ranks[rankId - 1];
            }
            return null;
        }

        /// <summary>
        /// 根据积分获取对应品级
        /// </summary>
        public int GetRankByPoints(int points)
        {
            for (int i = 4; i >= 0; i--)
            {
                if (points >= ranks[i].minPoints)
                {
                    return ranks[i].rankId;
                }
            }
            return 1;
        }

        /// <summary>
        /// 获取升级所需的积分
        /// </summary>
        public int GetPointsNeededForNextRank(int currentRank, int currentPoints)
        {
            if (currentRank >= 5)
            {
                return -1; // 已经是最高品级
            }

            Rank nextRank = ranks[currentRank];
            return nextRank.minPoints - currentPoints;
        }

        /// <summary>
        /// 检查是否满足升级条件
        /// </summary>
        public bool CanPromote(int currentRank, int successRate)
        {
            if (currentRank >= 5)
            {
                return false; // 已经是最高品级
            }

            Rank nextRank = ranks[currentRank];
            return successRate >= nextRank.minSuccessRate;
        }

        /// <summary>
        /// 检查是否满足降级条件
        /// </summary>
        public bool CanDemote(int currentRank, int successRate)
        {
            if (currentRank <= 1)
            {
                return false; // 已经是最低品级
            }

            Rank currentRankInfo = ranks[currentRank - 1];
            return successRate < currentRankInfo.minSuccessRate;
        }

        /// <summary>
        /// 获取品级进度（当前品级的百分比）
        /// </summary>
        public float GetRankProgress(int currentRank, int currentPoints)
        {
            Rank rankInfo = GetRankInfo(currentRank);
            if (rankInfo == null)
            {
                return 0;
            }

            int pointsInRank = currentPoints - rankInfo.minPoints;
            int pointsNeeded = rankInfo.maxPoints - rankInfo.minPoints;

            if (pointsNeeded <= 0)
            {
                return 100;
            }

            float progress = (float)pointsInRank / pointsNeeded * 100;
            return Mathf.Clamp(progress, 0, 100);
        }

        /// <summary>
        /// 获取所有品级信息
        /// </summary>
        public Rank[] GetAllRanks()
        {
            return ranks;
        }
    }
}
