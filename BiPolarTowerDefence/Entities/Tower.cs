using System;
using BiPolarTowerDefence.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BiPolarTowerDefence.Entities
{
    public class Tower: BaseObject, IMyGameDrawable
    {
        private const int SpriteAnimationHeight = 600;
        private const int SpriteAnimationWidth = 600;
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
        private Vector3 shotVector = new Vector3(1f,0f,0f);



        public Tower(Level level, Vector3 position) : base(level._game, position)
        {
            _level = level;
            this.Initialize();
            _isSelected = false;

            this.width = Tile.TILE_SIZE;
            this.height = Tile.TILE_SIZE;
        }

        public override void Initialize()
        {
            this._texture = Game.Content.Load<Texture2D>("gun");
            base.Initialize();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_tech == TowerTechLevel.Base)
            {

                var t = new Texture2D(_game.GraphicsDevice, 1,1,false, SurfaceFormat.Color);
                t.SetData(new[]{Color.White});

                spriteBatch.Draw(t,this.GetRect(),Color.LightSlateGray);
                return;
            }

            var animationIndex = 0;
            var angleDiff = Math.Abs(Math.Abs(shotVector.X) - Math.Abs(shotVector.Z));
            if (angleDiff > 0.5)
            {
                //Horizantal
                if (Math.Abs(shotVector.X) > Math.Abs(shotVector.Z))
                {
                    if (shotVector.X > 0)
                    {
                        animationIndex = 0;//right
                    }
                    else
                    {
                        animationIndex = 4;//Left

                        Console.WriteLine(shotVector);
                        Console.WriteLine(angleDiff);
                        Console.WriteLine("Left");
                    }
                }
                else //Vertical
                {
                    if (shotVector.Z < 0) //Up
                    {
                        animationIndex = 6;
                    }
                    else
                    {
                        animationIndex = 2;
                    }
                }
            }
            else
            {
                //Horizantal
                if (shotVector.Z < 0)
                {
                    if (shotVector.X > 0)
                    {
                        animationIndex = 7;//Up right
                    }
                    else
                    {
                        animationIndex = 5; //up left
                    }
                }
                else //Vertical
                {
                    if (shotVector.X > 0) //Down right
                    {
                        animationIndex = 1;
                        //Console.WriteLine("Down right");
                    }
                    else // Down left
                    {
                        animationIndex = 3;
                        //Console.WriteLine("Down left");
                    }
                }
            }

            var towerColor = Color.White;
            switch (type)
            {
                case TowerType.Earthy:
                    towerColor = Color.Green;
                    break;
                case TowerType.Fiery:
                    towerColor = Color.Red;
                    break;
                case TowerType.Frosty:
                    towerColor = Color.Blue;
                    break;
            }

            //spriteBatch.Draw(this._texture, this.GetRect(),new Rectangle(0,0,SpriteAnimationWidth,SpriteAnimationHeight),Color.White,0f,new Vector2(this.height - Tile.TILE_SIZE/2, this.width/2),SpriteEffects.None, 1f );
            spriteBatch.Draw(this._texture, this.GetRect(),new Rectangle(animationIndex * SpriteAnimationWidth,0,SpriteAnimationWidth,SpriteAnimationHeight),towerColor,0f,new Vector2(0,0),SpriteEffects.None, 1f );
        }

        public override void Update(GameTime gameTime)
        {

            switch (this._tech)
            {
                case TowerTechLevel.Base:
                    return; //Don't do anything if we are no tower at all
                    break;
                case TowerTechLevel.Tier1:
                    break;
            }

            if (_count++ % rateOfFire == 0)
            {
                this.shootBullet();
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
                Bullet.SpawnBullet(_level, this.getCenterLocation() + shotVector*20, shotVector*projectileSpeed, this, towerRange);
            }


        }

        private void TierUp()
        {
            if (_tech == TowerTechLevel.Base)
            {
                if (_level.PayUp(10))
                {
                    _tech = TowerTechLevel.Tier1;
                    OnUpgrade();
                }

            }
            else if (_tech == TowerTechLevel.Tier1)
            {
                if (_level.PayUp(500))
                {
                    _tech = TowerTechLevel.Tier2;
                    OnUpgrade();
                }
            }
            else if (_tech == TowerTechLevel.Tier2)
            {
                if (_level.PayUp(1000))
                {
                    _tech = TowerTechLevel.Tier3;
                    OnUpgrade();
                }
                else if (_tech == TowerTechLevel.Tier3)
                {
                    if (_level.PayUp(2000))
                    {
                        _tech = TowerTechLevel.Tier4;
                        OnUpgrade();
                    }
                }

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


        private Vector3 getCenterLocation()
        {
            return this.position + (new Vector3(this.width, 0, this.height))/2;
        }

        private float VectorToAngle(Vector3 vector)
        {
            var myVector = new Vector2(vector.X, vector.Z);
            if (myVector.Length() < 1)
            {
                myVector = Vector2.UnitY * -1;
            }
            myVector.Normalize();

            return (int)Math.Atan2(myVector.Y, myVector.X);
        }
    }

}