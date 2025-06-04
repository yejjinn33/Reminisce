# 🐾 REMINISCE : 기억을 찾아가는 고양이의 어드벤처

### 🎮 소개
**REMINISCE**는 **Unity 기반의 스토리 어드벤처 게임**으로, 기억을 잃은 고양이가 다양한 미니게임을 통해 기억의 조각을 되찾아가는 과정을 담았습니다.

- 장르: 스토리 + 미니게임 어드벤처

- 주인공: 기억을 잃은 고양이

- 개발 기간: 4주

- 개발 인원: 2명 (기획, 맵 제작, 애니메이션, UI, 구현 분담)

##
### 🛠 주요 기능

🎮 인트로 씬	도입 스토리와 기본 조작 튜토리얼

🐾 이동 기능	걷기 / 달리기 / 점프 / 차징 점프

🧭 스테이지 구성	집 → 공원 → 골목 순으로 기억 조각 수집

🎮 미니게임 1	술래잡기 (길고양이를 피해 도망치기)

🎵 미니게임 2	리듬게임 (노트에 맞춰 밥먹기)

☔ 미니게임 3	점프맵 (물웅덩이를 피해 우산까지 이동)

🎞 애니메이션	씬 전환마다 직접 제작한 애니메이션 삽입

 🧠 기억 UI	수집한 기억 조각 UI 클릭 → 회상 애니메이션
 
⏹ ESC 기능	게임 종료 팝업 구현
##
### 🌟 자랑할 만한 포인트

- 서로 다른 장르의 3종 미니게임(술래잡기, 리듬게임, 점프맵) 설계 및 구현

- 직접 제작한 고양이 애니메이션과 감성적인 UI 디자인

- Unity를 활용한 맵 구성, 캐릭터 조작, 오브젝트 상호작용 구현 경험

- 반복적인 기능 테스트와 디버깅을 통해 완성도 높은 결과물 도출
##
### ❗ 어려웠던 점

- 무료 에셋의 한계를 넘어 고양이 애니메이션 직접 제작

- 미니게임마다 조작 방식이 달라 각 씬별 로직 구성에 시간 소요

- 리듬게임의 판정 로직 정교화 과정에서 난이도 밸런싱 고민

- 점프 관련 버그 해결을 위해 지속적인 테스트 및 수정 반복
##
### 🔧 사용 에셋 (출처 링크 포함)
- [고양이](https://assetstore.unity.com/packages/3d/characters/animals/lowpoly-toon-cat-lite-66083)

- [길고양이](https://assetstore.unity.com/packages/3d/characters/animals/mammals/free-chibi-cat-165490)

- [맵/아이템 외 다수](https://assetstore.unity.com/packages/3d/environments/simplepoly-city-low-poly-assets-58899)

- [파티클](https://assetstore.unity.com/packages/vfx/particles/polygonal-s-low-poly-particle-pack-118355)

- 자세한 목록은 /Assets/_AssetCredits.txt 파일 참고
##
### 👩‍💻 개발자
- **최예진: 게임 기획, 맵 제작, 미니게임 전체 구현, UI 일부**

- 신예솔: 스토리 구성, UI 디자인, 애니메이션 작업
##
### 📁 프로젝트 실행 방법
markdown
코드 복사
Unity 버전: 2021.3.x 이상 권장

1. 이 저장소를 클론합니다.
2. Unity에서 프로젝트 폴더를 열어 실행합니다.
3. Assets > Scenes > Start.unity 를 실행하면 시작 화면으로 이동합니다.
