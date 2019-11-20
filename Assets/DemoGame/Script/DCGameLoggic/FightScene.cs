using DC.ActorSystem;
using DC.DCResourceSystem;
using UnityEngine;

namespace DC.GameLogic
{
    /// <summary>
    /// 如果使用了物理，创建actor前需要先加载场景，不然actor会掉下去
    /// 新建的actor是
    ///     其它玩家
    ///     新的npc
    /// </summary>
    public class FightScene : Singleton<FightScene>
    {

        public GameActor CreateActor(HeroCfg heroCfg, int actorId)
        {
            return null;
        }

        /// <summary>
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Vector3 GetBirthPosition(int index, ActorSide side)
        {
            if (ActorSide.blue == side)
            {
                var blueCenter = new Vector3(4,0,4);
                blueCenter.x -= (index * 2);
            }
            else if (ActorSide.red == side)
            {
                var blueCenter = new Vector3(4, 0, -4);
                blueCenter.x -= (index * 2);
            }

            return Vector3.zero;
        }

        private void CreateDemoHeroFighter()
        {
            var fighterCfg = MockSystem.Instance.DemoFighterCfg();
            fighterCfg.BuildDerivedData();

            var heroPrefab = ResourceSys.Instance.Load<GameObject>(fighterCfg.mPrefabPath);

            var hero = Object.Instantiate(heroPrefab, GameMain.Instance.RootTf);

            var actor = hero.GetComponent<GameActor>();
            actor.SetHeroCfg(fighterCfg);
            actor.SetActorSide(ActorSide.blue);
            actor.SetModel(fighterCfg.mModelPath);
            actor.UpdateModel();

            hero.GetComponent<HeroEntity>().mHeroCfg = fighterCfg;
            hero.transform.position = new Vector3(2, 0, 0);
        }

        private void CreateDemoEnemyFighter(Vector3 pos)
        {
            var fighterCfg = MockSystem.Instance.DemoEnemyFighterCfg();
            fighterCfg.BuildDerivedData();

            var heroPrefab = ResourceSys.Instance.Load<GameObject>(fighterCfg.mPrefabPath);

            var hero = Object.Instantiate(heroPrefab, GameMain.Instance.RootTf);

            var actor = hero.GetComponent<GameActor>();
            actor.SetHeroCfg(fighterCfg);
            actor.SetModel(fighterCfg.mModelPath);
            actor.UpdateModel();

            hero.GetComponent<HeroEntity>().mHeroCfg = fighterCfg;
            hero.transform.position = pos;
        }
    }

    public class CSActorItem
    {
        public HeroCfg mHeroCfg;

        public int actorId;

    }
}