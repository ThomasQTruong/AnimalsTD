# [CS 4700] Animals Tower Defense
## Members
Thomas Truong\
Phu Truong\
Darren Manalo

## Info
- Currently a prototype.
  - Map/Enemies/Towers/Stats are test variants.
    - Current default stats: 1000$ and 100 hp.
  - Enemies follow the track.
  - Can place towers.
    - Can right-click to cancel.
    - Has placement visualization.
    - Cannot place ontop of Shop UI.
    - Cannot go negative in money (does not place).
      - Can still see placement visualization with insufficient balance.
    - Prof. Eger: works.
      - Does actually shoot.
      - Costs 250$.
    - Banana Tower: WIP.
  - Can sell towers.
    - Currently set at 50% money back for testing.
  - Currently have 7 total rounds (All Phu enemies).
    1. 5 Phus.
    2. 10 Phus.
    3. 20 Phus.
    4. 30 Phus.
    5. 50 Phus.
    6. 75 Phus.
    7. 100 Phus.

## Plans
- Create actual towers/enemies/map.
  - Implement tower upgrades.
  - Implement splash damage towers.
  - Implement pierce attacks.
- Prevent placement on the track.
- Create main menu.
- Make Shop UI pretty.
- Implement auto next round.
- Balance the prices, health, and damage.
- Create Easy/Normal/Hard difficulties.
  - Tower prices increase by difficulty.
- Implement game over.
- Reconfigure the rounds and add more rounds.
  - Want to create a good algorithm for near-infinite rounds.

## Credits
#### Development Guide(s)
- [Bloons TD Tutorial](https://www.youtube.com/watch?v=Iy03ja20qz0)
- [Anchor Points](https://www.youtube.com/watch?v=jcw4cBJbvrc)
- [Resolution Scaling](https://www.youtube.com/watch?v=hXU-ZJb6GHw)
- [Shop UI](https://www.youtube.com/watch?v=1-_-716Ouy8)
#### Assets
- [Images: Animal Cube - Cats](https://assetstore.unity.com/packages/2d/animal-cube-cat-series-2d-asset-208164)
- [Images: Animal Cube - Ducks](https://assetstore.unity.com/packages/2d/animal-cube-duck-series-2d-asset-222908)
- [Plugin: Auto Letterbox (Auto Scaling)](https://assetstore.unity.com/packages/tools/camera/auto-letterbox-56814)