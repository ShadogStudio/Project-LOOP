# Project LOOP

포트폴리오용 **3D 탑다운 로그라이크** Unity 프로젝트입니다.

마을을 거점으로 던전에 반복 진입하고, 복귀 시 수확물을 쌓으며, 사망 시 이번 런의 휴대 재화만 잃습니다.

## 현재 단계

플레이스홀더 기준으로 **마을 ↔ 던전 루프, 근접 전투, 마을 상점**까지 동작합니다. 상용 외형은 로컬 `Assets/ThirdParty`에만 두고 연결할 예정입니다.

## 특징

* 3D 탑다운 시점
* 마을(영구 재화) ↔ 던전(런 한정 재화) 루프
* 공개 Git은 플레이스홀더로 실행 가능
* 구매/외부 에셋은 Git에 올리지 않음

## 기술

* Unity **6000.3** (Unity 6)
* Universal Render Pipeline (URP)
* Input System

## 실행 방법

1. Unity Hub에서 프로젝트를 연다 (권장: `6000.3.20f1`).
2. `Assets/ProjectLOOP/Scenes/Town.unity`를 연다 (빌드 설정 첫 씬).
3. Play.
4. **WASD / 방향키** 이동.
5. **던전 입장**(보라 포털) → 노란 구체로 전리품 → 빨간 적과 전투 → **마을 복귀**(초록 포털, 예치).
6. 공격: **마우스 좌클릭 / Space / J**
7. **상점**(주황): **1** 활력(+최대 HP), **2** 힘(+공격력)

자세한 기획: [Docs/DESIGN.md](Docs/DESIGN.md) · 진행 상태: [DEVELOPMENT.md](DEVELOPMENT.md)  
기술 포트폴리오: [Docs/PORTFOLIO.md](Docs/PORTFOLIO.md) · AI 사용 예시: [Docs/AI_USAGE.md](Docs/AI_USAGE.md)

## 공개 vs 로컬

| 구분 | Git | 설명 |
|------|-----|------|
| `Assets/ProjectLOOP` | 포함 | 자체 코드·씬·플레이스홀더 |
| `Assets/ThirdParty` | 미포함 (README만) | 구매/외부 에셋 |
| APK / EXE | 미포함 | 로컬에서 ThirdParty 포함 빌드 |

## 구조

```text
Assets/
  ProjectLOOP/   # 공개 베이스
  Settings/      # URP
  ThirdParty/    # 로컬 전용
Docs/
  DESIGN.md
  PORTFOLIO.md
  AI_USAGE.md
```

## 빌드

PC(EXE) / Android(APK)는 로컬에서 에셋 포함 후 빌드. 산출물은 Git에 올리지 않는다.
