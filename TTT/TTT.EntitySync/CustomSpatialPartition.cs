using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AltV.Net.EntitySync;
using AltV.Net.EntitySync.SpatialPartitions;

namespace TTT.EntitySync
{
    /// <summary>
    ///     THIS OVERRIDE IS ONLY USED BY BLIPMANAGER(Static) CAUSE THIS TYPE OF BLIP ARE ONLY DESIGNED TO BE STATIC.
    ///     WE NEED TO VERIFY DIMENSION ONLY INSTEAD OF POSITION + DIMENSION.
    /// </summary>
    public class GlobalEntity : SpatialPartition
    {
        private readonly HashSet<IEntity> _entities = new();

        public override void Add(IEntity entity)
        {
            this._entities.Add(entity);
        }

        public override void Remove(IEntity entity)
        {
            this._entities.Remove(entity);
        }

        private static bool CanSeeOtherDimension(int dimension, int otherDimension)
        {
            return dimension switch
            {
                > 0 => dimension == otherDimension || otherDimension == int.MinValue,
                < 0 => otherDimension == 0 || dimension == otherDimension || otherDimension == int.MinValue,
                _ => otherDimension is 0 or int.MinValue
            };
        }

        public override IList<IEntity> Find(Vector3 position, int dimension)
        {
            return this._entities.Where(entity => CanSeeOtherDimension(dimension, entity.Dimension)).ToList();
        }

        public override void UpdateEntityPosition(IEntity entity, in Vector3 oldPosition, in Vector3 newPosition)
        {
        }

        public override void UpdateEntityRange(IEntity entity, uint oldRange, uint newRange)
        {
        }

        public override void UpdateEntityDimension(IEntity entity, int oldDimension, int newDimension)
        {
        }
    }
}