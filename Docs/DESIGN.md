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

Stub implementation:

- `TownWallet` — PlayerPrefs persistence
- `RunInventory` — in-memory for the current run
- Town return portal deposits loot; death zone clears loot and returns to Town

## Scenes (Stub)

| Scene | Role |
|-------|------|
| `Town` | Hub. Dungeon entrance portal. Shows persistent gold. |
| `Dungeon` | Temporary run space. Loot pickups, return portal, death zone. |

Playable with placeholders only (no third-party art required).

## Camera / Control (Stub)

- Top-down follow camera
- WASD / Arrow keys movement (Input System)

## Public vs Local Assets

| Path | Git | Use |
|------|-----|-----|
| `Assets/ProjectLOOP/` | Yes | Code, scenes, placeholders |
| `Assets/Settings/` | Yes | URP settings |
| `Assets/ThirdParty/` | No (README only) | Purchased / external art for APK·EXE |
| `Build/`, `*.apk`, `*.exe` | No | Local distribution builds |

## Open Questions (Next Design Pass)

- Combat model (melee / ranged / skills)
- Procedural dungeon generation vs hand-authored rooms
- Meta progression beyond gold (unlocks, NPCs, crafting)
- Failure cost beyond run loot (equipment durability, etc.)
- Session length target and difficulty curve
- Mobile (APK) vs PC (EXE) input UX differences

## Non-Goals (Current Phase)

- Full combat systems
- Procedural generation
- Shipping store assets in the public repository
