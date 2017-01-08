using System;
using BiPolarTowerDefence.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BiPolarTowerDefence.Entities
{
    public class Tower: BaseObject, IMyGameDrawable
    {
        private readonly Level _level;
        private Texture2D _texture;
        private int _count = 0;
        private TowerTechLevel _tech = TowerTechLevel.Base;
        private int towerRange = Tile.TILE_SIZE*3;
        private int rateOfFire = 60;
        private string turretTargetSetting = "LowHealth..";
        private float projectileSpeed = 25f;
        public TowerType type = TowerType.Normal;
        private bool _isSelected;
        private Vector3 shotVector;


        public Tower(Level level, Vector3 position) : base(level._game, position)
        {
            _level = level;
            this.Initialize();
            _isSelected = false;
        }

        public override void Initialize()
        {
            this._texture = Game.Content.Load<Texture2D>("tower");
            base.Initialize();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this._texture, new Vector2(this.position.X,this.position.Z),new Rectangle(0,0,100,100),Color.White,0f,new Vector2(50,80),0.5f,SpriteEffects.None, 0f );
        }

        public override void Update(GameTime gameTime)
        {

            switch (this._tech)
            {
                case TowerTechLevel.Base:
                    break;
                case TowerTechLevel.Tier1:
                    break;
            }

            if (_count++ % rateOfFire == 0)
            {
                this.shootBullet();
                Bullet.SpawnBullet(_level, this.position + new Vector3(0,0,0), shotVector*projectileSpeed, this, towerRange);
            }

            var pos = this._game.mouseState.Position;
            var rect = this.GetRect();
            if (rect.Contains(pos) && this._game.mouseState.LeftButton == ButtonState.Pressed)
            {
                this._isSelected = true;
            }
            base.Update(gameTime);
        }

        private void shootBullet()
        {
            Enemy myTarget = null;
            float currentDistance =float.MaxValue;
            int currentlife =int.MaxValue;

            foreach (var item in _level.getComponents())
            {
                var enemy = item as Enemy;
                if (enemy != null && enemy.Enabled)
                {
                    var distance = (this.position - enemy.position).Length();
                    if (distance < (float) towerRange)
                    {
                        var enemyLifeStatus = enemy.Life;
                        var enemytypeStatus = enemy.EnemyType;
                        switch (turretTargetSetting)
                        {
                            case "LowHealth":
                                if (enemyLifeStatus < currentlife)
                                {
                                    myTarget = enemy;
                                    currentDistance = distance;
                                    currentlife = enemyLifeStatus;
                                }
                                break;
                            case "HighHealth":
                                if (enemyLifeStatus > currentlife)
                                {
                                    myTarget = enemy;
                                    currentDistance = distance;
                                    currentlife = enemyLifeStatus;
                                }
                                break;
                            default:
                                if (distance < currentDistance)
                                {
                                    myTarget = enemy;
                                    currentDistance = distance;
                                    currentlife = enemyLifeStatus;
                                }
                                break;

                        }


//                        if (distance < towerRange && (int) this.type == (int) enemy.EnemyType)
//                        {
//                            myTarget = enemy;
//                            continue;
//                        }
                    }
                }

            }
            if (myTarget != null && myTarget.Enabled)
            {
                Vector3 targetVector = new Vector3(0,0,0);
                Vector3 targetVelocityVector = new Vector3(0,0,0);
                Vector3 enemyVelocityVector = new Vector3(0,0,0);
                targetVector = myTarget.position - this.position;
                targetVector.Normalize();
                targetVelocityVector = targetVector * projectileSpeed;
                enemyVelocityVector = myTarget.VelocityVector;
                shotVector = targetVelocityVector + enemyVelocityVector;
                shotVector.Normalize();
                Console.WriteLine(shotVector);

            }


        }

        public void OnUpgrade()
        {
            switch (this._tech)
            {
                case TowerTechLevel.Base:
                    break;
                case TowerTechLevel.Tier1:
                    //Tower er til.
                    break;
                case TowerTechLevel.Tier2:
                    // upgrade AOE
                    break;
                case TowerTechLevel.Tier3:
                    towerRange = 500;
                    break;
                case TowerTechLevel.Tier4:
                     rateOfFire =  30;
                    break;
            }
        }




    }

}