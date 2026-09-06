# Project LOOP — Design Document

## Overview

- **Project**: Project LOOP
- **Purpose**: Portfolio Unity project (public Git base + local full-asset builds)
- **Genre**: 3D top-down roguelike
- **Engine**: Unity 6 (URP)

## Core Fantasy

마을을 거점으로 던전에 반복 진입한다. 성공적으로 복귀하면 수확물을 마을에 쌓고, 죽으면 이번 런에서 챙긴 것은 잃는다.

## Core Loop

```text
Town (prepare / spend persistent wealth)
  → Enter Dungeon
  → Explore / Collect run loot
  → Return to Town (deposit run loot)  OR  Die (lose run loot)
  → Repeat
```

## Economy Rules

| Location | Currency | On death | On successful return |
|----------|----------|----------|----------------------|
| Town wallet | Persistent gold | Kept | Receives deposited run loot |
| Run inventory | Dungeon-carried loot | Cleared | Deposited into town wallet |

## Planned Scenes

| Scene | Role |
|-------|------|
| `Town` | Hub. Prepare, spend persistent wealth, enter dungeon. |
| `Dungeon` | Run space. Explore, collect loot, return or die. |

## Public vs Local Assets

| Path | Git | Use |
|------|-----|-----|
| `Assets/ProjectLOOP/` | Yes | Code, scenes, placeholders (기획 확정 후 구현) |
| `Assets/Settings/` | Yes | URP settings |
| `Assets/ThirdParty/` | No (README only) | Purchased / external art for APK·EXE |
| `Build/`, `*.apk`, `*.exe` | No | Local distribution builds |

## Current Phase

**기획 우선.** 코드/씬 구현은 기획(Open Questions 포함)이 정리된 뒤에 진행한다.

## Open Questions (Next Design Pass)

- Combat model (melee / ranged / skills)
- Procedural dungeon generation vs hand-authored rooms
- Meta progression beyond gold (unlocks, NPCs, crafting)
- Failure cost beyond run loot (equipment durability, etc.)
- Session length target and difficulty curve
- Mobile (APK) vs PC (EXE) input UX differences
- Camera feel (height, angle, zoom) and control scheme details

## Non-Goals (Current Phase)

- Gameplay code / stub prototypes
- Full combat systems
- Procedural generation
- Shipping store assets in the public repository
