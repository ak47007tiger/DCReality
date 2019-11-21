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
        public void Init(HeroCfg player, HeroCfg ai)
        {
            {
                var actor = CreateHero(player, PlayerDataMgr.Instance.GetMainActorId());
                actor.SetActorSide(ActorSide.blue);
                var birthPosition = GetBirthPosition(0, ActorSide.blue);
                actor.transform.position = birthPosition;

                ActorSys.Instance.AddActor(actor.GetActorId(), actor);

                ActorSys.Instance.SetMainActor(actor);
            }

            for (var i = 0; i < 5; i++)
            {
                var actor = CreateHero(ai, PlayerDataMgr.Instance.GenerateActorId());
                actor.SetActorSide(ActorSide.red);
                var birthPosition = GetBirthPosition(i, ActorSide.red);
                actor.transform.position = birthPosition;

                ActorSys.Instance.AddActor(actor.GetActorId(), actor);
            }
        }

        public GameActor CreateHero(HeroCfg heroCfg, int actorId)
        {
            var heroPrefab = ResourceSys.Instance.Load<GameObject>(heroCfg.mPrefabPath);

            var hero = Object.Instantiate(heroPrefab, GameMain.Instance.RootTf);

            var actor = hero.GetComponent<GameActor>();
            actor.SetHeroCfg(heroCfg);
            actor.SetModel(heroCfg.mModelPath);
            actor.UpdateModel();

            hero.GetComponent<HeroEntity>().mHeroCfg = heroCfg;

            return actor;
        }

        public GameActor CreateNpc(NPCConfig npcCfg, int actorId)
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
                return blueCenter;
            }

            if (ActorSide.red == side)
            {
                var redCenter = new Vector3(4, 0, -4);
                redCenter.x -= (index * 2);
                return redCenter;
            }

            return Vector3.zero;
        }

        private void CreateDemoHeroFighter()
        {
            var fighterCfg = MockSystem.Instance.DemoFighterCfg();

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