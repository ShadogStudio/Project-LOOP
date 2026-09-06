# DEVELOPMENT

## Current Status

기획 v1 확정 — 구현 1단계(자체 코어 + Simple dungeon generator) 대기

## In Progress

* (없음 — 구현 시작 전)

## Completed

* [x] Unity URP 프로젝트 초기 구성
* [x] TutorialInfo / Readme 템플릿 제거
* [x] Unity 개발 규칙 (Cursor rule) 추가
* [x] `Assets/ThirdParty` Git 제외 규칙
* [x] `Assets/ProjectLOOP` 폴더 골격
* [x] 핵심 루프·재화 규칙
* [x] 자체 구현 원칙 확정 (완성형 템플릿에 핵심 로직 비의존)
* [x] 던전: Simple procedural(베이스) → Dungeon Architect(최종) provider 교체 방향 확정
* [x] Core Asset Set (POLYGON Fantasy 중심) 정리
* [x] 기획 v1 Open Questions 확정 (전투/메타/실패/세션/카메라/Simple 규칙)
* [x] EOS(클라우드 저장·업적)는 v2+ · `IOnlineServices` 훅으로만 예약

## Next

* [ ] `IDungeonGenerator` + `SimpleProceduralDungeonGenerator` 설계·구현
* [ ] Town ↔ Dungeon / TownWallet·RunInventory / 탑다운 이동·카메라 자체 구현
* [ ] 근접 기본 공격·피격·사망/복귀 최소 전투
* [ ] 저장 포트 + `IOnlineServices` Null stub (로컬 먼저)
* [ ] ThirdParty 훅에 Synty 바인딩 (로컬)
* [ ] 이후 Dungeon Architect 어댑터 / EOS 본연동 (v2+)

## Issues

* [!] ThirdParty·Dungeon Architect·EOS 없이도 공개 베이스가 컴파일·오프라인 동작하도록 어댑터 경계를 유지할 것
* [!] EOS 자격 증명·설정은 공개 Git에 올리지 말 것
