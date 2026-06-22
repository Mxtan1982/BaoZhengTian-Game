# 开发文档

## 项目概览

包青天审案游戏是一款基于 Unity 的 Android 益智推理视觉小说游戏。

## 核心系统

### 1. GameManager (游戏管理器)
- **位置**: `Assets/Scripts/Core/GameManager.cs`
- **功能**:
  - 全局游戏状态管理
  - 玩家进度保存和加载
  - 品级升降判定
  - 积分管理

**使用示例**:
```csharp
GameManager.Instance.StartCase(1);  // 开始案件
GameManager.Instance.CompleteCase(1, true);  // 完成案件 (true=正确)
var progress = GameManager.Instance.GetPlayerProgress();  // 获取玩家进度
```

### 2. DataManager (数据管理器)
- **位置**: `Assets/Scripts/Core/DataManager.cs`
- **功能**:
  - 案件数据管理
  - 证据数据存储
  - 动态加载和添加案件

**使用示例**:
```csharp
DataManager dm = gameObject.GetComponent<DataManager>();
CaseData caseData = dm.GetCaseData(1);  // 获取案件
List<CaseData> cases = dm.GetAllCases();  // 获取所有案件
List<EvidenceData> evidence = dm.GetCaseEvidence(1);  // 获取证据列表
```

### 3. CaseSystem (案件系统)
- **位置**: `Assets/Scripts/Game/CaseSystem.cs`
- **功能**:
  - 案件审理逻辑
  - 证据收集
  - 嫌疑人讯问
  - 结论判定

**使用示例**:
```csharp
CaseSystem caseSystem = GetComponent<CaseSystem>();
caseSystem.InitializeCase(1);  // 初始化案件
caseSystem.CollectEvidence(101);  // 收集证据
string interrogation = caseSystem.InterrogateSuspect("侍女小翠");  // 讯问嫌疑人
bool correct = caseSystem.MakeConclusion("侍女小翠");  // 提出结论
```

### 4. DialogueSystem (对话系统)
- **位置**: `Assets/Scripts/Game/DialogueSystem.cs`
- **功能**:
  - 视觉小说对话流程
  - 选择支系统
  - 对话节点管理

**使用示例**:
```csharp
DialogueSystem dialogue = GetComponent<DialogueSystem>();
dialogue.StartDialogue(1);  // 开始对话节点 1
DialogueSystem.DialogueNode node = dialogue.GetCurrentNode();
dialogue.SelectChoice(1);  // 选择选项 1
dialogue.EndDialogue();  // 结束对话
```

### 5. RankingSystem (品级系统)
- **位置**: `Assets/Scripts/Game/RankingSystem.cs`
- **功能**:
  - 品级管理 (1-5: 捕头、县令、刺史、尚书、丞相)
  - 升降级判定
  - 进度计算

**使用示例**:
```csharp
RankingSystem ranking = GetComponent<RankingSystem>();
RankingSystem.Rank rank = ranking.GetRankInfo(2);  // 获取品级信息
int rank = ranking.GetRankByPoints(1500);  // 根据积分获取品级
bool canPromote = ranking.CanPromote(2, 85);  // 检查升级条件
float progress = ranking.GetRankProgress(3, 1500);  // 获取品级进度
```

## 数据结构

### CaseData (案件数据)
```csharp
public class CaseData
{
    public int caseId;                      // 案件ID
    public string caseName;                 // 案件名称
    public string caseDescription;          // 案件描述
    public int difficultyLevel;             // 难度等级 (1-3)
    public List<string> suspects;           // 嫌疑人列表
    public string correctSuspect;           // 真凶
    public List<EvidenceData> evidenceList; // 证据列表
    public string caseStory;                // 案件故事/背景
    public int rewardPoints;                // 破案奖励积分
    public int penaltyPoints;               // 破案失败惩罚积分
}
```

### EvidenceData (证据数据)
```csharp
public class EvidenceData
{
    public int evidenceId;                  // 证据ID
    public string evidenceName;             // 证据名称
    public string evidenceDescription;      // 证据描述
    public string relatedSuspect;           // 相关嫌疑人
    public bool isFound;                    // 是否已找到
    public int findLocation;                // 发现位置ID
}
```

### PlayerProgress (玩家进度)
```csharp
public class PlayerProgress
{
    public int currentRank;                 // 当前品级 (1-5)
    public int totalPoints;                 // 总积分
    public int casesCompleted;              // 完成案件数
    public int casesCorrect;                // 正确破案数
    public List<int> completedCaseIds;      // 已完成案件列表
    public int lastPlayedCaseId;            // 最后玩的案件ID
}
```

## 开发流程

### 第一阶段：核心系统架构 ✅
- [x] GameManager
- [x] DataManager
- [x] CaseSystem
- [x] DialogueSystem
- [x] RankingSystem

### 第二阶段：UI框架和对话系统
- [ ] 主菜单UI
- [ ] 案件选择界面
- [ ] 游戏场景UI
- [ ] 对话框UI
- [ ] 证据展示界面

### 第三阶段：案件设计和数据编辑
- [ ] 编写更多案件
- [ ] 编写对话文本
- [ ] 设计证据关系
- [ ] 平衡积分和难度

### 第四阶段：美术资源和优化
- [ ] 角色立绘
- [ ] 背景美术
- [ ] UI美术设计
- [ ] 音效和音乐

### 第五阶段：测试和发布
- [ ] 功能测试
- [ ] 性能优化
- [ ] Android构建配置
- [ ] APK打包和发布

## 品级系统详解

| 品级 | 名称 | 最低积分 | 最高积分 | 维持成功率 |
|------|------|---------|---------|----------|
| 1 | 捕头 | 0 | 499 | 40% |
| 2 | 县令 | 500 | 999 | 60% |
| 3 | 刺史 | 1000 | 1999 | 70% |
| 4 | 尚书 | 2000 | 4999 | 80% |
| 5 | 丞相 | 5000+ | 99999 | 90% |

升级条件：成功率达到下一品级的最低要求
降级条件：成功率低于当前品级的最低要求

## 扩展和自定义

### 添加新案件
```csharp
CaseData newCase = new CaseData
{
    caseId = 3,
    caseName = "新案件名",
    // ... 其他数据
};
DataManager.AddCase(newCase);
```

### 添加新对话
```csharp
DialogueNode newNode = new DialogueNode
{
    nodeId = 10,
    speakerName = "角色名",
    dialogueText = "对话内容",
    // ... 其他数据
};
DialogueSystem.AddDialogueNode(newNode);
```

## 调试

所有核心系统都在关键操作时输出 Debug 日志：

```csharp
Debug.Log($"案件已初始化: {currentCase.caseName}");
Debug.Log($"品级已升级: {newRank}");
Debug.Log($"证据已收集: {evidenceName}");
```

在 Unity Editor 的 Console 窗口查看调试信息。

## 下一步

1. **创建UI界面** - 使用 Unity UI 构建游戏界面
2. **编写更多案件** - 在 DataManager 中添加更多案件数据
3. **优化游戏流程** - 完善玩家体验
4. **准备美术资源** - 准备角色、背景等美术素材
5. **测试和优化** - 在真实设备上测试

---

**更新日期**: 2026-06-22
