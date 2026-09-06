# Project LOOP

포트폴리오용 **3D 탑다운 로그라이크** Unity 프로젝트입니다.

마을을 거점으로 던전에 반복 진입하고, 복귀 시 수확물을 쌓으며, 사망 시 이번 런의 휴대 재화만 잃는 구조를 목표로 합니다.

## 현재 단계

**구현 1단계.** 플레이스홀더로 마을 ↔ 던전 루프가 동작합니다. 상용 외형은 로컬 `Assets/ThirdParty` 바인딩 예정입니다.

## 특징 (목표)

* 3D 탑다운 시점
* 마을(영구 재화) ↔ 던전(런 한정 재화) 루프
* 공개 Git은 플레이스홀더로 실행 가능한 베이스
* 상용/추가 에셋은 로컬 `Assets/ThirdParty`에만 두고 APK·EXE 빌드에 사용

## 기술

* Unity **6000.3** (Unity 6)
* Universal Render Pipeline (URP)
* Input System

## 실행 방법

1. Unity Hub에서 이 프로젝트를 연다 (권장 버전: `6000.3.20f1`).
2. `Assets/ProjectLOOP/Scenes/Town.unity`를 연다 (빌드 설정의 첫 씬).
3. Play 한다.
4. **WASD / 방향키**로 이동한다.
5. 보라 포털 → 던전, 노란 구체 → 런 전리품, 빨간 적 → 전투, 초록 포털 → 마을 복귀(예치).
6. 공격: **마우스 좌클릭 / Space / J**
7. 마을 주황 상점 위: **1** 활력(+최대 HP), **2** 힘(+공격력)

## 공개 저장소 vs 로컬 빌드

| 구분 | Git | 설명 |
|------|-----|------|
| `Assets/ProjectLOOP` | 포함 | 자체 코드·씬·플레이스홀더 |
| `Assets/ThirdParty` | **미포함** (README만) | 구매/외부 에셋 |
| APK / EXE | **미포함** | 로컬에서 ThirdParty 포함 빌드 |

자세한 기획은 [Docs/DESIGN.md](Docs/DESIGN.md), 진행 상태는 [DEVELOPMENT.md](DEVELOPMENT.md)를 참고하세요.

## 프로젝트 구조

```text
Assets/
  ProjectLOOP/     # 공개 베이스 (골격)
  Settings/        # URP
  ThirdParty/      # 로컬 전용 에셋
Docs/
  DESIGN.md
```

## 빌드

* PC(EXE) / Android(APK)는 로컬에서 에셋을 포함한 뒤 빌드한다.
* 빌드 산출물은 Git에 올리지 않는다 (`Build/`, `*.apk` 등 ignore).
