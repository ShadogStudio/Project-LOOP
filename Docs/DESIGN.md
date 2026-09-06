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
| `Dungeon` | Run space. Generated layout, explore, collect loot, return or die. |

---

## Architecture Decisions (Confirmed)

### Core systems — own implementation

이동, 카메라, 전투, 재화, 런 루프 등 **핵심 시스템은 자체 구현**한다.

- TopDown Engine / uMMORPG 등 완성형 템플릿에 게임 로직을 맡기지 않는다.
- 에셋은 표현(모델·애니·VFX·UI 스킨)과 선택적 도구(저장, 조이스틱 등)에 사용한다.
- 공개 Git에는 자체 코드가 남아 포트폴리오로 설명할 수 있어야 한다.

### Dungeon generation — swappable providers

```text
Game code
  → IDungeonGenerator (project interface)
       ├─ SimpleProceduralDungeonGenerator   ← 개발 베이스 (기본)
       └─ DungeonArchitectDungeonGenerator   ← 최종 목표 (에셋 있을 때)
```

| Stage | Provider | Goal |
|-------|----------|------|
| Dev base | Simple procedural placement | 방/복도/입출구/스폰 포인트를 규칙 기반으로 배치 |
| Final | Dungeon Architect adapter | 동일 인터페이스로 교체·호출 |

원칙:

1. 게임플레이는 **생성기 구현체가 아니라 인터페이스 결과물**(레이아웃, 스폰 포인트, 출구 등)만 본다.
2. ThirdParty(Dungeon Architect 등)가 없어도 프로젝트가 컴파일·실행되도록, 어댑터는 선택 조립 또는 플레이스홀더 구현으로 분리한다.
3. Synty 던전 모듈/프리팹은 **배치 슬롯에 꽂히는 콘텐츠**로 취급한다. 생성 로직과 분리한다.

### Art / tools — asset-ready hooks

추가 에셋이 들어와도 코드 전면 수정 없이 연결되도록, 다음을 **호출 지점(hook)** 으로 둔다.

| Hook | Purpose | Example later binding |
|------|---------|------------------------|
| `IDungeonGenerator` | 던전 레이아웃 | Simple → Dungeon Architect |
| Character / prefab refs | 플레이어·적 외형 | Placeholder → POLYGON Hero/Enemies |
| Animation set refs | 이동·전투 클립 | Placeholder → Human Mega |
| VFX slots | 히트·스킬 | None → POLYGON Particle FX |
| UI skin / widget | HUD·인벤 | Stub UI → GUI Pro Fantasy RPG |
| Mobile stick input | 터치 이동 | Optional → Ultimate Joystick |
| Save backend | 마을 재화 등 로컬 저장 | PlayerPrefs / Easy Save → 동일 포트를 EOS Player Data 로 확장 |
| `IOnlineServices` | 클라우드 저장·업적 등 공통 온라인 | Null stub → Epic Online Services |

로컬 전용 에셋 경로: `Assets/ThirdParty/` (Git 미포함, README만 공개).

### Online services — Epic Online Services (EOS)

공통으로 재사용할 온라인 기능만 대상으로 한다. **본연동은 코어 루프 플레이 가능 이후(v2+)**.

| Feature (v2+) | Scope |
|---------------|--------|
| Cloud save | 마을 지갑·해금 등 메타 진행 (런 인벤은 로컬/세션) |
| Achievements | 공통 업적 이벤트 (첫 클리어, 누적 골드 등) |
| Auth | EOS 로그인(필요 시). 게스트/오프라인 폴백 유지 |

원칙:

1. 게임플레이는 `IOnlineServices` / 저장 포트만 호출한다. EOS SDK를 직접 호출하지 않는다.
2. v1 기본 구현은 **Null / Local stub** (오프라인 완전 동작).
3. EOS 패키지·자격 증명은 `Assets/ThirdParty` 또는 로컬 설정으로 두고 공개 Git에 시크릿을 올리지 않는다.
4. 멀티플레이·매치메이킹 등은 범위 밖(당분간).

---

## Core Asset Set (from 유니티 에셋 list)

시각 톤은 **Synty POLYGON Fantasy**로 통일한다.

### Phase A — 로컬 개발에 우선 임포트

| Role | Asset |
|------|--------|
| Town | POLYGON - Town Pack, Shops Pack |
| Dungeon modules | POLYGON - Dungeons Pack, Fantasy Dungeon Map |
| Nature (optional bridge) | POLYGON - Nature Pack, Adventure Pack |
| Player / NPC | Modular Fantasy Hero, Fantasy Characters, Knights / Fantasy Rivals |
| Customize | Character Enhancement Toolkit for Polygon Packs |
| Animation | Human Mega Animations Pack (+ Human Basic Motions) |
| Blockout | POLYGON - Prototype Pack |
| Particles | POLYGON - Particle FX Pack |

### Phase B — UX / polish (필요할 때)

| Role | Asset |
|------|--------|
| UI | GUI Pro - Fantasy RPG or Simple Fantasy UI, POLYGON Icons |
| Mobile input | Ultimate Joystick |
| Audio | Fantasy Game Sound Effects / Fantasy Sounds Bundle / Monster Sounds |
| Save (optional) | Easy Save |

### Phase C — final dungeon tool

| Role | Asset |
|------|--------|
| Procedural dungeon | Dungeon Architect (via `IDungeonGenerator` adapter) |

### Phase D — online (after core loop)

| Role | Service |
|------|---------|
| Cloud save / Achievements | Epic Online Services via `IOnlineServices` |

### Out of scope for this title

Sci-Fi / Western / Pirate / Office 등 비판타지 POLYGON 팩, 2D 전용 팩, 완성형 게임 템플릿(TopDown Engine, uMMORPG 등)에 핵심 로직 의존.

---

## Development Roadmap

```text
1. 기획 v1 확정 (완료)
2. 자체 코어 골격
   - Town ↔ Dungeon 씬 흐름
   - TownWallet / RunInventory
   - Top-down move + camera (own)
3. IDungeonGenerator + SimpleProceduralDungeonGenerator
   - Placeholder modules로 방 배치·입출구·스폰
4. ThirdParty 훅에 Synty 모듈 바인딩 (로컬만)
5. 최소 전투·루팅·사망/복귀
6. UI / 사운드 / VFX 슬롯 채우기
7. DungeonArchitectDungeonGenerator 어댑터 연결
8. Epic Online Services 연동 (클라우드 저장·업적, `IOnlineServices`)
9. APK / EXE 로컬 빌드 (ThirdParty 포함)
```

공개 Git: 인터페이스 + Simple generator + placeholders.  
로컬 빌드: ThirdParty 메쉬/애니/UI/DA 연결.

---

## Public vs Local Assets

| Path | Git | Use |
|------|-----|-----|
| `Assets/ProjectLOOP/` | Yes | Own code, scenes, placeholders, generator interface |
| `Assets/Settings/` | Yes | URP settings |
| `Assets/ThirdParty/` | No (README only) | POLYGON, DA, GUI, SFX, etc. |
| `Build/`, `*.apk`, `*.exe` | No | Local distribution builds |

---

## Confirmed Gameplay (v1)

| 항목 | 결정 |
|------|------|
| 전투 | 근접 중심 — 기본 공격 1종 + 피격/사망. 원거리·스킬은 슬롯만 예약 |
| 메타 진행 | 마을 골드로 상점/해금(최소). NPC 대화·크래프트는 후순위 |
| 실패 비용 | 런 휴대 재화만 손실. 장비 내구도 없음 |
| 세션 | 한 런 약 5~15분. 초반 방·적 밀도는 낮게 |
| 플랫폼 | PC(EXE) 우선, WASD. 모바일은 Ultimate Joystick 훅만 예약 |
| 카메라 | 탑다운 고정 각도 약 50° + 플레이어 팔로우. 줌은 후순위 |
| 조작 | PC: WASD 이동, 마우스 또는 단일 키로 기본 공격 |

### Simple procedural dungeon (v1)

`SimpleProceduralDungeonGenerator` 기본 규칙:

| Rule | Value |
|------|--------|
| Room count | 5~8 |
| Connectivity | 복도로 연결 (입구에서 출구까지 도달 가능) |
| Entrances / exits | 입구 1, 출구 1 |
| Enemy / loot density | 희소 (초반 난이도 낮게) |
| Modules | Placeholder 우선 → 이후 POLYGON 던전 모듈 슬롯 바인딩 |

런 흐름:

```text
Town (prepare / minimal shop)
  → IDungeonGenerator (Simple v1)
  → Dungeon (melee, sparse loot)
  → Return (deposit)  OR  Die (clear run loot)
  → Town
```

---

## Current Phase

**구현 1단계 완료(플레이 가능).** 다음: 근접 전투 최소 버전.

## Deferred (v2+)

- 원거리·스킬 전투 및 전투 피드백 고도화
- Dungeon Architect 어댑터 전환·튜닝
- Epic Online Services (클라우드 저장·업적) 본연동
- 모바일(APK) 입력 UX 본구현
- 카메라 줌/시네마틱
- NPC 퀘스트·크래프트 등 확장 메타
- 장비 손실·내구도 등 추가 실패 비용

## Non-Goals (Near Term)

- Depending on TopDown Engine for core loop
- Shipping ThirdParty assets in the public repository
- Implementing Dungeon Architect adapter before Simple generator works
- Implementing EOS before local save + core loop work
- EOS multiplayer / matchmaking in v1–v2 scope
