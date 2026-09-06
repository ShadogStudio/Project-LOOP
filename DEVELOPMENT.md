# DEVELOPMENT

## Current Status

구현 1단계 플레이테스트 확인 — 이동·루트 획득/예치·사망 시 런 루트 손실 동작

## In Progress

* [~] 근접 기본 공격·피격 (다음)

## Completed

* [x] Unity URP 프로젝트 초기 구성
* [x] TutorialInfo / Readme 템플릿 제거
* [x] Unity 개발 규칙 (Cursor rule) 추가
* [x] `Assets/ThirdParty` Git 제외 규칙
* [x] `Assets/ProjectLOOP` 폴더 골격
* [x] 기획 v1 확정
* [x] Town ↔ Dungeon 씬 흐름
* [x] TownWallet / RunInventory + 로컬 저장 포트
* [x] `IOnlineServices` Null stub
* [x] 탑다운 이동·카메라 (~50°)
* [x] `IDungeonGenerator` + `SimpleProceduralDungeonGenerator` (5~8방 + 복도)
* [x] 플레이스홀더로 런 루프 실행 가능
* [x] 플레이테스트: 이동 / 골드 획득·유지 / 사망 시 손실 확인

## Next

* [ ] 근접 기본 공격·피격·적 AI 최소 버전
* [ ] 마을 최소 상점/해금
* [ ] ThirdParty 훅에 Synty 바인딩 (로컬)
* [ ] Dungeon Architect 어댑터 / EOS 본연동 (v2+)

## Issues

* [!] 현재 적 스폰은 DeathZone 플레이스홀더 — 전투 구현 시 교체
* [!] ThirdParty·DA·EOS 없이도 공개 베이스가 컴파일·오프라인 동작해야 함
* [!] EOS 자격 증명은 공개 Git에 올리지 말 것
