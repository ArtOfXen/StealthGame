﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace Game1
{
    class PullProjectile : Projectile
    {
        double pullStartSeconds;
        double currentSeconds;
        float pullAnimationLength;
        public static bool inUse { get; private set; } = false; // only one pull projectile allowed at once

        public PullProjectile(ActorModel projectileModel, Vector3 startPosition, int movementSpeed, float angleOfFire) : 
            base (ProjectileClassification.pull, projectileModel, startPosition, movementSpeed, angleOfFire)
        {
            actionEffectMinimumArea = new BoundingSphere(startPosition, modelData.boxSize.X / 2);
            actionEffectMaximumArea = new BoundingSphere(startPosition, modelData.boxSize.X * 10);

            pullAnimationLength = 0.5f;
            inUse = true;
        }

        public override void move(Vector3? changeInPosition = null, bool checkTerrainCollision = true)
        {
            if (!actionStarted && !MovementBlocked)
            {
                displace(new Vector3((float)Math.Sin(MathHelper.ToRadians(currentYawAngleDeg)), 0f, (float)Math.Cos(MathHelper.ToRadians(currentYawAngleDeg))));

                actionEffectMinimumArea = new BoundingSphere(position, modelData.boxSize.X / 2);
                actionEffectMaximumArea = new BoundingSphere(position, modelData.boxSize.X * 10);
            }
            else if (actionStarted)
            {
                currentSeconds = DateTime.Now.TimeOfDay.TotalSeconds;

                if (currentSeconds > pullStartSeconds + pullAnimationLength)
                {
                    requiresDeletion = true;
                    inUse = false;
                }
            }

        }

        public override void startAction()
        {
            base.startAction();
            pullStartSeconds = DateTime.Now.TimeOfDay.TotalSeconds;
        }

        
    }
}
