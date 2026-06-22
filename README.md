# 包青天审案游戏 (BaoZhengTian Mystery Game)

一款基于Unity开发的Android益智推理视觉小说游戏。玩家扮演包青天，通过收集证据、讯问嫌疑人、逻辑推理来审理案件。推理错误会被降级！

## 游戏特色

- 🎮 **益智推理玩法** - 收集证据，推理真凶
- 📖 **视觉小说风格** - 沉浸式对话和故事体验
- 📊 **品级系统** - 初级、中级、高级等，错案降级
- 🎯 **多个案件** - 丰富的故事和推理挑战
- 📱 **Android手游** - 随时随地玩耍

## 项目结构

```
Assets/
├── Scripts/
│   ├── Core/              # 核心系统
│   │   ├── GameManager.cs
│   │   └── DataManager.cs
│   ├── Game/              # 游戏逻辑
│   │   ├── CaseSystem.cs
│   │   ├── DialogueSystem.cs
│   │   └── RankingSystem.cs
│   ├── UI/                # 用户界面
│   │   ├── DialogueUI.cs
│   │   ├── CaseUI.cs
│   │   └── RankingUI.cs
│   └── Data/              # 数据定义
│       ├── CaseData.cs
│       └── EvidenceData.cs
├── Scenes/
│   ├── MainMenu.unity
│   ├── GameScene.unity
│   └── CaseSelect.unity
├── Prefabs/
├── Resources/
└── Settings/
```

## 开发计划

- [x] 第一阶段：核心系统架构
- [ ] 第二阶段：UI框架和对话系统
- [ ] 第三阶段：案件设计和数据编辑
- [ ] 第四阶段：美术资源和优化
- [ ] 第五阶段：测试和发布

## 技术栈

- **引擎**: Unity 2022 LTS 或更新版本
- **语言**: C#
- **平台**: Android
- **目标设备**: Android 8.0+ (API 26+)

## 快速开始

1. 克隆仓库
2. 用 Unity 打开项目
3. 按照开发计划完成各阶段
4. 测试和打包 APK

## 许可证

MIT License

---

**开发者**: Mxtan1982  
**开始日期**: 2026-06-22