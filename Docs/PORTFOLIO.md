# Project LOOP — 기술 포트폴리오

**역할**: 기획·클라이언트·저장·온라인 훅·문서 전담 (개인 프로젝트)  
**저장소**: https://github.com/ShadogStudio/Project-LOOP  
**문서 기준일**: 2026-09-07  
**엔진**: Unity 6000.3.20f1 · URP · Input System

---

## 1. 한 줄 요약

Unity 6로 **3D 탑다운 로그라이크**의 코어 루프를 자체 구현한 포트폴리오 프로젝트다.  
공개 Git에는 플레이스홀더로 실행 가능한 베이스만 두고, 구매 에셋은 `Assets/ThirdParty`(Git 제외)에 분리한다.

---

## 2. 핵심 루프

```text
마을 (영구 골드 / 상점) → 던전 입장 → 탐험·전투·루팅
  → 마을 복귀(예치) 또는 사망(런 전리품 손실) → 반복
```

| 구분 | 내용 |
|------|------|
| 마을 지갑 | 영구 골드. 상점·메타 해금에 사용. 사망해도 유지 |
| 런 인벤토리 | 던전 전리품. 복귀 시 예치, 사망 시 초기화 |

---

## 3. 기술 스택

| 영역 | 선택 | 이유 |
|------|------|------|
| 엔진 | Unity 6 URP | PC 우선, 이후 모바일 빌드 여지 |
| 입력 | Input System | WASD·마우스·키 공격 |
| 저장 | `ISaveService` + PlayerPrefs | v1 메타(골드·해금) 영속화 |
| 온라인 | `IOnlineServices` + Null 스텁 | v2+ EOS 바인딩 자리만 확보 |
| 던전 | `IDungeonGenerator` + Simple 구현 | 외부 도구 없이도 실행 |

---

## 4. 아키텍처

### 4.1 세션 중심

`GameSession`(DontDestroyOnLoad)이 마을 지갑·런 인벤토리·메타 보너스·저장·온라인 포트를 소유한다.  
씬 전환 전 `EnsureExists` + 저장으로 상점 해금이 유실되지 않도록 고정했다.

### 4.2 교체 가능한 제공자

```text
게임플레이
  ├─ IDungeonGenerator → SimpleProceduralDungeonGenerator (현재)
  │                    → (예약) 전용 던전 도구 어댑터
  ├─ ISaveService      → LocalPlayerPrefsSaveService
  └─ IOnlineServices   → NullOnlineServices → (v2+) EOS
```

게임 코드는 레이아웃·스폰·출구 **결과**와 인터페이스만 본다. 외부 SDK/에셋이 없어도 컴파일·플레이된다.

### 4.3 공개 vs 로컬

| 경로 | Git | 역할 |
|------|-----|------|
| `Assets/ProjectLOOP` | 포함 | 자체 코드·씬·플레이스홀더 |
| `Assets/ThirdParty` | 미포함 | 구매/외부 아트 |
| 빌드 산출물 | 미포함 | APK/EXE |

---

## 5. 구현 하이라이트 (검증된 범위)

### 5.1 마을 ↔ 던전 런 루프

- `Town` / `Dungeon` 씬, `ScenePortal`로 전환
- 복귀 시 런 골드 → 마을 지갑 예치
- 사망 시 런 인벤토리만 초기화

### 5.2 단순 절차적 던전

- 방 5~8, 복도 연결, 입·출구 각 1
- 적 희소 스폰 + **최소 1마리 보장**
- 시드 기반 `System.Random`

### 5.3 근접 전투

- `MeleeAttack` OverlapSphere + 쿨다운
- 추적형 적(`SimpleChaseEnemy`), HP, 낙하/`FallYKill`
- 스윙 플래시·월드 HP 오버레이로 최소 피드백

### 5.4 마을 상점 · 메타 영속화

- 활력(+최대 HP), 힘(+공격력) 영구 해금 (구매 상한)
- `LocalPlayerPrefsSaveService`로 골드·해금 저장
- 포탈 진입 전 저장·세션 보장으로 리셋 버그 방지

### 5.5 UX 힌트

- 포탈·상점 월드 라벨 (한국어)
- Stub HUD로 지갑/상태 표시

---

## 6. 담당 기여

- 코어 루프·경제·전투·상점·저장 설계 및 구현
- 인터페이스 분리로 외부 도구·온라인 의존 최소화
- 공개 Git 라이선스·시크릿·ThirdParty 바이너리 분리 정책
- DEVELOPMENT / DESIGN / README로 상태와 코드 일치 유지

---

## 7. 의도적으로 아직 하지 않은 것

- 공개 저장소에 상용 에셋 바이너리 포함
- v1에서 EOS 본연동 (훅만 존재)
- 완성형 탑다운/전투 템플릿에 코어 위임
- 멀티플레이

**다음**: 로컬 ThirdParty 외형 바인딩 → (v2+) 던전 도구 어댑터, EOS 클라우드 저장·업적

---

## 8. 실행 (리뷰어용)

1. Unity Hub에서 `6000.3.20f1`로 프로젝트 오픈  
2. `Assets/ProjectLOOP/Scenes/Town.unity` Play  
3. WASD 이동 → 보라 포털(던전) → 전투·루팅 → 초록 포탈(복귀)  
4. 상점(주황): `1` 활력 / `2` 힘 · 공격: 좌클릭 / Space / J  

상세: [DESIGN.md](DESIGN.md) · [DEVELOPMENT.md](../DEVELOPMENT.md) · [AI_USAGE.md](AI_USAGE.md)
