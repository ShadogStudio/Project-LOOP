# Changelog

## [Unreleased]

### Added

* 포트폴리오용 폴더 골격 (`Assets/ProjectLOOP`)
* 기획 문서 (`Docs/DESIGN.md`) 및 개발 상태 문서 (`DEVELOPMENT.md`)
* `Assets/ThirdParty` 로컬 전용 에셋 폴더 (README만 Git 포함)
* 자체 코어 구현·던전 generator 교체(Simple → Dungeon Architect) 기획
* POLYGON Fantasy 중심 Core Asset Set 정리
* 기획 v1 확정 (근접 전투, 최소 상점 메타, 런 재화 손실, PC 우선, Simple 던전 5~8방)
* Epic Online Services 예약 (클라우드 저장·업적, `IOnlineServices`, 본연동은 v2+)
* 구현 1단계: Town/Dungeon 루프, 재화, 탑다운 이동, Simple procedural 던전, 온라인 Null stub

### Changed

* 기획 확정 전까지 게임플레이 스텁 코드를 제거하고 문서 중심으로 정리
* Open Questions를 Confirmed Gameplay (v1) / Deferred (v2+)로 정리
* Build Settings 시작 씬을 Town / Dungeon으로 설정

### Improved

* 공개 Git과 로컬 풀에셋 빌드(APK/EXE) 경로를 분리
