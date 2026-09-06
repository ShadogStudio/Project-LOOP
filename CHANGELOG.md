# Changelog

## [Unreleased]

### Added

* 포트폴리오용 폴더 골격 (`Assets/ProjectLOOP`)
* 기획 문서 (`Docs/DESIGN.md`) 및 개발 상태 문서 (`DEVELOPMENT.md`)
* `Assets/ThirdParty` 로컬 전용 에셋 폴더 (README만 Git 포함)
* 자체 코어 구현·던전 생성기 교체(Simple → 전용 도구) 기획
* 기획 v1 확정 (근접 전투, 최소 상점 메타, 런 재화 손실, PC 우선, Simple 던전 5~8방)
* Epic Online Services 예약 (클라우드 저장·업적, `IOnlineServices`, 본연동은 v2+)
* 구현 1단계: 마을/던전 루프, 재화, 탑다운 이동, 단순 절차적 던전, 온라인 Null 스텁
* 최소 근접 전투 (플레이어 공격, 추적 적, HP/사망, 낙하 사망)
* 마을 최소 상점 (영구 최대 HP / 근접 데미지 해금)
* 포탈·상점 안내 라벨, 공격 스윙 피드백, 적 HP 오버레이

### Changed

* 기획 확정 전까지 게임플레이 스텁 코드를 제거하고 문서 중심으로 정리
* 미결 질문을 확정 게임플레이(v1) / 보류(v2+)로 정리
* 빌드 설정 시작 씬을 Town / Dungeon으로 설정
* 문서에서 구체 상용 에셋 목록을 제거하고 한글화

### Improved

* 공개 Git과 로컬 풀에셋 빌드(APK/EXE) 경로를 분리
