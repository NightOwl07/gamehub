using System.Collections.Generic;
using System.Numerics;
using AltV.Net.Data;
using AltV.Net.EntitySync;

namespace TTT.EntitySync
{
    /// <summary>
    ///     Blip class that stores all data related to a single blip.
    /// </summary>
    public class Blip : Entity, IEntity
    {
        public Blip(Vector3 position, int dimension, uint range, ulong entityType) : base(entityType, position,
            dimension, range)
        {
        }

        /// <summary>
        ///     The text to display on the blip in the map menu
        /// </summary>
        public string Name
        {
            get
            {
                if (!this.TryGetData("name", out string name))
                    return null;

                return name;
            }
            set => this.SetData("name", value);
        }

        /// <summary>
        ///     ID of the sprite to use, can be found on the ALTV wiki
        /// </summary>
        public int Sprite
        {
            get
            {
                if (!this.TryGetData("sprite", out int spriteId))
                    return 0;

                return spriteId;
            }
            set => this.SetData("sprite", value);
        }

        /// <summary>
        ///     Blip Color code, can also be found on the ALTV wiki
        /// </summary>
        public int Color
        {
            get
            {
                if (!this.TryGetData("color", out int color))
                    return 0;

                return color;
            }
            set => this.SetData("color", value);
        }

        /// <summary>
        ///     Scale of the blip, 1 is regular size.
        /// </summary>
        public float Scale
        {
            get
            {
                if (!this.TryGetData("scale", out float scale))
                    return 1;

                return scale;
            }
            set => this.SetData("scale", value);
        }

        /// <summary>
        ///     Whether this blip can be seen on the minimap from anywhere on the map, or only when close to it(it will always show
        ///     on the main map).
        /// </summary>
        public bool ShortRange
        {
            get
            {
                if (!this.TryGetData("shortRange", out bool shortRange))
                    return true;

                return shortRange;
            }
            set => this.SetData("shortRange", value);
        }

        public bool IsStaticBlip { get; set; }

        /// <summary>
        ///     Destroy this blip.
        /// </summary>
        public void Delete()
        {
            AltEntitySync.RemoveEntity(this);
        }

        public void SetPosition(Position pos)
        {
            this.Position = pos;
        }

        public void SetBlipType(int type)
        {
            //TODO Transformer in runtime un blip static en dynamique et inversement.
        }
    }

    public static class BlipStreamer
    {
        public static Dictionary<ulong, Blip> BlipList = new();

        /// <summary>
        ///     Create static blip without any range limit
        /// </summary>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="scale"></param>
        /// <param name="shortRange"></param>
        /// <param name="spriteId"></param>
        /// <param name="position"></param>
        /// <param name="dimension"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public static Blip CreateStaticBlip(string name, int color, float scale, bool shortRange, int spriteId,
            Vector3 position, int dimension, uint range = 100)
        {
            Blip blip = new(position, dimension, range, 4)
            {
                Color = color,
                Scale = scale,
                ShortRange = shortRange,
                Sprite = spriteId,
                Name = name,
                IsStaticBlip = true
            };
            AltEntitySync.AddEntity(blip);
            BlipList.Add(blip.Id, blip);
            return blip;
        }

        /// <summary>
        ///     Create Dynamic Blip.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="scale"></param>
        /// <param name="shortRange"></param>
        /// <param name="spriteId"></param>
        /// <param name="position"></param>
        /// <param name="dimension"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public static Blip CreateDynamicBlip(string name, int color, float scale, bool shortRange, int spriteId,
            Vector3 position, int dimension, uint range = 200)
        {
            Blip blip = new(position, dimension, range, 5)
            {
                Color = color,
                Scale = scale,
                ShortRange = shortRange,
                Sprite = spriteId,
                Name = name,
                IsStaticBlip = false
            };
            AltEntitySync.AddEntity(blip);
            BlipList.Add(blip.Id, blip);
            return blip;
        }

        /// <summary>
        ///     Destroy a dynamic blip
        /// </summary>
        /// <param name="blip">The blip to destroy</param>
        public static void DestroyBlip(Blip blip)
        {
            BlipList.Remove(blip.Id);
            AltEntitySync.RemoveEntity(blip);
        }

        public static Blip GetBlip(ulong dynamicObjectId)
        {
            return BlipList[dynamicObjectId];
        }

        public static List<Blip> GetAllBlip()
        {
            List<Blip> objects = new();

            foreach (KeyValuePair<ulong, Blip> entity in BlipList)
            {
                Blip obj = GetBlip(entity.Key);

                if (obj != null)
                    objects.Add(obj);
            }

            return objects;
        }

        public static (Blip obj, float distance) GetClosestBlip(Vector3 pos)
        {
            if (GetAllBlip().Count == 0)
                return (null, 5000);

            Blip obj = null;
            float distance = 5000;

            foreach (Blip o in GetAllBlip())
            {
                float dist = Vector3.Distance(o.Position, pos);
                if (dist < distance)
                {
                    obj = o;
                    distance = dist;
                }
            }

            return (obj, distance);
        }
    }
}