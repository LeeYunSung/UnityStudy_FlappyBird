# UnityStudy_FlappyBird

### - What is FlappyBird Game?
정해진 목숨 내에서 Player(새)가 장애물(기둥)을 피하고, Boost기능을 사용해서 최고점수를 얻는 게임

### - Major Function
1) 시작 카운트(3, 2, 1 후 시작), 일시정지 및 다시재생 기능
2) 장애물(기둥) Object Pooling
> Update문에 의존하는 방식이 아닌, Queue를 활용하여 장애물(기둥)이 해야할 일과 Pool이 해야할 일을 구분하여 구현함
> 1) 게임 시작 시, 장애물(기둥) 5개를 생성하고 활성화된 장애물 객체를 List에 보관
> 2) Player(새)가 장애물을 통과했을 때, Pool 객체에게 알림. Pool객체는 반납된 객체(여유분) 가 Queue에 있는지 확인하여 있을 떄는 바로 주고 없을 때는 생성하여 줌.
> 3) 5초 Coroutine 함수 실행 후에 Pool에 장애물(기둥) 객체 반납. Pool객체는 반납된 객체를 Queue에 다시 보관

3) Life Point 및 그에 따른 Player 효과(기둥에 부딪히면 Player(새)가 깜빡임)
4) Booster 및 그에 따른 Player 효과(부스터 사용 중, Player(새)가 무지개색으로 변함)
5) 점수 저장 및 Ranking 화면
6) Player(새)의 상하 이동 범위 제한

### - Learned
1) UI Button, GameObject Button 구현 방법 구분
2) Trigger 체크 유무에 따른 CollisionEnter(), TriggerEnger() 사용 구분
3) 애니메이션 Layer, Trigger, Bool
4) Singletone 패턴을 활용한 Prefabs 객체 관리
5) 기타 코드 개선 방법

### - Screenshot
![캡처](https://user-images.githubusercontent.com/32055099/114313165-6038c200-9b30-11eb-9e55-177f9a537f0a.JPG)
